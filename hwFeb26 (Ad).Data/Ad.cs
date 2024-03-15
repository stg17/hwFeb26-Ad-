using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace hwFeb26__Ad_.Data
{
    public class Ad
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool MyAd { get; set; }
    }

    public static class Extension
    {
        public static T GetOrNull<T>(this SqlDataReader reader, string columnName)
        {
            var value = reader[columnName];
            if (value == DBNull.Value)
            {
                return default(T);
            }

            return (T)value;
        }
    }
}
