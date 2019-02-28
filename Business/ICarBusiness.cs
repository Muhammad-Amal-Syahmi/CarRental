using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DataModel;

namespace Business
{
    public interface ICarBusiness
    {
        Task AddCar(Car NewCar);
        Task DeleteCar(int id);
        Task EditCarDetails(Car car);
        Task<Car> FindCar(int? id);
        Task<IEnumerable<Car>> SearchCar(string CarModel, string Location);
    }
}