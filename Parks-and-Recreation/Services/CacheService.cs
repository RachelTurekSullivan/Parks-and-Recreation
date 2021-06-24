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
        private readonly IMemoryCache _cache;


        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<List<ParkDataModel>> GetParkData()
        {
            List<ParkDataModel> data = null;
            //if cache has expired? 
            if (!_cache.TryGetValue("ParkData", out data))
            {
                var parkData = await _apiService.GetParkData();
                _cache.Set("ParkData", parkData, TimeSpan.FromMinutes(5));
                var result = _apiService.GetParkData().Result;
            }

            return data;
        }
      
    }
}
