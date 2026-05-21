namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Ensures access to session extension methods
using System.Collections.Generic;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

public class OrdersController : Controller
{
    // Dynamically reads values from session middleware on every request execution
    private int SessionUserId => HttpContext.Session.GetInt32("User id") ?? 1;
    private int SessionUserType => HttpContext.Session.GetInt32("UserType") ?? 1; // Default fallback to Customer (1)

    // GET: /Orders
    [HttpGet]
    public IActionResult Index()
    {
        // Passes both credentials downstream to bypass database role lookups
        var orders = OrdersRepo.GetUserOrders(SessionUserId, SessionUserType);
        return View(orders);
    }

    // GET: /Orders/Order/5
    [HttpGet]
    public IActionResult Order(int id)
    {
        // Validates order scope context based on whether user is customer or courier
        var order = OrdersRepo.GetOrderDetails(id, SessionUserId, SessionUserType);

        if (order == null)
            return RedirectToAction(nameof(Index));

        return View(order);
    }

    // POST: /Orders/UpdateStatus
    [HttpPost]
    public IActionResult UpdateStatus(int orderId, string statusas)
    {
        // Controller-level security barrier: only allow Couriers (Type 2) to modify status metrics
        if (SessionUserType == 2)
        {
            OrdersRepo.UpdateOrderStatus(orderId, SessionUserId, statusas);

        }

        // Fresh redirect to update UI interface layouts
        return RedirectToAction(nameof(Index));
    }
}