using DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace travelapp1.Controllers
{
    public class AdminController : Controller
    {
        Operations cd = new Operations();
        // GET: Admin
        public ActionResult Index()
        {

            List<User> l = new List<User>();
            l = cd.GetUser();
            return View(l);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection c)
        {
            string s = c["AdminEmail"].ToString();
            string k = c["AdminPassword"].ToString();
            bool k1 = false;
            foreach (var item in cd.GetAdmin())
            {
                if (item.AdminEmail == s && item.AdminPassword == k)
                {
                    TempData["User"] = item;
                    Session["u1"] = item;
                    k1 = true;
                }
            }
            if (k1)
            {

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Invalid Credentials..Try Again";
                return View();
            }


        }
        public ActionResult Edit(int id)
        {
            User k = cd.GetUser(id);
            return View(k);
        }
        [HttpPost]
        public ActionResult Edit(int id, User k)
        {
            try
            {
                cd.PutUser(id, k);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            User k = cd.GetUser(id);
            return View(k);
        }
        public ActionResult Delete(int id)
        {
            User k = cd.GetUser(id);
            return View(k);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection c)
        {
            try
            {
                cd.DeleteUser(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Create(User k)
        {
            try
            {
                k.Photo="~/Images/"+k.Photo;
                cd.PostUser(k);
                
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
    }
}