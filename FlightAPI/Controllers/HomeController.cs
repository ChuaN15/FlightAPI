using FlightAPI.Models;
using System;
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

        public JsonResult Login(User user)
        {
            ent.Configuration.ProxyCreationEnabled = false;

            try
            {
                //Select * from User where Email = $Email AND Password = $Password
                var whichUser = ent.Users.Where(x => x.Email == user.Email &&
                x.Password == user.Password).FirstOrDefault();
                return Json(whichUser, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json("Error: " + e.Message, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult GetAirportList()
        {
            ent.Configuration.ProxyCreationEnabled = false;

            try
            {
                //Select * From Airport
                var allAirports = ent.Airports.ToList();
                return Json(allAirports, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e2)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetDepartureSchedules(FlightRequest request)
        {
            ent.Configuration.ProxyCreationEnabled = false;

            try
            {
                var allSchedules = ent.FlightSchedules.ToList();
                allSchedules = allSchedules.Where(x => x.DepartureAirport == request.Departure
                && x.ArrivalAirport == request.Arrival
                && x.Date == request.StartDate).ToList();

                return Json(allSchedules, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e2)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetArrivalSchedules(FlightRequest request)
        {
            ent.Configuration.ProxyCreationEnabled = false;

            try
            {
                var allSchedules = ent.FlightSchedules.ToList();
                allSchedules = allSchedules.Where(x => x.DepartureAirport == request.Arrival
                && x.ArrivalAirport == request.Departure
                && x.Date == request.EndDate).ToList();

                return Json(allSchedules, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e2)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}