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
	/// <summary>
	/// Configuration flag. If true, prevents access to the cart view when the cart is empty.
	/// </summary>
	private const bool BlockEmptyCartAccess = false;


    /// <summary>
    /// Handles 'Index' action for cart view.
    /// </summary>
    /// <returns>View to render.</returns>
    [HttpGet]
	public ActionResult Index()
	{
		var cart = new CartViewModel();
		cart.Items = CartRepo.ListItems(1);

		if (BlockEmptyCartAccess && !cart.Items.Any())
		{
			return NotFound();
		}

		return View(cart);
	}

	[HttpPost]
    public IActionResult SaveAddress(string address)
    {
        float[] coords = GetCoordinates(address);

        CartRepo.UpdateAddress(address, coords[0], coords[1]);

        return Ok();
    }

	private float[] GetCoordinates(string address)
	{

		if(address != null)
		{
			return [6f, 7f];
		}

		return [0, 0];

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