using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDlisious.Models;
// ***********************************
using Microsoft.EntityFrameworkCore;
// ***********************************

namespace CRUDlisious.Controllers
{
    public class HomeController : Controller
    {
        // This is how we communicate with the database //
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }


        // ***********  Main index Page, shows all dishes ***************** // 
        [HttpGet("")]
        public IActionResult Index()
        {
            Dish[] AllDishes = dbContext.Dishes.ToArray();
            return View(AllDishes);
        }


        // ***********  Display add dish form ************************** // 
        [HttpGet("Add")]
        public IActionResult Add_Page()
        {
            return View("Add_Page");
        }


        // ***********  Create new dish POST ************************ // 
        [HttpPost("/createdish")]
        public IActionResult CreateDish(Dish FromForm)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(FromForm);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return Add_Page();
            }
        }


        // ***********  Submit Edited Dish and redirects *************************** // 
        [HttpPost("/submitedit/{DishId}")]
        public IActionResult SubmitEdit(int DishId, Dish FromForm)
        {
            if(ModelState.IsValid)
            {
                FromForm.DishId = DishId;
                dbContext.Update(FromForm);
                dbContext.Entry(FromForm).Property("CreatedAt").IsModified=false;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return EditPage(DishId);
            }
        }


        // ***********  Edit 1 dish Page *************************** // 
        [HttpGet("Edit/{DishId}")]
        public ViewResult EditPage(int DishId)
        {
            Dish ToView = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == DishId);
            return View("EditPage", ToView);
        }


        // **************  Detail View 1 Dish Page ********************* // 
        [HttpGet("View/{DishId}")]
        public ViewResult ViewPage(int DishId)
        {
            Dish ToView = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == DishId);
            return View("ViewPage", ToView);
        }

        [HttpGet("Remove/{DishId}")]
        public RedirectToActionResult Remove(int DishId)
        {
            Dish ToDelete = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == DishId);
            dbContext.Remove(ToDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
