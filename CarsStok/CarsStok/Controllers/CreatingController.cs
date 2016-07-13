using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsStok.Controllers
{
    public class CreatingController : Controller
    {
        Models.StokDBEntities context = new Models.StokDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Update(int id)
        {
            var list = context.RentalHistories.Where(i => i.Id == id).First();
            return View("Update", list);
        }

        public ActionResult UpdateComplete(List<string> Data)
        {
            string name = Data[0];
            string model = Data[1];
            int temp;
            int period = (!int.TryParse(Data[2], out temp)) ? 0 : temp;
            int tempId;
            int ID = (!int.TryParse(Data[3], out tempId)) ? 0 : tempId;

            var car = context.Cars.Where(i => i.Cars_model == model);

            if (car.Count() == 0)
            {
                context.Cars.Add(new Models.Car
                {
                    Cars_model = model
                });
                context.SaveChanges();
                car = context.Cars.Where(i => i.Cars_model == model);
            }

            var item = context.RentalHistories.Where(i => i.Id == ID).First();

            item.Id = ID;
            item.Client_name = name;
            item.Cars_model = model;
            item.Rental_period = period;
            item.Cars_id = car.First().Id;

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index", "Home", context);
        }

        [HttpPost]
        public ActionResult Insert(List<string> Data)
        {
            string name = Data[0];
            string model = Data[1];
            int temp;
            int period = (!int.TryParse(Data[2], out temp)) ? 0 : temp;

            var car = context.Cars.Where(i => i.Cars_model == model);

            if (car.Count() == 0)
            {
                context.Cars.Add(new Models.Car
                {
                    Cars_model = model
                });
                context.SaveChanges();
            }

            int id = context.Cars.Where(i => i.Cars_model == model).First().Id;

            var item = new Models.RentalHistory
            {
                Client_name = name,
                Cars_id = id,
                Cars_model = model,
                Rental_period = period
            };
            try
            {
                context.RentalHistories.Add(item);
                context.SaveChanges();
            }
            catch
            {
            }
            return RedirectToAction("Index", "Home", context);
        }

    }
}