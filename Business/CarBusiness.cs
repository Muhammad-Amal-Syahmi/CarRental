using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataModel;

namespace Business
{
    public class CarBusiness : ICarBusiness
    {
        //AWS_POSTGREQL_TRIALEntities dbContext = new AWS_POSTGREQL_TRIALEntities();
        private readonly AWS_POSTGREQL_TRIALEntities _dbContext;
        
        public CarBusiness(AWS_POSTGREQL_TRIALEntities dbContext)
        {
            _dbContext = dbContext;
        }
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

            qrySearch = from car in _dbContext.Cars
                        where car.CarModel.ToLower().Contains(CarModel.ToLower()) && car.Location.ToLower().Contains(Location.ToLower())
                        orderby car.Id
                        select car;

            return await qrySearch.ToListAsync();
        }

        public async Task AddCar(Car NewCar)
        {
            //LINQ Query(Comprehension) Syntax
            int max = await _dbContext.Cars.MaxAsync(p => p.Id);
            NewCar.Id = max + 1;
            _dbContext.Cars.Add(NewCar);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Car> FindCar(int? id)
        {
            return await _dbContext.Cars.FindAsync(id);
        }

        public async Task EditCarDetails(Car car)
        {
            _dbContext.Entry(car).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCar(int id)
        {
            Car car = await _dbContext.Cars.FindAsync(id);
            _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();
        }
    }
}
