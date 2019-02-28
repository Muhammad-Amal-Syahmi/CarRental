using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataModel;

namespace Business
{
    public class CarBusiness : ICarBusiness
    {
        private readonly AWS_POSTGREQL_TRIALEntities _dbContext;

        public CarBusiness(AWS_POSTGREQL_TRIALEntities dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Car>> SearchCar(string carModel, string location)
        {
            IQueryable<Car> qrySearch;
            if (carModel == null)
            {
                carModel = "";
            }
            if (location == null)
            {
                location = "";
            }

            // Query syntax
            //qrySearch = from car in _dbContext.Cars
            //            where car.CarModel.ToLower().Contains(carModel.ToLower()) && car.Location.ToLower().Contains(location.ToLower())
            //            orderby car.Id
            //            select car;

            // Lamda syntax
            qrySearch = _dbContext.Cars
                .Where(c =>
                    c.CarModel.ToLower().Contains(carModel.ToLower())
                    &&
                    c.Location.ToLower().Contains(location.ToLower())
                 )
                .OrderBy(c => c.Id)
                .Select(c => c);

            return await qrySearch.ToListAsync();
        }

        public async Task AddCar(Car NewCar)
        {
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
