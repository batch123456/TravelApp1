using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace travelapp1.Controllers
{
    public class UserController : Controller
    {
        Operations cd = new Operations();
        // GET: User
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(FormCollection c)
        {
            string s = c["Email"].ToString();
            string k = c["Password"].ToString();
            bool k1 = false;
            foreach (var item in cd.GetUser())
            {
                if (item.Email == s && item.Password == k)
                {
                    TempData["user1"] = item;
                    Session["u1"] = item;
                    k1 = true;
                }
            }
            if (k1)
            {
                return RedirectToAction("Dashboard");

            }
            else
            {
                ViewBag.Message = "Invalid Credentials..Try Again";
                return View();
            }


        }
        public ActionResult Profile()
        {
            User s = (User)TempData["user1"];
            TempData["user1"] = s;
            return View(s);
        }
        [HttpPost]
        public ActionResult Profile(User s)
        {
            try
            {
                string k = "~/Images/";
                s.Photo = k + s.Photo;
                cd.PutUser(s.UserId, s);
                return RedirectToAction("Dashboard");
            }
            catch
            {
                return View();
            }


        }
        public ActionResult MyAlbums()
        {
            List<TravelDetail> k = cd.GetDetails();
            return View(k);

        }
        public ActionResult VisitingPlaces(string Location)
        {
            List<PlacesToVisit> k = cd.GetPlaces();
            
            User s = (User)TempData["user1"];
            TempData["User1"] = s;
         
            k = k.Where(x => (x.UserId == s.UserId || x.Location == Location)).ToList();
            return View(k);

        }
        public ActionResult AddPlace()
        {
            User s = (User)TempData["user1"];
            TempData["user1"] = s;
            
            PlacesToVisit place = new PlacesToVisit();
          
            place.UserId = s.UserId;


            return View(place);
        }
        [HttpPost]
        public ActionResult AddPlace(PlacesToVisit v)
        {
            try
            {
                string k = v.Location;
                v.PlaceImage = "~/Images/" + v.PlaceImage;
                cd.PostPlace(v);
                return RedirectToAction("VisitingPlaces",new { Location = k });

            }
            catch
            {
                return View();

            }


        }
        public ActionResult AddAlbum()
        {
            User s = (User)TempData["user1"];
            TempData["User1"] = s;
            TravelDetail k = new TravelDetail();
            k.UserId = s.UserId;
            return View(k);

        }
        [HttpPost]
        public ActionResult AddAlbum(TravelDetail k)
        {
            try
            {
                cd.PostDetails(k);
                return RedirectToAction("MyAlbums");

            }
            catch
            {
                return View();
            }
        }
    }
}