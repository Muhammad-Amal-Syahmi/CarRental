using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.DataModel;

namespace Business
{
    public class CarBusiness
    {
        private AWS_POSTGREQL_TRIALEntities dbContext = new AWS_POSTGREQL_TRIALEntities();
        public List<Car> SearchCar(string CarModel, string Location)
        {
            IQueryable<Car> qrySearch;
            if (CarModel != null && Location != null)
            {
                //LINQ Lamda(Method) Syntax
                qrySearch = from car in dbContext.Cars
                            where car.CarModel.ToLower().Contains(CarModel.ToLower()) && car.Location.ToLower().Contains(Location.ToLower())
                            orderby car.Id
                            select car;
            }
            else if (CarModel == null && Location != null)
            {
                qrySearch = from car in dbContext.Cars
                            where car.CarModel.ToLower().Contains(" ") && car.Location.ToLower().Contains(Location.ToLower())
                            orderby car.Id
                            select car;
            }
            else if (CarModel != null && Location == null)
            {
                qrySearch = from car in dbContext.Cars
                            where car.CarModel.ToLower().Contains(CarModel.ToLower()) && car.Location.ToLower().Contains(" ")
                            orderby car.Id
                            select car;
            }
            else
                qrySearch = from car in dbContext.Cars
                            orderby car.Id
                            select car;

            return qrySearch.ToList();
        }

        public void AddCar(Car NewCar)
        {
            //LINQ Query(Comprehension) Syntax
            int max = dbContext.Cars.Max(p => p.Id);
            NewCar.Id = max + 1;
            dbContext.Cars.Add(NewCar);
            dbContext.SaveChanges();
        }

        public Car FindCar(int? id)
        {
            return dbContext.Cars.Find(id);
        }

        public void EditCarDetails(Car car)
        {
            dbContext.Entry(car).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void DeleteCar(int id)
        {
            Car car = dbContext.Cars.Find(id);
            dbContext.Cars.Remove(car);
            dbContext.SaveChanges();
        }
    }
}
