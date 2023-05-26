using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using appPetech.Integration;
using appPetech.Models;

namespace appPetech.Controllers
{
    [Route("[controller]")]
    public class DogBreedController : Controller
    {
        private readonly ILogger<DogBreedController> _logger;
        private readonly DogBreedIntegration _dogBreed;

        public DogBreedController(ILogger<DogBreedController> logger, DogBreedIntegration dogBreed)
        {
            _logger = logger;
            _dogBreed = dogBreed;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(string search)
        {
            var dataPerros = await FetchApiData();

            if (!string.IsNullOrEmpty(search))
            {
                dataPerros = dataPerros.Where(p => p.Breed.ToLower().Contains(search.ToLower())).ToList();
            }

            ViewData["Search"] = search;
            return View("~/Views/DogBreed/Index.cshtml", dataPerros);
        }

        [HttpGet("FetchApi")]
        public async Task<List<RazaPerros>> FetchApiData()
        {
            var apiData = await _dogBreed.GetDogBreedsAsync();
            return apiData;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("error")]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
