public class DishViewModel
{
    public int Id { get; set; }
    public int RestoranasId { get; set; }
    public string Pavadinimas { get; set; }
    public string Aprasas { get; set; }
    public decimal BazineKaina { get; set; }

    public int Kiekis { get; set; } = 1;

    public List<ChoiceCategoryVM> Kategorijos { get; set; } = new();
}

public class ChoiceCategoryVM
{
    public int Id { get; set; }
    public string Pavadinimas { get; set; }
    public bool ArKeliPasirinkimai { get; set; }
    public bool ArPrivalomas { get; set; }

    public List<ChoiceVariantVM> Variantai { get; set; } = new();
}

public class ChoiceVariantVM
{
    public int Id { get; set; }
    public string Pavadinimas { get; set; }
    public decimal KainosPokytis { get; set; }
}
