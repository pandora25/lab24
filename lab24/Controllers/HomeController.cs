using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lab24.Models;

namespace lab24.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //read
        public ActionResult ShowUSerDEtails()
        {

            return View("Result");
        }
        //Search
        public ActionResult SearchUser()
        {
            return View("Result");
        }

        public ActionResult Registration()
        {
            return View();
        }
        //create user
        public ActionResult AddNewUser(User newUser)
        {
            AmazonesEntities1 ORM = new AmazonesEntities1();
            User foundinDB = ORM.Users.Find(newUser.LastName);
            if (foundinDB == null )
            {
                ORM.Users.Add(newUser);
                ORM.SaveChanges();
                ViewBag.Result =newUser.LastName;
                return View("Result");
            }
            else
            { 
               ViewBag.Error = newUser.LastName;
                return View("Error");
            }
        }
        //adding items
        public ActionResult AddItem(Item AddingItems)
        {
            AmazonesEntities1 ORM = new AmazonesEntities1();

            if (ModelState.IsValid)
            {
                ORM.Items.Add(AddingItems);
                ORM.SaveChanges();
                ViewBag.Result = ORM.Items.ToList();
                ViewBag.Message = $"{AddingItems.Name} has been added";
                return RedirectToAction("Swither");
            }
            return View();
         
        }
        //pass the user to next Session
        public ActionResult Swither()
        {
            return RedirectToAction("ListItems");
        }
  
        public ActionResult ListItems(Item AllItems)
        {
            AmazonesEntities1 ORM = new AmazonesEntities1();
           List<Item> ItemsinfoundedinDB =  ORM.Items.ToList();
            ViewBag.Message = ItemsinfoundedinDB;
            return View();
        }
     
        //delete
        public ActionResult DeleteItem(string Name)
        {
            AmazonesEntities1 ORM = new AmazonesEntities1();

            Item founded = ORM.Items.Find(Name);

                ORM.Items.Remove(founded);
                ORM.SaveChanges();
                return RedirectToAction("ListItems");
        }

        public ActionResult UpdateItem(string UpdateItems)
        {
            AmazonesEntities1 ORM = new AmazonesEntities1();

            Item item = ORM.Items.Find(UpdateItems);

            return View(item);
        }
 
        //update
        public ActionResult SaveUpdatedItem(Item UpdateItems)
        {
            //1.creat orm
            AmazonesEntities1 ORM = new AmazonesEntities1();
            //2.find the customer
            Item oldItemRecord = ORM.Items.Find(UpdateItems.Name);
            if (oldItemRecord != null && ModelState.IsValid)
            {
                //3.update the exiting customer
                // oldCustomerRecord.CustomerID.Replace
                //oldItemRecord.Name = UpdateItems.Name;
                oldItemRecord.Price = UpdateItems.Price;
                oldItemRecord.Quantity = UpdateItems.Quantity;
                oldItemRecord.Origin = UpdateItems.Origin;
                oldItemRecord.OrderNumber = UpdateItems.OrderNumber;

                ORM.Entry(oldItemRecord).State = System.Data.Entity.EntityState.Modified;
                ORM.SaveChanges();
                return RedirectToAction("ListItems");
            }
            else
            {
                ViewBag.ErrorMessage = "Oops! Something Went Wrong";
                return View("Error");
            }
        }
    }
}