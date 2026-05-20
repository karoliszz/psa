namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

public class MenuController : ControllerBase
{
    // Laikinas hardcoded meniu – pirmam restoranui
    private const int DemoRestoranasId = 1;
    private const int DemoUserId = 1;

    [HttpGet]
    public IActionResult Index()
    {
        var menu = MenuRepo.GetMenuForRestaurant(DemoRestoranasId);
        return View(menu);
    }

    [HttpGet]
    public IActionResult Dish(int id)
    {
        var dish = MenuRepo.GetDish(DemoRestoranasId, id);
        return View(dish);
    }

    [HttpPost]
    public IActionResult AddToCart(DishViewModel model, List<int> selectedVariants)
    {
        int userId = DemoUserId;
        int restId = model.RestoranasId;

        // 1) Užtikriname, kad vartotojas turi krepšelį šiame restorane
        int cartId = CartRepo.EnsureCartExists(userId, restId);

        // 2) Perskaičiuojame galutinę kainą (bazinė + pasirinktų variantų pokyčiai)
        decimal extra = 0m;

        if (selectedVariants != null && model.Kategorijos != null)
        {
            foreach (var cat in model.Kategorijos)
            {
                if (cat.Variantai == null) continue;

                foreach (var v in cat.Variantai)
                {
                    if (selectedVariants.Contains(v.Id))
                        extra += v.KainosPokytis;
                }
            }
        }

        decimal finalPrice = model.BazineKaina + extra;

        // 3) Įrašome į krepselioirasas
        int cartItemId = CartRepo.InsertCartItem(cartId, model, finalPrice);

        // 4) Įrašome pasirinktus variantus į pasirenka
        if (selectedVariants != null && selectedVariants.Any())
        {
            CartRepo.InsertChoices(cartItemId, selectedVariants);
        }

        // 5) Parodome popup per TempData ir grįžtam į meniu
        TempData["added"] = true;
        return RedirectToAction("Index", "Menu");
    }
}
