using Dependency_Injection_Service_Lifetime.Models;
using Dependency_Injection_Service_Lifetime.services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace Dependency_Injection_Service_Lifetime.Controllers
{
    public class HomeController : Controller
    {
        private readonly IScopedGuidService _scoped1;
        private readonly IScopedGuidService _scoped2;

        private readonly ITransientGuidService _transient1;
        private readonly ITransientGuidService _transient2;

        private readonly ISingletonGuidService _singleton1;
        private readonly ISingletonGuidService _singleton2;

        private readonly ILogger<HomeController> _logger;

        public HomeController(IScopedGuidService scoped1,
            IScopedGuidService scoped2,
            ISingletonGuidService singleton1,
            ISingletonGuidService singleton2,
            ITransientGuidService Transient1,
            ITransientGuidService Transient2)

        {
            _singleton1 = singleton1;
            _singleton2 = singleton2;
            _transient1 = Transient1;
            _transient2 = Transient2;
            _scoped1 = scoped1;
            _scoped2 = scoped2;
        }

        public IActionResult Index()
        {
            StringBuilder messages = new StringBuilder();
            messages.Append($"Transient 1:{_transient1.GetGuid()}\n");
            messages.Append($"Transient 2:{_transient2.GetGuid()}\n\n\n");

            messages.Append($"Scoped 1:{_scoped1.GetGuid()}\n");
            messages.Append($"Transient 2:{_scoped2.GetGuid()}\n\n\n");

            messages.Append($"Singleton 1:{_singleton1.GetGuid()}\n");
            messages.Append($"Singleton 2:{_singleton2.GetGuid()}\n\n\n");

        //    In C#, StringBuilder is a class provided by the .NET Framework that is used for efficient string manipulation. 
        //        It is part of the System.Text namespace and is particularly useful when you need to concatenate or modify 
        //strings in situations where the number of operations is significant. The primary advantage of StringBuilder over 
        //regular string concatenation using the + operator or string.Concat is that it minimizes memory overhead and performs 
        //    faster in such scenarios.


            return Ok(messages.ToString());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}