using System.Collections.Generic;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using MySql.Data.MySqlClient;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

public class OrdersRepo
{
    private static string conn =
        "server=localhost;database=boltfood;uid=root;pwd=;";

    // GET ALL ORDERS
    public static List<OrderViewModel> GetUserOrders(int userId)
    {
        var list = new List<OrderViewModel>();

        using var connObj = new MySqlConnection(conn);
        connObj.Open();

        // First, get the user's VartotojoTipas
        string userTypeSQL = "SELECT COALESCE(VartotojoTipas, 1) AS VartotojoTipas FROM vartotojas WHERE id = @userId";
        using var userTypeCmd = new MySqlCommand(userTypeSQL, connObj);
        userTypeCmd.Parameters.AddWithValue("@userId", userId);
        
        int vartotojoTipas = 1;
        using var userTypeReader = userTypeCmd.ExecuteReader();
        if (userTypeReader.Read())
        {
            vartotojoTipas = (int)userTypeReader["VartotojoTipas"];
        }
        userTypeReader.Close();

        // Build query based on VartotojoTipas
        string sql;
        if (vartotojoTipas == 1)
        {
            // Type 1: Customer - show orders where they are the customer
            sql = @"
                SELECT u.id, u.Data, u.PristatymoAdresas, u.Kaina, s.name AS Statusas, 1 AS VartotojoTipas
                FROM uzsakymas u
                JOIN statusas s ON s.id = u.Statusas
                WHERE u.fk_Vartotojasid = @userId
                ORDER BY u.Data DESC";
        }
        else
        {
            // Type 2: Courier - show orders where they are the courier
            sql = @"
                SELECT u.id, u.Data, u.PristatymoAdresas, u.Kaina, s.name AS Statusas, 2 AS VartotojoTipas
                FROM uzsakymas u
                JOIN statusas s ON s.id = u.Statusas
                WHERE u.fk_Kurjerisid = @userId
                ORDER BY u.Data DESC";
        }

        using var cmd = new MySqlCommand(sql, connObj);
        cmd.Parameters.AddWithValue("@userId", userId);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new OrderViewModel
            {
                Id = (int)reader["id"],
                Data = reader.GetDateTime("Data"),
                Adresas = reader["PristatymoAdresas"].ToString(),
                Statusas = reader["Statusas"].ToString(),
                Kaina = (float)reader["Kaina"],
                VartotojoTipas = (int)reader["VartotojoTipas"]
            });
        }

        return list;
    }

    // GET SINGLE ORDER WITH ITEMS
    public static OrderDetailsViewModel GetOrderDetails(int orderId, int userId)
    {
        OrderDetailsViewModel model = null;

        using var connObj = new MySqlConnection(conn);
        connObj.Open();

        // 1. order header
        string sqlOrder = @"
            SELECT id, Kaina
            FROM uzsakymas
            WHERE id=@id AND fk_Vartotojasid=@userId";

        using (var cmd = new MySqlCommand(sqlOrder, connObj))
        {
            cmd.Parameters.AddWithValue("@id", orderId);
            cmd.Parameters.AddWithValue("@userId", userId);

            using var reader = cmd.ExecuteReader();

            if (!reader.Read())
                return null;

            model = new OrderDetailsViewModel
            {
                Id = (int)reader["id"],
                Kaina = (float)reader["Kaina"],
                Items = new List<OrderItemViewModel>()
            };
        }

        // 2. order items
        string sqlItems = @"
            SELECT PatiekaloPavadinimas, PasirinkimuAprasymas, Kaina, Kiekis
            FROM uzsakymoirasas
            WHERE fk_Uzsakymasid=@id";

        using (var cmd = new MySqlCommand(sqlItems, connObj))
        {
            cmd.Parameters.AddWithValue("@id", orderId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                model.Items.Add(new OrderItemViewModel
                {
                    PatiekaloPavadinimas = reader["PatiekaloPavadinimas"].ToString(),
                    PasirinkimuAprasymas = reader["PasirinkimuAprasymas"].ToString(),
                    Kaina = (float)reader["Kaina"],
                    Kiekis = (int)reader["Kiekis"]
                });
            }
        }

        return model;
    }
}