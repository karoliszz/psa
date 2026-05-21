namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

public class RestaurantController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        int userId =
            HttpContext.Session.GetInt32("UserId") ?? 1;

        bool hasAddress =
            CartRepo.HasAddressInfo(userId);

        ViewBag.ShowAddressPopup =
            !hasAddress;

        // if no address yet show empty list
        if (!hasAddress)
        {
            return View(new List<object>());
        }

        var cartInfo =
            CartRepo.GetAddressInfo(userId);

        var restaurants =
            RestaurantRepo.ListRestaurants(
                cartInfo.KoordinateX,
                cartInfo.KoordinateY
            );

        return View(restaurants);
    }

    [HttpGet]
    public IActionResult Select(int id)
    {
        int userId =
            HttpContext.Session.GetInt32("UserId") ?? 1;

        if (!CartRepo.HasAddressInfo(userId))
        {
            return RedirectToAction(
                "Index",
                "Restaurant"
            );
        }

        return RedirectToAction(
            "Index",
            "Menu",
            new { restId = id }
        );
    }
}