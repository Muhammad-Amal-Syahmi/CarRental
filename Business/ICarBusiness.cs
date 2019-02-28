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
        Task<List<Car>> SearchCar(string CarModel, string Location);
    }
}