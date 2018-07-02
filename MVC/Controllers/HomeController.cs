using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileProcessing;
using FileProcessing.helpers;
using FileProcessing.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        //Dependency Injection may be valid here
        private TikaServiceHandler _tikaServiceHandler = new TikaServiceHandler();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessFile(IFormFile file)
        {
            string content;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                content = _tikaServiceHandler.ReadPdfFile(memoryStream.ToArray());
            }

            var shoppingList = new ShoppingList(content, RegexHelper.getMenuRegExp());

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
