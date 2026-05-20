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
	/// <summary>
	/// Entity identifier.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Item name.
	/// </summary>
	public string Pavadinimas { get; set; }

	/// <summary>
	/// Item description.
	/// </summary>
	public string Aprasas { get; set; }

	/// <summary>
	/// Item price.
	/// </summary>
	public decimal Kaina { get; set; }

	/// <summary>
	/// Item quantity.
	/// </summary>
	public int Kiekis { get; set; } = 1;
	
	/// <summary>
	/// Item availability status.
	/// </summary>
	public bool ArParduodamas { get; set; } = true;
}