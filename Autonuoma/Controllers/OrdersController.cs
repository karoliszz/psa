namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

public class OrdersController : Controller
{
    private const int DemoUserId = 2;

    // GET: /Orders
    [HttpGet]
    public IActionResult Index()
    {
        var orders = OrdersRepo.GetUserOrders(DemoUserId);
        return View(orders);
    }

    // GET: /Orders/Order/5
    [HttpGet]
    public IActionResult Order(int id)
    {
        var order = OrdersRepo.GetOrderDetails(id, DemoUserId);

        if (order == null)
            return RedirectToAction(nameof(Index));

        return View(order);
    }
}