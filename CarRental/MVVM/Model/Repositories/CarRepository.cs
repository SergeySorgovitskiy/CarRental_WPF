using CarRental.MainFolder.Repositories;
using CarRental.MVVM.Model.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.MVVM.Model.Repositories
{
    public class CarRepository : IDataRepository<Car>
    {
        private readonly ApplicationDataContext dbContext;

        public CarRepository(ApplicationDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddData(Car data)
        {
            try
            {
                dbContext.Cars.Add(data);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ChangeData(int id, Car newData)
        {
            try
            {
                Car product = dbContext.Cars.Find(id);

                if (product != null)
                {
                    product.Brand = newData.Brand;
                    product.Model = newData.Model;
                    product.Description = newData.Description;
                    product.Year = newData.Year;
                    product.BodyType = newData.BodyType;
                    product.IsAvailable = newData.IsAvailable;
                    product.Price = newData.Price;
                    product.ImagePath = newData.ImagePath;
                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Car> GetData()
        {
            try
            {
                return dbContext.Cars.ToList();
            }
            catch (Exception ex)
            {
                return new List<Car>();
            }
        }

        public bool RemoveData(Car data)
        {
            try
            {
                dbContext.Cars.Remove(data);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
