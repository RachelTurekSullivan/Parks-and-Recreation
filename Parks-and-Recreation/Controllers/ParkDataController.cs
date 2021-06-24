using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Parks_and_Recreation.Models;
using Parks_and_Recreation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parks_and_Recreation.Controllers
{
    public class ParkDataController : Controller
    {
        //private APIService _apiService = new APIService();
        private CacheService _cacheService;

        public ParkDataController(CacheService cacheService){
            _cacheService = cacheService;
        }
       
        [Route("ParkData")]
        public async Task<IActionResult> ParkList(string search)
        {
            
            if (search == null)
            {
                ViewBag.Parks = await _cacheService.GetParkData();
                return View();
            }

            else
            {

                var unfilteredParks = _cacheService.GetParkData().Result;
                ViewBag.Parks = new List<ParkDataModel>();

                foreach (var park in unfilteredParks)
                {
                    if (park.Description.Contains(search) || park.Parkname.Contains(search))
                    {
                        ViewBag.Parks.Add(park);
                    }
                }

                return View();
            }
        }

    }
}
