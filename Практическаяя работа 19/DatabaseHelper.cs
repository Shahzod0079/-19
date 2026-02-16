using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using Практическая_19.Modell;

namespace Практическая_19
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        }

        // КИНОТЕАТРЫ
        public List<Kinoteatr> GetKinoteatrs()
        {
            List<Kinoteatr> list = new List<Kinoteatr>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
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

        public bool AddKinoteatr(Kinoteatr k)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
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

        public bool UpdateKinoteatr(Kinoteatr k)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
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

        public bool DeleteKinoteatr(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Kinoteatrs WHERE Id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // АФИША
        public List<Afisha> GetAfishas()
        {
            List<Afisha> list = new List<Afisha>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
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

        public bool AddAfisha(Afisha a)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
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

        public bool UpdateAfisha(Afisha a)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
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

        public bool DeleteAfisha(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
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