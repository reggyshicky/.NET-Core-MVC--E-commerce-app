using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;
using System;
using Microsoft.Extensions.Hosting;
using static System.Collections.Specialized.BitVector32;
using System.Reflection.Metadata;
using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        //private readonly ApplicationDbContext _db;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
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
               _categoryRepo.Add(obj); 
               _categoryRepo.Save(); 
                TempData["success"] = "Category created succesfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        //Get method
        public IActionResult Edit(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u=>u.Id==id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category updated succesfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        
        
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _categoryRepo.Get(u => u.Id == id);
            if (obj==null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
