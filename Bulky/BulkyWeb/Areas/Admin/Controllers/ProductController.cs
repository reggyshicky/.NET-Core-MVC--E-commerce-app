using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;
using System;
using Microsoft.Extensions.Hosting;
using static System.Collections.Specialized.BitVector32;
using System.Reflection.Metadata;
using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAccess.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Linq;
using Bulky.Models.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            
            return View(objProductList);
        }
        //This is an HTTP GET request handler.It returns a view for creating a new product.In other words, when a user navigates to a URL or 
        //clicks a link that maps to this action, they will see a form or page for creating a new product.
        public IActionResult Upsert(int? id)
        {           
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.
                GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()

            };
            if (id == null || id == 0)
                //create
            {
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);

            }
                    
        }


        //This is an HTTP POST request handler.It is used to handle the submission of the form created in the Create GET action.When a user 
        //submits the form, the data is sent as an HTTP POST request to this action.

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath; //this WebRootPath will give us the path to the wwwRootPath
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    
                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);

                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                
                _unitOfWork.Save();
                TempData["success"] = "Product created succesfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.
                GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
           
        }

        
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }



        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
//IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.
//    GetAll().Select(u => new SelectListItem
//    {
//        Text = u.Name,
//        Value = u.Id.ToString()
//    });
// ViewBag.CategoryList = CategoryList;
//ViewData["CategoryList"] = CategoryList;

//u.Name is used as the Text, which is what is displayed in the dropdown list.This is the user - friendly name or label 
//for the category.
//u.Id.ToString() is used as the Value, which is not displayed to the user but is used to uniquely identify the category 
//when the user selects an option. This is typically a numeric identifier or a unique key for the category.
//Value is the data that gets sent to the server when the user selects an item.
//Get method
/*
public IActionResult Edit(int? id)
{
    if (id == null || id == 0)
    {
        return NotFound();
    }
    Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
    //Product? productFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
    //Product? productFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
    if (productFromDb == null)
    {
        return NotFound();
    }
    return View(productFromDb);
}

[HttpPost]
public IActionResult Edit(Product obj)
{
    if (ModelState.IsValid)
    {
        _unitOfWork.Product.Update(obj);
        _unitOfWork.Save();
        TempData["success"] = "Product updated succesfully";
        return RedirectToAction("Index");
    }
    return View();
}
*/