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

        public ActionResult Registration()
        {
            return View();
        }
        
        //create 
        public ActionResult AddNewUser(User newUser)
        {
            AmazonesEntities1 ORM = new AmazonesEntities1();

            User foundinDB = ORM.User.Find(newUser.LastName);

            if (foundinDB != User)
            {
                ORM.User.Add(newUser);
                ORM.SaveChanges();
                ViewBag.Result = newUser.LastName;

                return View("Result");
            }

            else if(foundinDB == User)
            {
               // ViewBag.Error = newUser.LastName;
                return View("Error");
            }
            else
            {
                return View("Error");
            }
        }

        //Search
        public ActionResult SearchUser()
        {
            return View("Result");
        }

        //delete
        public ActionResult DeleteItem(string Name)
        {
            AmazonesEntities1 ORM = new AmazonesEntities1();

            Item founded = ORM.Item.Find(Name);

            if (founded != null)
            {
                ORM.Item.Remove(founded);
                ORM.SaveChanges();
                ViewBag.Message = founded.Name;
                return RedirectToAction("DeletePage");
            }
            else
            {
                return View("Error");
            }
        }

        //read
        public ActionResult ShowUSerDEtails()
        {

            return View("Result");
        }

        //update
        public ActionResult UpdateUser()
        {

            return View("Result");
        }



    }
}