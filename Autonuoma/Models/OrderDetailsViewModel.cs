namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

public class OrderDetailsViewModel
{
    public int Id { get; set; }
    public float Kaina { get; set; }
    public List<OrderItemViewModel> Items { get; set; }
}