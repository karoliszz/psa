namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;

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

		if (BlockEmptyCartAccess && !cart.Items.Any())
		{
			return NotFound();
		}

		return View(cart);
	}
}