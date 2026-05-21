namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.Collections.Generic;
using System.Linq;
public class RestaurantViewModel
    {
        public int Id { get; set; }
        public double KoordinateX { get; set; }
        public double KoordinateY { get; set; }
        public string Pavadinimas { get; set; }
        public string Aprasas { get; set; }
        public string Adresas { get; set; }
        public string SavaitesPradziosVal { get; set; }
        public string SavaitesPabaigosVal { get; set; }
        public bool ArPatvirtintas { get; set; }
    }