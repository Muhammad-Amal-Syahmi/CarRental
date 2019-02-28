using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Business;
using DataAccess.DataModel;
using PagedList;

namespace CarRental.Controllers
{
    public class CarController : Controller
    {
        AWS_POSTGREQL_TRIALEntities dbContext;
        CarBusiness carBusiness;

        public CarController()
        {
            dbContext = new AWS_POSTGREQL_TRIALEntities();
            carBusiness = new CarBusiness();
        }

        // GET: Car/
        public async Task<ActionResult> Index(string SearchCarModel, string SearchLocation, int? page)
        {
            List<Car> ListOfCars = await carBusiness.SearchCar(SearchCarModel, SearchLocation);

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
        public async Task<ActionResult> Add([Bind(Include = "Id,CarModel,Location,PricePerDay")] Car car)
        {
            if (ModelState.IsValid)
            {
                await carBusiness.AddCar(car);

                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Car/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Car car = carBusiness.FindCar(id);
            Car car = await carBusiness.FindCar(id);

            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Car/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CarModel,Location,PricePerDay")] Car car)
        {
            if (ModelState.IsValid)
            {
                await carBusiness.EditCarDetails(car);

                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Car/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Car car = carBusiness.FindCar(id);
            Car car = await carBusiness.FindCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await carBusiness.DeleteCar(id);

            return RedirectToAction("Index");
        }
    }
}