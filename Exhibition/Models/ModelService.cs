using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exhibition.Models
{
    public class ModelService
    {

        public static bool AddCity(City city, int id = 0)
        {
            try
            {
                using (var db = new LiteDatabase(@"MyData.db"))
                {
                    LiteCollection<City> cities = db.GetCollection<City>("Cities");
                    if (id == 0)
                        cities.Insert(city);
                    else
                        cities.Update(city);
                }
                return true;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return false;
            }
        }

        public static bool DeleteCity(int id)
        {
            try
            {
                using (var db = new LiteDatabase(@"MyData.db"))
                {
                    LiteCollection<City> col = db.GetCollection<City>("Cities");
                    col.Delete(id);
                    return true;
                }
            }
            catch
            {
                return false;
            }
            
        }

        public static LiteCollection<City> getCities()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                LiteCollection<City> col = db.GetCollection<City>("Cities");
                return col;
            }
        }

        public static bool AddCountry(Country country, int id)
        {
            try
            {
                using (var db = new LiteDatabase(@"MyData.db"))
                {
                    LiteCollection<Country> countries = db.GetCollection<Country>("Countries");
                    if(id == 0)
                    countries.Insert(country);
                    else
                        countries.Update(country);
                }
                return true;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return false;
            }
        }
     

        public static LiteCollection<Country> getCountries()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                LiteCollection<Country> col = db.GetCollection<Country>("Countries");
                return col;
            }
        }

        public static bool DeleteCountry(int id)
        {
            try
            {
                using (var db = new LiteDatabase(@"MyData.db"))
                {
                    LiteCollection<Country> col = db.GetCollection<Country>("Countries");
                    col.Delete(id);
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }

        public static LiteCollection<User> getUser()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                LiteCollection<User> col = db.GetCollection<User>("Users");
                return col;
            }
        }

        public static bool AddUser(User user)
        {
            try
            {
                using (var db = new LiteDatabase("MyData.db"))
                {
                    LiteCollection<User> col = db.GetCollection<User>("Users");
                    col.Insert(user);
                }
                return true;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return false;
            }


        }

        public static bool UpdateStatus(int id)
        {
            try
            {
                using (var db = new LiteDatabase(@"MyData.db"))
                {
                    LiteCollection<User> col = db.GetCollection<User>("Users");
                    User us = col.FindOne(f => f.UserId == id);
                    us.Status = true;
                    col.Update(us);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}