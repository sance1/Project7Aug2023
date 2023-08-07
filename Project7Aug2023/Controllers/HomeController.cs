﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using Project7Aug2023.Models;

namespace Project7Aug2023.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    
}

