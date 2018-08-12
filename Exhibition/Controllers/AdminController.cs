using Exhibition.Models;
using FastMember;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exhibition.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetClients()
        {

          List<User> users =  ModelService.getUser().FindAll().ToList();
            return View(users);
        }

        public FileStreamResult ToExcel()
        {
            List<User> users = ModelService.getUser().FindAll().ToList();
            DataTable table = new DataTable();

            using (var reader = ObjectReader.Create(users))
            {
                table.Load(reader);
            }

           table.Columns.Remove("City");
           table.Columns.Remove("Country");

            table.Columns.Add("City", Type.GetType("System.String"));
            table.Columns.Add("Country", Type.GetType("System.String"));
            List<City> cities = ModelService.getCities().FindAll().ToList();
            List<Country> countries = ModelService.getCountries().FindAll().ToList();

            foreach (DataRow item in table.Rows)
            {

                item["City"] = cities.FirstOrDefault(f => f.Id == (int)item["CityId"]).Name;
                item["Country"] = countries.FirstOrDefault(f => f.Id == (int)item["CountryId"]).Name;
            }


            table.Columns.Remove("CityId");
            table.Columns.Remove("CountryId");
            //Response.Clear();
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("Logs.xlsx", System.Text.Encoding.UTF8));

            using (ExcelPackage pck = new ExcelPackage())
            { 
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Logs");
                ws.Cells["A1"].LoadFromDataTable(table, true);
                var ms = new System.IO.MemoryStream();
                FileInfo fileInfo = new FileInfo("abcde.xlsx");
                pck.SaveAs(ms);
                ms.Position = 0;
                FileStreamResult fs = new FileStreamResult(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                //ms.WriteTo(Response.OutputStream);
                return fs;
            }


        }

        public ActionResult UpdateStatus(int id = 0)
        {
            ModelService.UpdateStatus(id);
            return RedirectToAction("GetClients");
           
        }

        public ActionResult GetCountries()
        {
            List<Country> countries = ModelService.getCountries().FindAll().ToList();
            return View(countries);
        }

        public ActionResult GetCities()
        {
            List<City> cities = ModelService.getCities().FindAll().ToList();
            return View(cities);
        }

        public ActionResult AddCountry(Country country, int id= 0)
        {
            if(ModelState.IsValid)
            {           
                    ModelService.AddCountry(country,id);
                    return RedirectToAction("GetCountries");
            }
            if (id != 0)
            {
                Country c = ModelService.getCountries().FindById(id);
                return View(c);
            }
            return View();
        }

        public ActionResult AddCity(City city, int id = 0)
        {
            List<Country> countries = ModelService.getCountries().FindAll().ToList();
            ViewBag.CountryList = countries.Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() }).ToList();


            if (ModelState.IsValid)
            {
                city.Country = countries.FirstOrDefault(f => f.Id == city.CountryId);
                ModelService.AddCity(city, id);
                return RedirectToAction("GetCities");
            }
            if (id != 0)
            {
                City c = ModelService.getCities().FindById(id);
                return View(c);
            }
            return View();
        }

        public ActionResult DeleteCountry(int id = 0)
        {
            ModelService.DeleteCountry(id);
            return RedirectToAction("GetCountries");
        }

        public ActionResult DeleteCity(int id = 0)
        {
            ModelService.DeleteCity(id);
            return RedirectToAction("GetCities");
        }

    }
}