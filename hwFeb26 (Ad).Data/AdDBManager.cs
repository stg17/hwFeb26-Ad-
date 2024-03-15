using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hwFeb26__Ad_.Data
{
    public class AdDBManager
    {
        private readonly string _connectionString;
        public AdDBManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddAd(Ad ad)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Ads
                                    VALUES (@name ,@number, @desc, @date)
                                    SELECT SCOPE_IDENTITY()";
            
                command.Parameters.AddWithValue("@name", ad.Name == null ? "" : ad.Name);
           
            command.Parameters.AddWithValue("@number", ad.PhoneNumber);
            command.Parameters.AddWithValue("@desc", ad.Description);
            command.Parameters.AddWithValue("@date", DateTime.Today);
            connection.Open();
            ad.Id = (int)(decimal)command.ExecuteScalar();
        }

        public List<Ad> GetAds()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Ads ORDER BY DateCreated Desc";
            connection.Open();
            var ads = new List<Ad>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                ads.Add(new Ad()
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    Description = (string)reader["Description"],
                    PhoneNumber = (int)reader["PhoneNumber"],
                    DateCreated = (DateTime)reader["DateCreated"]
                }) ;
            }
            return ads;
        }

        public void Delete(int Id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Ads WHERE @Id = Id";
            command.Parameters.AddWithValue("@id", Id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

}
