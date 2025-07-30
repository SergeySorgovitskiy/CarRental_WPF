using CarRental.MainFolder.Repositories;
using CarRental.MVVM.Model.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.MVVM.Model.Repositories
{
    public class UserRepository : IDataRepository<User>
    {
        private readonly ApplicationDataContext dbContext;

        public UserRepository(ApplicationDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddData(User data)
        {
            try
            {
                dbContext.Users.Add(data);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangeData(int id, User newData)
        {
            try
            {
                User user = dbContext.Users.Find(id);

                if (user != null)
                {
                    user.UserName = newData.UserName;
                    user.Email = newData.Email;
                    user.Password = newData.Password;
                    user.IsAdmin = newData.IsAdmin;

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

        public List<User> GetData()
        {
            try
            {
                return dbContext.Users.ToList();
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }

        public bool RemoveData(User data)
        {
            try
            {
                dbContext.Users.Remove(data);
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