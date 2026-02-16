using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Практическая_19.Classes.Common;
using Практическая_19.Modell;

namespace Практическая_19.Classes
{
    public class AfishaContext
    {
        public List<Afisha> GetAll()
        {
            List<Afisha> list = new List<Afisha>();

            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT a.*, k.Name as KinoteatrName 
                              FROM Afishas a
                              JOIN Kinoteatrs k ON a.KinoteatrId = k.Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Afisha
                        {
                            Id = reader.GetInt32("Id"),
                            KinoteatrId = reader.GetInt32("KinoteatrId"),
                            KinoteatrName = reader.GetString("KinoteatrName"),
                            FilmName = reader.GetString("FilmName"),
                            SessionTime = reader.GetDateTime("SessionTime"),
                            TicketPrice = reader.GetDecimal("TicketPrice")
                        });
                    }
                }
            }
            return list;
        }

        public bool Add(Afisha a)
        {
            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Afishas (KinoteatrId, FilmName, SessionTime, TicketPrice) VALUES (@kid, @f, @t, @p)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kid", a.KinoteatrId);
                cmd.Parameters.AddWithValue("@f", a.FilmName);
                cmd.Parameters.AddWithValue("@t", a.SessionTime);
                cmd.Parameters.AddWithValue("@p", a.TicketPrice);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(Afisha a)
        {
            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                string sql = "UPDATE Afishas SET KinoteatrId=@kid, FilmName=@f, SessionTime=@t, TicketPrice=@p WHERE Id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", a.Id);
                cmd.Parameters.AddWithValue("@kid", a.KinoteatrId);
                cmd.Parameters.AddWithValue("@f", a.FilmName);
                cmd.Parameters.AddWithValue("@t", a.SessionTime);
                cmd.Parameters.AddWithValue("@p", a.TicketPrice);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM Afishas WHERE Id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}