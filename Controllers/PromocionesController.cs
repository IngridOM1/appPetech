using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using appPetech.Models;
using appPetech.Data;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace appPetech.Controllers

public class PromocionesController : Controller
{
    private readonly ILogger<PromocionesController> _logger;

    public PromocionesController(ILogger<PromocionesController> logger)
    {
        _logger = logger;
    }
    public IActionResult Index()
    {
        return View();
    }

      public IActionResult Promos()
    {
        return View();
    }

     public IActionResult Superofertas()
    {
        return View();
    }

     public IActionResult Liquidaciones()
    {
        return View();
    }


    }
    
    
