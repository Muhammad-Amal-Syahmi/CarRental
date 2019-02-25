using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataAccess.DataModel;
using PagedList;

namespace CarRental.Controllers
{
    public class CarController : Controller
    {
        private AWS_POSTGREQL_TRIALEntities dbContext = new AWS_POSTGREQL_TRIALEntities();

        public ActionResult Index(string Search, string SearchType, int? page)
        {
            IQueryable<Car> qrySearch;
            if (Search != null)
            {
                if (SearchType == "CarModel")
                    qrySearch = from car in dbContext.Cars
                                where car.CarModel.ToLower().Contains(Search.ToLower())
                                orderby car.Id
                                select car;
                else
                    qrySearch = from car in dbContext.Cars
                                where car.Location.ToLower().Contains(Search.ToLower())
                                orderby car.Id
                                select car;
            }

            else
                qrySearch = from car in dbContext.Cars
                            orderby car.Id
                            select car;

            return View(qrySearch.ToList().ToPagedList(page ?? 1, 10));
        }

        public ActionResult Add()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,CarModel,Location,PricePerDay")] Car car)
        {
            if (ModelState.IsValid)
            {
                int max = dbContext.Cars.Max(p => p.Id);
                car.Id = max + 1;
                dbContext.Cars.Add(car);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = dbContext.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CarModel,Location,PricePerDay")] Car car)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(car).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = dbContext.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = dbContext.Cars.Find(id);
            dbContext.Cars.Remove(car);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}