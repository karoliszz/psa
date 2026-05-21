namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using System.Collections.Generic;
using System.Linq;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;

public static class MenuRepo
{
    public static List<DishViewModel> GetMenuForRestaurant(int restId)
    {
        // RESTAURANT 1
        if (restId == 1)
        {
            return new List<DishViewModel>
            {
                new DishViewModel
                {
                    Id = 1,
                    RestoranasId = 1,
                    Pavadinimas = "BBQ Burger",
                    Aprasas = "Sultingas jautienos burgeris su BBQ padažu",
                    BazineKaina = 9.5m,

                    Kategorijos = new()
                    {
                        new ChoiceCategoryVM
                        {
                            Id = 1,
                            Pavadinimas = "Dydis",
                            ArKeliPasirinkimai = false,
                            ArPrivalomas = true,

                            Variantai = new()
                            {
                                new ChoiceVariantVM
                                {
                                    Id = 1,
                                    Pavadinimas = "Mažas",
                                    KainosPokytis = 0m
                                },

                                new ChoiceVariantVM
                                {
                                    Id = 2,
                                    Pavadinimas = "Didelis",
                                    KainosPokytis = 2m
                                }
                            }
                        }
                    }
                },

                new DishViewModel
                {
                    Id = 2,
                    RestoranasId = 1,
                    Pavadinimas = "Bulvytės",
                    Aprasas = "Traškios bulvytės",
                    BazineKaina = 3.5m
                }
            };
        }

        // RESTAURANT 2
        if (restId == 2)
        {
            return new List<DishViewModel>
            {
                new DishViewModel
                {
                    Id = 10,
                    RestoranasId = 2,
                    Pavadinimas = "Suši rinkinys",
                    Aprasas = "12 dalių sushi rinkinys",
                    BazineKaina = 14m
                },

                new DishViewModel
                {
                    Id = 11,
                    RestoranasId = 2,
                    Pavadinimas = "Miso sriuba",
                    Aprasas = "Japoniška sriuba",
                    BazineKaina = 4m
                }
            };
        }

        return new List<DishViewModel>();
    }

    public static DishViewModel GetDish(
        int restId,
        int dishId)
    {
        return GetMenuForRestaurant(restId)
            .First(x => x.Id == dishId);
    }
}