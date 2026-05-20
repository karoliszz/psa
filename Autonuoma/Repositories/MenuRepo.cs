public static class MenuRepo
{
    public static List<DishViewModel> GetMenuForRestaurant(int restId)
    {
        return new List<DishViewModel>
        {
            new DishViewModel
            {
                Id = 2,
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
                        ArKeliPasirinkimai = true,
                        ArPrivalomas = true,
                        Variantai = new()
                        {
                            new ChoiceVariantVM { Id = 1, Pavadinimas = "Mažas", KainosPokytis = 0m },
                            new ChoiceVariantVM { Id = 2, Pavadinimas = "Vidutinis", KainosPokytis = 1.00m },
                            new ChoiceVariantVM { Id = 3, Pavadinimas = "Didelis", KainosPokytis = 2.00m }
                        }
                    },
                    new ChoiceCategoryVM
                    {
                        Id = 2,
                        Pavadinimas = "Padažas",
                        ArKeliPasirinkimai = false,
                        ArPrivalomas = true,
                        Variantai = new()
                        {
                            new ChoiceVariantVM { Id = 4, Pavadinimas = "BBQ", KainosPokytis = 0 },
                            new ChoiceVariantVM { Id = 5, Pavadinimas = "Aštrus", KainosPokytis = 0.50m },
                            new ChoiceVariantVM { Id = 6, Pavadinimas = "Česnakinis", KainosPokytis = 0.50m }
                        }
                    }
                }
            }
        };
    }

    public static DishViewModel GetDish(int restId, int dishId)
    {
        return GetMenuForRestaurant(restId).First(x => x.Id == dishId);
    }
}