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
        
        public async Task<int> GetParkData()
        {

            var client = new HttpClient();
            var result = await client.GetAsync(parkDataURL);
            result.EnsureSuccessStatusCode();

            var resultStr = await result.Content.ReadAsStringAsync();
            Console.WriteLine(resultStr);
            return Convert.ToInt32(resultStr);
        }

        
    }
}
