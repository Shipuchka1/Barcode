using Exhibition.Models;
using iTextSharp.text.pdf;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Exhibition.Controllers
{
    public class ExhibitionController : Controller
    {
        // GET: Exhibition
        public ActionResult Index(User user)
        {
            List<Country> countries = ModelService.getCountries().FindAll().ToList();

            ViewBag.CountryList = countries.Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() }).ToList();

            List<City> cities = ModelService.getCities().FindAll().ToList();

            user.City = cities.FirstOrDefault(f => f.Id == user.CityId);
            user.Country = countries.FirstOrDefault(f => f.Id == user.CountryId);
            if (ModelState.IsValid)
            {
                user.Status = false;
                if (ModelService.AddUser(user) && user.FullName != null)
                {
                    if (TempData.ContainsKey("User"))
                        TempData["User"] = user;
                    else
                        TempData.Add("User", user);
                    return RedirectToAction("AfterRegistration");
                }
            }
           
            

            return View();
        }

        public JsonResult City_Bind(int country_id = 0)

        {

            List<City> cities = ModelService.getCities().Find(f=>f.CountryId==country_id).ToList();
            List<SelectListItem> citylist = cities.Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() }).ToList();

            return Json(citylist, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AfterRegistration()
        {
            User us = (User)TempData["User"];
            string stb2 = "";
            Barcode128 code2 = new Barcode128();
            code2.CodeType = Barcode.CODE128_UCC;
            code2.ChecksumText = true;
            code2.GenerateChecksum = true;
            code2.StartStopText = true;
            code2.Code = stb2;

            System.Drawing.Bitmap bm2 = new System.Drawing.Bitmap(code2.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White));

            ViewBag.UserId = us.UserId;

            bm2.Save(Server.MapPath("~/Template/barcode" + us.UserId + ".gif"), System.Drawing.Imaging.ImageFormat.Gif);
            return View();
        }
    }
}