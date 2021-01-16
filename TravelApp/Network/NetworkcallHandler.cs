using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models;

namespace TravelApp.Network
{
    public static class NetworkcallHandler
    {
        private const string BASE_URL = "http://localhost:51758/api/";

        public static async Task<User> GetUserDataAsync( string username, string password)
        {
            var uri = new System.Uri(BASE_URL + "User/getByName/" + username + "/" + password);

            using (var httpClient = new Windows.Web.Http.HttpClient())
            {
                // Always catch network exceptions for async methods
                try
                {
                    var result = await httpClient.GetStringAsync(uri);
                    User user = JsonConvert.DeserializeObject<User>(result);
                    return user;

                }
                catch (Exception ex)
                {              
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }


    }
}
