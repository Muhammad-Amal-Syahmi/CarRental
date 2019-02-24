using System.Linq;
using System.Web.Mvc;
using DataAccess.DataModel;

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
        public ActionResult Index(string Search)
        {
            IQueryable<Car> qrySearch;
            //if (Search != null)
            qrySearch = from car in dbContext.Cars
                        where car.CarModel.Contains(Search)
                        select car;
            //else
            //    qrySearch = from car in dbContext.Cars
            //                orderby car.Id
            //                select car;

            return View(qrySearch);
        }

        //public ActionResult Index()
        //{
        //    return View(dbContext.Cars.ToList());
        //}
    }
}