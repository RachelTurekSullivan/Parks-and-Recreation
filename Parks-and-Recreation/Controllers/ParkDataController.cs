using Microsoft.AspNetCore.Mvc;
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
        private APIService _apiService = new APIService();
        
        [Route("ParkData")]
        public async Task<IActionResult> List()
        {
            var parkList = await _apiService.GetParkData();

            return View(parkList);
        }
    }
}
