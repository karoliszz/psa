namespace Org.Ktu.Isk.P175B602.Autonuoma;

using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;
using NLog;

/// <summary>
/// <para>Program entry class.</para>
/// <para>Static members are thread safe, instance members are not.</para>
/// </summary>
public class Program
{
    /// <summary>
    /// Logger for this class.
    /// </summary>
    Logger log = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Configure logging subsystem.
    /// </summary>
    private static void ConfigureLogging()
    {
        var config = new NLog.Config.LoggingConfiguration();

        var console =
            new NLog.Targets.ConsoleTarget("console")
            {
                Layout = @"${date:format=HH\:mm\:ss}|${level}| ${message} ${exception}"
            };

        config.AddTarget(console);
        config.AddRuleForAllLevels(console);

        LogManager.Configuration = config;
    }

    /// <summary>
    /// Program entry point.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    public static void Main(string[] args)
    {
        ConfigureLogging();

        var self = new Program();
        self.Run(args);
    }

    /// <summary>
    /// Program body.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    private void Run(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            // Set the address and port the Kestrel server should bind to
            builder.WebHost.ConfigureKestrel(opts =>
            {
                opts.Listen(System.Net.IPAddress.Loopback, 5000);
            });

            // Add services
            builder.Services
                .AddRazorPages()
                .AddRazorOptions(opts =>
                {
                    // This will allow having _Exception.cshtml as the root view
                    opts.ViewLocationFormats.Add("/Views/{0}.cshtml");
                });

            // SESSION SUPPORT
            builder.Services.AddDistributedMemoryCache(); // Required infrastructure for session storage

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2); // Retained the 2-hour window from main
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Build the web app
            var app = builder.Build();

            // Initialize configuration helper
            Config.CreateSingletonInstance(app.Configuration);

            // Add middleware to set request ID and no-cache headers
            app.Use(async (context, next) =>
            {
                // Set request ID to be able to correlate request descriptors
                context.Items["HttpRequestID"] = Guid.NewGuid().ToString();

                // Set no-cache headers
#pragma warning disable CA1861
                context.Response.Headers.CacheControl =
                    new[]
                    {
                        "no-store, no-cache, must-revalidate, max-age=0",
                        "post-check=0, pre-check=0"
                    };
#pragma warning restore CA1861

                context.Response.Headers.Pragma = "no-cache";

                // Invoke next middleware in chain
                await next();
            });

            // Middleware pipeline configuration
            app.UseStaticFiles();
            app.UseRouting();

            // ENABLE SESSION (Must run after UseRouting to map session cookies properly)
            app.UseSession();

            // HARDCODED USER SESSION FALLBACK
            app.Use(async (context, next) =>
            {
                if (context.Session.GetInt32("UserId") == null)
                {
                    context.Session.SetInt32("UserId", 1);
                }

                await next();
            });

            app.UseAuthorization();

            app.MapDefaultControllerRoute();
            app.MapRazorPages();

            // Run the web app
            app.Run();
        }
        catch (Exception e)
        {
            log.Error(e, "Unhandled exception caught when initializing program. The main thread is now dead.");
        }
    }
}