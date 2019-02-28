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
        private readonly ICarBusiness _carBusiness;

        public CarController(ICarBusiness carBusiness)
        {
            _carBusiness = carBusiness;
        }

        // GET: Car/
        public async Task<ActionResult> Index(string SearchCarModel, string SearchLocation, int? page)
        {
            var ListOfCars = await _carBusiness.SearchCar(SearchCarModel, SearchLocation);

            return View( ListOfCars.ToPagedList(page ?? 1, 10));
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
                await _carBusiness.AddCar(car);

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
            Car car = await _carBusiness.FindCar(id);

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
                await _carBusiness.EditCarDetails(car);

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
            Car car = await _carBusiness.FindCar(id);
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
            await _carBusiness.DeleteCar(id);

            return RedirectToAction("Index");
        }
    }
}