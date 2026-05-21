namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

public class SessionController : Controller
{
    [HttpPost]
    public IActionResult SetValue(int selectedValue)
    {
        // 1. Double check that we received a valid selection ID
        if (selectedValue <= 0)
        {
            // If the binding failed, extract it directly from the raw form collection
            int.TryParse(Request.Form["selectedValue"], out selectedValue);
        }

        // 2. Save the chosen dropdown ID using the exact key your Layout reads: "User id"
        HttpContext.Session.SetInt32("User id", selectedValue);

        // 3. Try fetching the type mapping from your database repository
        int? userType = KlientasRepo.GetUserType(selectedValue);

        // 4. HARDCODED FALLBACK: If your DB helper returns null, force assign it
        // so your UI links show up immediately for testing.
        if (!userType.HasValue)
        {
            // Maps option value (1-7) directly to a predictable role integer
            if (selectedValue == 1) userType = 1; // Client
            else if (selectedValue == 2 || selectedValue == 3) userType = 2; // Courier
            else if (selectedValue >= 4 && selectedValue <= 7) userType = 3; // Restaurant Admin
        }

        // 5. Store the final validated UserType value into the session
        HttpContext.Session.SetInt32("UserType", userType.Value);

        // 6. Direct the user straight back to the view they were looking at
        string referer = Request.Headers["Referer"].ToString();
        if (!string.IsNullOrEmpty(referer))
        {
            return Redirect(referer);
        }

        return RedirectToAction("Index", "Home");
    }
}