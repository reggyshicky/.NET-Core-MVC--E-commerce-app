using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;
using System;
using Microsoft.Extensions.Hosting;
using static System.Collections.Specialized.BitVector32;
using System.Reflection.Metadata;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        //This is an HTTP GET request handler.It returns a view for creating a new category.In other words, when a user navigates to a URL or 
        //clicks a link that maps to this action, they will see a form or page for creating a new category.
        public IActionResult Create()
        {
            return View();
        }


        //This is an HTTP POST request handler.It is used to handle the submission of the form created in the Create GET action.When a user 
        //submits the form, the data is sent as an HTTP POST request to this action.
        
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid) 
            {
                _db.Categories.Add(obj); //keeping track of what it has to add
                _db.SaveChanges(); //go to the db and create that category
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
    }
}
