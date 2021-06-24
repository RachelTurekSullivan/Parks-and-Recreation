using Parks_and_Recreation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;

namespace Parks_and_Recreation.Services
{
    public class CacheService
    {
        private readonly APIService _apiService = new();
        private List<ParkDataModel> ParkData = new();
        private readonly IMemoryCache _cache;
        private string CacheKey;
        private DateTime _dateTime;


        public CacheService()
        {
            
            _dateTime = DateTime.Now;
        }

        public async Task<List<ParkDataModel>> GetParkData()
        {
            //if cache has expired? 
            if (DateTime.Now.Subtract(_dateTime).TotalMinutes > 10 || this.ParkData.Count==0)
            {
                RefreshParkData();
                _dateTime = DateTime.Now;
            }
            
            return this.ParkData; 
        }

        
        private async void RefreshParkData()
        {
            var result = _apiService.GetParkData().Result;
            this.ParkData = result;
        }

      

    }
}
