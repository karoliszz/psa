
namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Cart view model.
/// </summary>
public class CartViewModel
{
	/// <summary>
	/// Collection of cart items.
	/// </summary>
	public IList<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

	/// <summary>
	/// Delivery fee amount.
	/// </summary>
	public decimal DeliveryFee { get; set; } = 1.50m;

	/// <summary>
	/// Saved address.
	/// </summary>
	public string PristatymoAdresas { get; set; } = "";

	/// <summary>
	/// X coordinate.
	/// </summary>
	public float KoordinateX { get; set; }

	/// <summary>
	/// Y coordinate.
	/// </summary>
	public float KoordinateY { get; set; }

	/// <summary>
	/// Subtotal of available items.
	/// </summary>
	public decimal Subtotal => Items.Where(i => i.ArParduodamas).Sum(i => i.Kaina * i.Kiekis);

	/// <summary>
	/// Indicates presence of available items.
	/// </summary>
	public bool HasAvailableItems => Items.Any(i => i.ArParduodamas);

	/// <summary>
	/// Indicates presence of unavailable items.
	/// </summary>
	public bool HasUnavailableItems => Items.Any(i => !i.ArParduodamas);

	/// <summary>
	/// Indicates checkout eligibility.
	/// </summary>
	public bool CanCheckout => HasAvailableItems && !HasUnavailableItems;

	/// <summary>
	/// Total cost including delivery fee.
	/// </summary>
	public decimal Total => HasAvailableItems ? Subtotal + DeliveryFee : 0m;
}

/// <summary>
/// Cart item view model.
/// </summary>
public class CartItemViewModel
{
	public int Id { get; set; }

	public string Pavadinimas { get; set; }

	public string Aprasas { get; set; }

	public decimal Kaina { get; set; }

	public int Kiekis { get; set; } = 1;

	public bool ArParduodamas { get; set; } = true;
}