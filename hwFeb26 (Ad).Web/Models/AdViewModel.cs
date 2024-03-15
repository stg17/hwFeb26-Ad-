using hwFeb26__Ad_.Data;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Text.Json;

namespace hwFeb26__Ad_.Web.Models
{
    public class AdViewModel
    {
        public List<Ad> Ads { get; set;}
    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }

        
    }
}
