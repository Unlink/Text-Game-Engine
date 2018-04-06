using System;
using System.Data.SqlClient;
using Hra.Properties;

namespace Hra
{
    internal class DatabaseModel
    {
        /// <summary>
        ///     Vloží skore do databazy
        /// </summary>
        /// <param name="player"></param>
        /// <param name="time"></param>
        /// <param name="score"></param>
        public void InsertScore(string player, int time, int score)
        {
            using (var con = new SqlConnection(Settings.Default.dbConnector))
            {
                con.Open();
                var command = new SqlCommand(
                    "INSERT INTO Potko (player, pc, [date], time, score) VALUES(@1, @2, @5, @3, @4)", con);
                command.Parameters.AddWithValue("@1", player);
                command.Parameters.AddWithValue("@2", Environment.MachineName);
                command.Parameters.AddWithValue("@3", time);
                command.Parameters.AddWithValue("@4", score);
                command.Parameters.AddWithValue("@5", DateTime.Now);
                command.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        ///     Vráti top hračov
        /// </summary>
        /// <returns></returns>
        public void GetTop()
        {
            using (var con = new SqlConnection(Settings.Default.dbConnector))
            {
                con.Open();
                using (var command = new SqlCommand("SELECT TOP 10 * FROM Potko ORDER BY score desc", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int pocitadlo = 1;
                    Console.WriteLine("{0,-7} {1,-20} {2,-15} {3,-5} {4}", "Poradie", "Meno", "Pc", "Skóre", "Dátum");
                    while (reader.Read())
                    {
                        Console.WriteLine("{0,6}. {1,-20} {2,-15} {3,-5} {4}", pocitadlo++,
                            reader.GetString(1), reader.GetString(2), reader.GetInt32(5), reader.GetDateTime(3));
                    }
                }
            }
        }
    }
}