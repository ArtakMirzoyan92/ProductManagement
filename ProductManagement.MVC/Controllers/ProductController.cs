﻿using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.MVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
