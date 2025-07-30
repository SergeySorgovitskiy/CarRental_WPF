using CarRental.MainFolder.Repositories;
using CarRental.MVVM.Model.DataContext;
using CarRental.MVVM.Model.Repositories;
using System;


namespace CarRental.MVVM.Model.UW
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDataContext _context;
        private CarRepository _carRepository;
        private UserRepository _userRepository;
        private BookingRepository _bookingRepository;


        public UnitOfWork(ApplicationDataContext context)
        {
            _context = context;
        }

        public CarRepository CarRepository
        {
            get
            {
                if (_carRepository == null)
                    _carRepository = new CarRepository(_context);
                return _carRepository;
            }
        }
        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }
        public BookingRepository BookingRepository
        {
            get
            {
                if (_bookingRepository == null)
                    _bookingRepository = new BookingRepository(_context);
                return _bookingRepository;
            }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
