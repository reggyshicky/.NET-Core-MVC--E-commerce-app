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
using Bulky.DataAccess.Migrations;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ApplicationDbContext _db;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            
            return View(objCompanyList);
        }
        //This is an HTTP GET request handler.It returns a view for creating a new company.In other words, when a user navigates to a URL or 
        //clicks a link that maps to this action, they will see a form or page for creating a new company.
        public IActionResult Upsert(int? id)
        {          
            if (id == null || id == 0)  
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company CompanyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(CompanyObj);

            }
                    
        }


        //This is an HTTP POST request handler.It is used to handle the submission of the form created in the Create GET action.When a user 
        //submits the form, the data is sent as an HTTP POST request to this action.

        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
            
            if (ModelState.IsValid)
            {
                if (CompanyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                }
                
                _unitOfWork.Save();
                TempData["success"] = "Company created succesfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(CompanyObj);
            }
           
        }

        
      

        #region APICALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]

        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new { sucess = false, message = "Errorwhile deleting" });
            }

            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}

//If the code is working when you remove the [HttpDelete] attribute from your action method, it means that the action can respond to 
//HTTP DELETE requests without explicitly specifying the attribute. In ASP.NET Core, action methods are mapped to HTTP methods based 
//on naming conventions, even if you don't use the [HttpDelete] attribute. This is known as "convention-based routing."


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
    Company? companyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
    //Company? companyFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
    //Company? companyFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
    if (companyFromDb == null)
    {
        return NotFound();
    }
    return View(companyFromDb);
}

[HttpPost]
public IActionResult Edit(Company obj)
{
    if (ModelState.IsValid)
    {
        _unitOfWork.Company.Update(obj);
        _unitOfWork.Save();
        TempData["success"] = "Company updated succesfully";
        return RedirectToAction("Index");
    }
    return View();
}
*/