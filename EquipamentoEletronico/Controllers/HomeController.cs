﻿using Microsoft.AspNetCore.Mvc;

namespace EquipamentoEletronico.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
