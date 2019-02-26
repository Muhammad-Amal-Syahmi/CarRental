using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Business;
using DataAccess.DataModel;
using PagedList;

namespace CarRental.Controllers
{
    public class CarController : Controller
    {
        CarBusiness carBusiness = new CarBusiness();

        // GET: Car/?page=&SearchCarModel=&SearchCarLocation
        public ActionResult Index(string SearchCarModel, string SearchLocation, int? page)
        {
            List<Car> ListOfCars = carBusiness.SearchCar(SearchCarModel, SearchLocation);

            return View(ListOfCars.ToPagedList(page ?? 1, 10));
        }

        // GET: Car/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: Car/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,CarModel,Location,PricePerDay")] Car car)
        {
            if (ModelState.IsValid)
            {
                carBusiness.AddCar(car);

                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Car car = carBusiness.FindCar(id);

            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Car/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CarModel,Location,PricePerDay")] Car car)
        {
            if (ModelState.IsValid)
            {
                carBusiness.EditCarDetails(car);

                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = carBusiness.FindCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            carBusiness.DeleteCar(id);

            return RedirectToAction("Index");
        }
    }
}