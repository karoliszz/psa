namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using System.Collections.Generic;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;

public class RestaurantRepo : RepoBase
{
    public static List<RestaurantViewModel> ListRestaurants(
        float userX,
        float userY)
    {
        // 3km radius
        const double maxDistanceKm = 3.0;

        var query = $@"
            SELECT 
                id,
                KoordinateX,
                KoordinateY,
                Pavadinimas,
                Aprasas,
                Adresas,
                SavaitesPradziosVal,
                SavaitesPabaigosVal,
                ArPatvirtintas
            FROM `{Config.TblPrefix}restoranas`
            WHERE
            (
                6371 * ACOS(
                    COS(RADIANS(?userY))
                    * COS(RADIANS(KoordinateY))
                    * COS(RADIANS(KoordinateX) - RADIANS(?userX))
                    + SIN(RADIANS(?userY))
                    * SIN(RADIANS(KoordinateY))
                )
            ) <= ?maxDistance
        ";

        var drc = Sql.Query(query, args =>
        {
            args.Add("?userX", userX);
            args.Add("?userY", userY);
            args.Add("?maxDistance", maxDistanceKm);
        });

        var result = Sql.MapAll<RestaurantViewModel>(drc, (dre, t) =>
        {
            t.Id = dre.From<int>("id");

            t.KoordinateX =
                dre.From<double?>("KoordinateX") ?? 0;

            t.KoordinateY =
                dre.From<double?>("KoordinateY") ?? 0;

            t.Pavadinimas =
                dre.From<string>("Pavadinimas") ?? "";

            t.Aprasas =
                dre.From<string>("Aprasas") ?? "";

            t.Adresas =
                dre.From<string>("Adresas") ?? "";

            t.SavaitesPradziosVal =
                dre.From<string>("SavaitesPradziosVal") ?? "";

            t.SavaitesPabaigosVal =
                dre.From<string>("SavaitesPabaigosVal") ?? "";

            t.ArPatvirtintas =
                dre.From<int?>("ArPatvirtintas") == 1;
        });

        return result;
    }
}