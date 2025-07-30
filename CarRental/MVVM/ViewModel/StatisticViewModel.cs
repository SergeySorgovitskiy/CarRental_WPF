using CarRental.MainFolder;
using CarRental.MVVM.Model.UW;
using System.Collections.ObjectModel;

namespace CarRental.MVVM.ViewModel
{
    public class StatisticViewModel : ObservableObject
    {
        private readonly UnitOfWork _unitOfWork;

        private int _totalCars;
        public int TotalCars
        {
            get { return _totalCars; }
            set { _totalCars = value; OnPropertyChanged(nameof(TotalCars)); }
        }

        private int _totalUsers;
        public int TotalUsers
        {
            get { return _totalUsers; }
            set { _totalUsers = value; OnPropertyChanged(nameof(TotalUsers)); }
        }

        private int _activeBookings;
        public int ActiveBookings
        {
            get { return _activeBookings; }
            set { _activeBookings = value; OnPropertyChanged(nameof(ActiveBookings)); }
        }

        public StatisticViewModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            TotalCars = _unitOfWork.CarRepository.GetData().Count;
            TotalUsers = _unitOfWork.UserRepository.GetData().Count;
            ActiveBookings = _unitOfWork.BookingRepository.GetData().Count; 
        }
    }
}
