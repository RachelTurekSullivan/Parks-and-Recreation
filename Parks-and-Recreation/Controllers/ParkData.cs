using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parks_and_Recreation.Controllers
{
    public class ParkData : Controller
    {
        [Route("ParkData")]
        public IActionResult List()
        {
            return View();
        }
    }
}
