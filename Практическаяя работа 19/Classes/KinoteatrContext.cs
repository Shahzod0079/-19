using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Практическая_19.Classes.Common;
using Практическая_19.Modell;

namespace Практическая_19.Classes
{
    public class KinoteatrContext
    {
        public List<Kinoteatr> GetAll()
        {
            List<Kinoteatr> list = new List<Kinoteatr>();

            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM Kinoteatrs";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Kinoteatr
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                            HallCount = reader.GetInt32("HallCount"),
                            SeatCount = reader.GetInt32("SeatCount")
                        });
                    }
                }
            }
            return list;
        }

        public bool Add(Kinoteatr k)
        {
            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Kinoteatrs (Name, HallCount, SeatCount) VALUES (@n, @h, @s)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@n", k.Name);
                cmd.Parameters.AddWithValue("@h", k.HallCount);
                cmd.Parameters.AddWithValue("@s", k.SeatCount);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(Kinoteatr k)
        {
            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                string sql = "UPDATE Kinoteatrs SET Name=@n, HallCount=@h, SeatCount=@s WHERE Id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", k.Id);
                cmd.Parameters.AddWithValue("@n", k.Name);
                cmd.Parameters.AddWithValue("@h", k.HallCount);
                cmd.Parameters.AddWithValue("@s", k.SeatCount);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM Kinoteatrs WHERE Id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}