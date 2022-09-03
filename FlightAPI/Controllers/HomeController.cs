﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightAPI.Controllers
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



        FlightAppDBEntities ent = new FlightAppDBEntities();
        public JsonResult registerUsers(User user)
        {
            ent.Configuration.ProxyCreationEnabled = false;

            try
            {
                ent.Users.Add(user);
                ent.SaveChanges();
                return Json("User added Successfully", JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json("Error: " + e.Message, JsonRequestBehavior.AllowGet);

            }
        }
    }
}