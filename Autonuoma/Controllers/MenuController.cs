namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

public class MenuController : ControllerBase
{
    private const int DemoUserId = 1;

    [HttpGet]
    public IActionResult Index(int restId)
    {
        // save currently opened restaurant in session
        HttpContext.Session.SetInt32("CurrentRestaurantId", restId);

        var menu =
            MenuRepo.GetMenuForRestaurant(restId);

        ViewBag.RestaurantId = restId;

        return View(menu);
    }

    [HttpGet]
    public IActionResult Dish(int id, int restId)
    {
        var dish =
            MenuRepo.GetDish(restId, id);

        return View(dish);
    }

    [HttpPost]
    public IActionResult AddToCart(
        DishViewModel model,
        List<int> selectedVariants)
    {
        int userId = DemoUserId;

        int restId = model.RestoranasId;

        int cartId =
            CartRepo.EnsureCartExists(
                userId,
                restId
            );

        decimal extra = 0m;

        if (selectedVariants != null &&
            model.Kategorijos != null)
        {
            foreach (var cat in model.Kategorijos)
            {
                if (cat.Variantai == null)
                    continue;

                foreach (var v in cat.Variantai)
                {
                    if (selectedVariants.Contains(v.Id))
                    {
                        extra += v.KainosPokytis;
                    }
                }
            }
        }

        decimal finalPrice =
            model.BazineKaina + extra;

        int cartItemId =
            CartRepo.InsertCartItem(
                cartId,
                model,
                finalPrice
            );

        if (selectedVariants != null &&
            selectedVariants.Any())
        {
            CartRepo.InsertChoices(
                cartItemId,
                selectedVariants
            );
        }

        TempData["added"] = true;

        return RedirectToAction(
            "Index",
            "Menu",
            new { restId = restId }
        );
    }
}