using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CarRental.MainFolder;
using CarRental.MVVM.Model;
using CarRental.MVVM.Model.UW;

namespace CarRental.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly MainViewModel _mainViewModel;
        private ObservableCollection<Car> _cars;
        private decimal _minPriceFilter;
        private decimal _maxPriceFilter;
        private string _bodyTypeFilter;
        private decimal _yearFilter;

        public ICommand BookingCarCommand { get; }
        public ICommand ClearFiltersCommand { get; }

        public ObservableCollection<Car> Cars
        {
            get { return _cars; }
            set
            {
                _cars = value;
                OnPropertyChanged();
            }
        }

        public decimal MinPriceFilter
        {
            get { return _minPriceFilter; }
            set
            {
                _minPriceFilter = value;
                OnPropertyChanged();
                LoadCars();
            }
        }

        public decimal MaxPriceFilter
        {
            get { return _maxPriceFilter; }
            set
            {
                _maxPriceFilter = value;
                OnPropertyChanged();
                LoadCars();
            }
        }

        public string BodyTypeFilter
        {
            get { return _bodyTypeFilter; }
            set
            {
                _bodyTypeFilter = value;
                OnPropertyChanged();
                LoadCars();
            }
        }

        public decimal YearFilter
        {
            get { return _yearFilter; }
            set
            {
                _yearFilter = value;
                OnPropertyChanged();
                LoadCars();
            }
        }

        public void LoadCars()
        {
            Cars = new ObservableCollection<Car>(ApplyFilters(_unitOfWork.CarRepository.GetData().Where(c => c.IsAvailable == false)));
        }

        private IEnumerable<Car> ApplyFilters(IEnumerable<Car> cars)
        {
            var filteredCars = cars;

            if (MinPriceFilter > 0)
                filteredCars = filteredCars.Where(car => car.Price >= MinPriceFilter);

            if (MaxPriceFilter > 0)
                filteredCars = filteredCars.Where(car => car.Price <= MaxPriceFilter);

            if (!string.IsNullOrEmpty(BodyTypeFilter))
                filteredCars = filteredCars.Where(car => car.BodyType == BodyTypeFilter);

            if (YearFilter > 0)
                filteredCars = filteredCars.Where(car => car.Year == YearFilter);

            return filteredCars.ToList();
        }

        private void ClearFilters()
        {
            MinPriceFilter = 0;
            MaxPriceFilter = 0;
            BodyTypeFilter = null;
            YearFilter = 0;
            LoadCars();
        }

        public HomeViewModel(UnitOfWork unitOfWork, MainViewModel mainViewModel)
        {
            _unitOfWork = unitOfWork;
            _mainViewModel = mainViewModel;

            LoadCars();
            ClearFiltersCommand = new RelayCommand(o => ClearFilters());
            BookingCarCommand = new RelayCommand(BookingCar, CanBookCar);
        }

        private void BookingCar(object parameter)
        {
            Car car = (Car) parameter;
                car.IsAvailable= true;
            _unitOfWork.CarRepository.ChangeData(car.CarID, car);
                var currentUser = _mainViewModel.CurrentUser;
                if (currentUser == null)
                {
                    MessageBox.Show("User not logged in");
                    return;
                }

                var newBooking = new Booking
                {
                    UserID = currentUser.UserID,
                    CarID = car.CarID,
                    User = currentUser,
                    Car = car,
                };

                bool success = _unitOfWork.BookingRepository.AddData(newBooking);

                if (success)
                {
                    MessageBox.Show("Booking successful!");
                _mainViewModel.BookingsVM.Bookings.Add(newBooking);
                Cars = new ObservableCollection<Car>(_unitOfWork.CarRepository.GetData().Where(c => c.IsAvailable == false));
                }
                else
                {
                    MessageBox.Show("Error during booking");
                }
        }

        private bool CanBookCar(object parameter)
        {
            // В этом методе вы можете определить, можно ли забронировать автомобиль
            // Например, проверка доступности автомобиля
            // Здесь мы предполагаем, что автомобиль всегда доступен для бронирования
            return true;
        }


        public void ApplySearch()
        {
            if (!string.IsNullOrWhiteSpace(_mainViewModel.SearchText))
            {
                Cars = new ObservableCollection<Car>(_unitOfWork.CarRepository.GetData()
                    .Where(car => car.Brand.ToLower().Contains(_mainViewModel.SearchText.ToLower())));
            }
            else
            {
                LoadCars();
            }
        }
    }
}
