using System.Linq;
using System.Web.Mvc;
using DataAccess.DataModel;
using PagedList;

namespace CarRental.Controllers
{
    public class CarController : Controller
    {
        AWS_POSTGREQL_TRIALEntities dbContext = new AWS_POSTGREQL_TRIALEntities();

        //GET: Car
        //public ActionResult Index()
        //{


        //    //var qryCars = dbContext.Cars
        //    //    .Select(x => new
        //    //    {
        //    //        Id = x.Id,
        //    //        CarModel = x.CarModel,
        //    //        Location = x.Location,
        //    //        PricePerDay = x.PricePerDay
        //    //    }
        //    //    )
        //    //    .ToList();

        //    var qry = from car in dbContext.Cars
        //              orderby car.Id
        //              select car;

        //    return View(qry);
        //}

        // Can we override ActionResult Index ?
        public ActionResult Index(string Search, int? page)
        {
            IQueryable<Car> qrySearch;
            if (Search != null)
                qrySearch = from car in dbContext.Cars
                            where car.CarModel.ToLower().Contains(Search.ToLower())
                            select car;
            else
                qrySearch = from car in dbContext.Cars
                            orderby car.Id
                            select car;

            return View(qrySearch.ToList().ToPagedList(page ?? 1, 10));
        }

        //public ActionResult Index()
        //{
        //    return View(dbContext.Cars.ToList());
        //}
    }
}