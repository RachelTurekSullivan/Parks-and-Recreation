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
    public class JSParkDataController : Controller
    {
        //private APIService _apiService = new APIService();
        private CacheService _jsCacheService;

        public JSParkDataController(CacheService cacheService){
            _jsCacheService = cacheService;
        }
       
        [Route("JSParkData")]
        public async Task<IActionResult> ParkList(string search)
        {
            if (search == null)
            {
                var parkList = await _jsCacheService.GetParkData();
                return View("JSParkList");
            }
            else
            {
                var unfilteredParks = await _jsCacheService.GetParkData();
                var parkList = new List<ParkDataModel>();


                foreach (var park in unfilteredParks)
                {
                    if (park.Description.Contains(search) || park.Parkname.Contains(search))
                    {
                        parkList.Add(park);
                    }
                }

                return View("JSParkList");
            }
        }

        [Route("ParkJSON")]
        public async Task<JsonResult> ParkJSON(string search)
        {
            if (search == null)
            {
                var parkList = await _jsCacheService.GetParkData();
                return Json(parkList);
            }
            else
            {
                var unfilteredParks = await _jsCacheService.GetParkData();
                var parkList = new List<ParkDataModel>();


                foreach (var park in unfilteredParks)
                {
                    if (park.Description.Contains(search) || park.Parkname.Contains(search))
                    {
                        parkList.Add(park);
                    }
                }

                return Json(parkList);
            }
        }



    }
}
