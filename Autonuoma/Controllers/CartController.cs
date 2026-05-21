namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;
using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

/// <summary>
/// Controller for cart operations.
/// </summary>
public class CartController : ControllerBase
{
	private const bool BlockEmptyCartAccess = false;

	[HttpGet]
	public ActionResult Index()
	{
		int userId =
			HttpContext.Session.GetInt32("UserId") ?? 1;

		var cart =
			CartRepo.GetAddressInfo(userId);

		cart.Items =
			CartRepo.ListItems(1);

		return View(cart);
	}

	[HttpPost]
	public IActionResult SaveAddress(
		string address,
		float coordinateX,
		float coordinateY)
	{
		int userId =
			HttpContext.Session.GetInt32("UserId") ?? 1;

		CartRepo.UpdateAddress(
			userId,
			address,
			coordinateX,
			coordinateY
		);

		return Ok();
	}

	[HttpPost]
	public IActionResult FinalizeOrder()
	{
		CartRepo.FinalizeOrder();


        return Json(new { success = true, redirectUrl = Url.Action("Index", "Order") });
    }

    [HttpPost]
    public IActionResult RemoveItem(int id)
    {
        // Assuming userId is 1 for demo; replace with actual user/session logic as needed
        bool removed = CartRepo.RemoveItem(id);


        if (removed)
            return Ok();
        else
            return NotFound();
    }


}