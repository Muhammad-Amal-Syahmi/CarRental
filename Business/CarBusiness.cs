using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataModel;

namespace Business
{
    public class CarBusiness : ICarBusiness
    {
        AWS_POSTGREQL_TRIALEntities dbContext = new AWS_POSTGREQL_TRIALEntities();
        public async Task<List<Car>> SearchCar(string CarModel, string Location)
        {
            IQueryable<Car> qrySearch;
            if (CarModel == null)
            {
                CarModel = "";
            }
            if (Location == null)
            {
                Location = "";
            }

            qrySearch = from car in dbContext.Cars
                        where car.CarModel.ToLower().Contains(CarModel.ToLower()) && car.Location.ToLower().Contains(Location.ToLower())
                        orderby car.Id
                        select car;

            return await qrySearch.ToListAsync();
        }

        public async Task AddCar(Car NewCar)
        {
            //LINQ Query(Comprehension) Syntax
            int max = await dbContext.Cars.MaxAsync(p => p.Id);
            NewCar.Id = max + 1;
            dbContext.Cars.Add(NewCar);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Car> FindCar(int? id)
        {
            return await dbContext.Cars.FindAsync(id);
        }

        public async Task EditCarDetails(Car car)
        {
            dbContext.Entry(car).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCar(int id)
        {
            Car car = await dbContext.Cars.FindAsync(id);
            dbContext.Cars.Remove(car);
            await dbContext.SaveChangesAsync();
        }
    }
}
