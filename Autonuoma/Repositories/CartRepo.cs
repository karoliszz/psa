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
}
