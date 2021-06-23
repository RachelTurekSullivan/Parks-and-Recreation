using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Parks_and_Recreation.Models;
using System.Xml.Serialization;

namespace Parks_and_Recreation.Services
{
    public class APIService
    {
        private string parkDataURL = "https://seriouslyfundata.azurewebsites.net/api/parks";

        public async Task<List<ParkDataModel>> GetParkData()
        {

            var client = new HttpClient();
            var response = await client.GetAsync(parkDataURL);
            response.EnsureSuccessStatusCode();

            string jsonString = await response.Content.ReadAsStringAsync();
            //var parkData = JsonConvert.DeserializeObject<ParkListModel>(jsonString);
            var parkData = JsonSerializer.Deserialize< List<ParkDataModel>>(jsonString);

            
            Console.WriteLine(parkData);
            return parkData;
        }
    }
}

      