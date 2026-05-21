namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.Data; // Ensure this is at the top of your file if not already present
using System;
/// <summary>
/// Database operations related to 'Klientas' entity.
/// </summary>
public class KlientasRepo : RepoBase
{
	public static List<Klientas> List()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}klientai` ORDER BY asmens_kodas ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Klientas>(drc, (dre, t) => {
				t.AsmensKodas = dre.From<string>("asmens_kodas");
				t.Vardas = dre.From<string>("vardas");
				t.Pavarde = dre.From<string>("pavarde");
				t.GimimoData = dre.From<DateTime>("gimimo_data");
				t.Telefonas = dre.From<string>("telefonas");
				t.Epastas = dre.From<string>("epastas");
			});

		return result;
	}

	public static Klientas Find(string asmkodas)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}klientai` WHERE asmens_kodas=?asmkodas";

		var drc =
			Sql.Query(query, args => {
				args.Add("?asmkodas", asmkodas);
			});

		if( drc.Count > 0 )
		{
			var result = 
				Sql.MapOne<Klientas>(drc, (dre, t) => {
					t.AsmensKodas = dre.From<string>("asmens_kodas");
					t.Vardas = dre.From<string>("vardas");
					t.Pavarde = dre.From<string>("pavarde");
					t.GimimoData = dre.From<DateTime>("gimimo_data");
					t.Telefonas = dre.From<string>("telefonas");
					t.Epastas = dre.From<string>("epastas");
				});

			return result;
		}

		return null;
	}



    public static int? GetUserType(int id)
    {
        var query = $@"SELECT VartotojoTipas FROM `{Config.TblPrefix}klientai` WHERE id=?id";

        // 1. Safe-guard the database call inside a try-catch block
        try
        {
            var drc = Sql.Query(query, args => {
                args.Add("?id", id);
            });

            // 2. Check if the returned data row collection itself is null or empty
            if (drc == null || drc.Count == 0)
            {
                return null;
            }

            // 3. Double check that the row and column exists before converting
            if (drc[0] != null)
            {
                var rawValue = drc[0]["VartotojoTipas"];

                if (rawValue != DBNull.Value && rawValue != null)
                {
                    return Convert.ToInt32(rawValue);
                }
            }
        }
        catch (Exception ex)
        {
            // If your Sql utility wrapper or DB connection throws an exception, catch it safely
            System.Diagnostics.Debug.WriteLine($"Database error in GetUserType: {ex.Message}");
        }

        return null;
    }


    public static void Insert(Klientas klientas)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}klientai`
			(
				asmens_kodas,
				vardas,
				pavarde,
				gimimo_data,
				telefonas,
				epastas
			)
			VALUES(
				?asmkod,
				?vardas,
				?pavarde,
				?gimdata,
				?tel,
				?email
			)";

		Sql.Insert(query, args => {
			args.Add("?asmkod", klientas.AsmensKodas);
			args.Add("?vardas", klientas.Vardas);
			args.Add("?pavarde", klientas.Pavarde);
			args.Add("?gimdata", klientas.GimimoData);
			args.Add("?tel", klientas.Telefonas);
			args.Add("?email", klientas.Epastas);
		});
	}

	public static void Update(Klientas klientas)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}klientai`
			SET
				vardas=?vardas,
				pavarde=?pavarde,
				gimimo_data=?gimdata,
				telefonas=?tel,
				epastas=?email
			WHERE
				asmens_kodas=?asmkod";

		Sql.Update(query, args => {
			args.Add("?asmkod", klientas.AsmensKodas);
			args.Add("?vardas", klientas.Vardas);
			args.Add("?pavarde", klientas.Pavarde);
			args.Add("?gimdata", klientas.GimimoData);
			args.Add("?tel", klientas.Telefonas);
			args.Add("?email", klientas.Epastas);
		});
	}

	public static void Delete(string id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}klientai` WHERE asmens_kodas=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}
}
