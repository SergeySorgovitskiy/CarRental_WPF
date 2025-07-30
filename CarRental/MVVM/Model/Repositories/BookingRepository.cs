using CarRental.MainFolder.Repositories;
using CarRental.MVVM.Model.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.MVVM.Model.Repositories
{
    public class BookingRepository : IDataRepository<Booking>, IDataRepositoryWithUserId<Booking>
    {
        private readonly ApplicationDataContext dbContext;

        public BookingRepository(ApplicationDataContext dbContext)
        {
            this.dbContext = dbContext;
        }


        // Реализация метода GetData(int userId)
        public List<Booking> GetDataWithUserId(int userId)
        {
            try
            {
                return dbContext.Bookings
                    .Include(b => b.User)
                    .Include(b => b.Car)
                    .Where(b => b.UserID == userId)
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Booking>();
            }
        }
        public bool AddData(Booking data)
        {
            try
            {
                dbContext.Bookings.Add(data);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangeData(int id, Booking newData)
        {
            try
            {
                Booking booking = dbContext.Bookings.Find(id);

                if (booking != null)
                {
                    booking.UserID = newData.UserID;
                    booking.CarID = newData.CarID;
                    booking.BookingID = newData.BookingID;

                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Реализация метода GetData(int userId)
        public List<Booking> GetData()
        {
            try
            {
                return dbContext.Bookings.ToList();
            }
            catch (Exception)
            {
                return new List<Booking>();
            }
        }

        public bool RemoveData(Booking data)
        {
            try
            {
                dbContext.Bookings.Remove(data);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
