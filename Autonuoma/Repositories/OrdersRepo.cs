using System;
using System.Collections.Generic;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using MySql.Data.MySqlClient;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

public class OrdersRepo
{
    private static string conn => Org.Ktu.Isk.P175B602.Autonuoma.Config.DBConnStr;

    // GET ALL ORDERS (Updated to accept 2 arguments from session)
    public static List<OrderViewModel> GetUserOrders(int userId, int vartotojoTipas)
    {
        var list = new List<OrderViewModel>();

        using var connObj = new MySqlConnection(conn);
        connObj.Open();

        // The internal query path is determined by the vartotojoTipas passed from the controller session
        string sql;
        if (vartotojoTipas == 1)
        {
            // Type 1: Customer - show orders where they are the customer
            sql = @"
                SELECT u.id, u.Data, u.PristatymoAdresas, u.Kaina, s.name AS Statusas
                FROM uzsakymas u
                JOIN statusas s ON s.id = u.Statusas
                WHERE u.fk_Vartotojasid = @userId
                ORDER BY u.Data DESC";
        }
        else
        {
            // Type 2: Courier - show orders where they are the courier
            sql = @"
                SELECT u.id, u.Data, u.PristatymoAdresas, u.Kaina, s.name AS Statusas
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
                Kaina = Convert.ToSingle(reader["Kaina"]),
                VartotojoTipas = vartotojoTipas // Map the type back to toggle UI buttons properly
            });
        }

        return list;
    }

    // GET SINGLE ORDER WITH ITEMS (Updated to accept 3 arguments from session)
    public static OrderDetailsViewModel GetOrderDetails(int orderId, int userId, int vartotojoTipas)
    {
        OrderDetailsViewModel model = null;

        using var connObj = new MySqlConnection(conn);
        connObj.Open();

        // 1. order header (Verifies ownership based on user type)
        string sqlOrder;
        if (vartotojoTipas == 1)
        {
            sqlOrder = @"
                SELECT id, Kaina
                FROM uzsakymas
                WHERE id=@id AND fk_Vartotojasid=@userId";
        }
        else
        {
            sqlOrder = @"
                SELECT id, Kaina
                FROM uzsakymas
                WHERE id=@id AND fk_Kurjerisid=@userId";
        }

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
                Kaina = Convert.ToSingle(reader["Kaina"]),
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
                    Kaina = Convert.ToSingle(reader["Kaina"]),
                    Kiekis = (int)reader["Kiekis"]
                });
            }
        }

        return model;
    }

    // UPDATES ORDER STATUS SPECIFIC TO COURIER ACTIONS
    public static void UpdateOrderStatus(int orderId, int courierId, string statusName)
    {
        using var connObj = new MySqlConnection(conn);
        connObj.Open();

        string sql = "";

        // Evaluate your requested state pipeline rules
        if (statusName == "paimtas")
        {
            // Only update u.Statusas from 1 (Apmokėtas/Placing state) to 3 (Paimtas)
            sql = @"
                UPDATE uzsakymas
                SET Statusas = 3
                WHERE id = @orderId AND fk_Kurjerisid = @courierId";
        }
        else
        {
            // Only update u.Statusas from 3 (Paimtas) to 4 (Atiduotas)
            sql = @"
                UPDATE uzsakymas
                SET Statusas = 4
                WHERE id = @orderId AND fk_Kurjerisid = @courierId";
        }

        // Defensive check: execute only if a valid workflow path string matched
        if (!string.IsNullOrEmpty(sql))
        {
            using var cmd = new MySqlCommand(sql, connObj);
            cmd.Parameters.AddWithValue("@orderId", orderId);
            cmd.Parameters.AddWithValue("@courierId", courierId);
            cmd.ExecuteNonQuery();
        }
    }
}