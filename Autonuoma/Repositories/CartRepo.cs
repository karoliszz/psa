namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using System.Collections.Generic;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;

/// <summary>
/// Database operations related to 'Cart' system.
/// </summary>
public class CartRepo : RepoBase
{
	public static List<CartItemViewModel> ListItems(int cartId)
	{
		var query = $@"
			SELECT 
				p.id AS id,
				p.Pavadinimas AS pavadinimas,
				p.Aprasas AS aprasas,
				ki.Kaina AS kaina,
				ki.Kiekis AS kiekis,
				p.ArParduodamas AS ar_parduodamas
			FROM `{Config.TblPrefix}krepselioirasas` ki
			INNER JOIN `{Config.TblPrefix}patiekalas` p ON ki.fk_Patiekalasid = p.id
			WHERE ki.fk_Krepselisid = ?cartId";

		var drc = Sql.Query(query, args => {
			args.Add("?cartId", cartId);
		});

		var result = Sql.MapAll<CartItemViewModel>(drc, (dre, t) => {
			t.Id = dre.From<int>("id");
			t.Pavadinimas = dre.From<string>("pavadinimas");
			t.Aprasas = dre.From<string>("aprasas");
			t.Kaina = dre.From<decimal>("kaina");
			t.Kiekis = dre.From<int>("kiekis");
			t.ArParduodamas = dre.From<bool>("ar_parduodamas");
		});

		return result;
	}

	public static int EnsureCartExists(int userId, int restId)
	{
		var drc = Sql.Query($@"
			SELECT id 
			FROM `{Config.TblPrefix}krepselis`
			WHERE fk_Vartotojasid = ?uid AND fk_Restoranasid = ?rid
			LIMIT 1",
			args => {
				args.Add("?uid", userId);
				args.Add("?rid", restId);
			}
		);

		if (drc.Count > 0)
			return Convert.ToInt32(drc[0]["id"]);

		var newId = Sql.Insert($@"
			INSERT INTO `{Config.TblPrefix}krepselis`
			(KoordinateX, KoordinateY, PristatymoAdresas, Kaina, fk_Vartotojasid, fk_Restoranasid)
			VALUES (0, 0, '', 0, ?uid, ?rid)",
			args => {
				args.Add("?uid", userId);
				args.Add("?rid", restId);
			}
		);

		return (int)newId;
	}



	public static int InsertCartItem(int cartId, DishViewModel dish, decimal finalPrice)
{
    var newId = Sql.Insert($@"
        INSERT INTO `{Config.TblPrefix}krepselioirasas`
        (Kaina, Kiekis, fk_Patiekalasid, fk_Patiekalasfk_Restoranasid, fk_Krepselisid)
        VALUES (?kaina, ?kiekis, ?pid, ?rid, ?cid)",
        args =>
        {
            args.Add("?kaina", finalPrice);
            args.Add("?kiekis", dish.Kiekis);
            args.Add("?pid", dish.Id);
            args.Add("?rid", dish.RestoranasId);
            args.Add("?cid", cartId);
        }
    );

    return (int)newId; // Sql.Insert grąžina AUTO_INCREMENT ID
}


	public static void InsertChoices(int cartItemId, List<int> variantIds)
	{
		foreach (var v in variantIds)
		{
			Sql.Insert($@"
				INSERT INTO `{Config.TblPrefix}pasirenka`
				(fk_KrepselioIrasasid, fk_PasirinkimoVariantasid)
				VALUES (?ci, ?vid)",
				args => {
					args.Add("?ci", cartItemId);
					args.Add("?vid", v);
				}
			);
		}
	}
}
