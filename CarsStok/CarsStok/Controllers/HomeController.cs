using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsStok.Controllers
{
    public class HomeController : Controller
    {
        Models.StokDBEntities context = new Models.StokDBEntities();
        IList<Models.RentalHistory> rentals;

        public ActionResult Index()
        {
            if (context.RentalHistories.ToList() == null) AddEntries();

            var list = (from item in context.RentalHistories
                        select new
                        {
                            Id = item.Id,
                            Client_name = item.Client_name,
                            Cars_id = item.Cars_id,
                            Cars_model =item.Cars_model,
                            Rental_period = item.Rental_period
                        }).ToList();
            return View(list);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var item = context.RentalHistories.Where(x => x.Id == id).First();
                context.RentalHistories.Remove(item);
                context.SaveChanges();
            }
            catch
            {
            }
            return RedirectToAction("Index");
        }

        public ActionResult Select(string name)
        {
            var list = (from item in context.RentalHistories
                         where item.Client_name == name
                         select item
                         ).ToList();

            return View("Index", list);
        }

        private void AddEntries()
        {
            rentals = new List<Models.RentalHistory>();
            rentals.Add(new Models.RentalHistory
            {
                Client_name = "Vlad",
                Cars_id = 1,
                Cars_model = "Zaz",
                Rental_period = 10000
            });
            try
            {
                context.RentalHistories.AddRange(rentals);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}