using System.Collections.ObjectModel;
using System.Windows.Input;
using CarRental.MainFolder;
using CarRental.MVVM.Model;
using CarRental.MVVM.Model.UW;
using CarRental.MVVM.View;

namespace CarRental.MVVM.ViewModel
{
    public class CarManageViewModel : ObservableObject
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly MainViewModel _mainViewModel;

        private ObservableCollection<Car> _cars;
        public ObservableCollection<Car> Cars
        {
            get { return _cars; }
            set
            {
                _cars = value;
                OnPropertyChanged();
            }
        }

        private Car _selectedCar;
        public Car SelectedCar
        {
            get { return _selectedCar; }
            set
            {
                _selectedCar = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCarCommand { get; }

        // Остальной код

        private void DeleteCar(object parameter)
        {
            Car car = parameter as Car;

            if (parameter is Car selectedCar)
            {
                bool success = _unitOfWork.CarRepository.RemoveData(car);

                if (success)
                {
                    System.Windows.MessageBox.Show("Автомобиль успешно удален!");
                    LoadCars();
                }
                else
                {
                    System.Windows.MessageBox.Show("Ошибка при удалении автомобиля");
                }
            }
        }
        public ICommand AddCarCommand { get; }
        public ICommand EditCarCommand { get; }

        // Пустой конструктор без параметров
        public CarManageViewModel()
        {
            // Инициализация необходимых переменных или другие операции
        }

        public CarManageViewModel(UnitOfWork unitOfWork, MainViewModel mainViewModel)
        {
            _unitOfWork = unitOfWork;
            _mainViewModel = mainViewModel;
            LoadCars();
            AddCarCommand = new RelayCommand(AddCar);
            EditCarCommand = new RelayCommand(EditCar);
            DeleteCarCommand = new RelayCommand(DeleteCar);
        }

        public void LoadCars()
        {
            var carsFromDb = _unitOfWork.CarRepository.GetData();
            Cars = new ObservableCollection<Car>(carsFromDb);
        }

        private void EditCar(object parameter)
        {
            Car car = parameter as Car;

            if (parameter is Car selectedCar)
            {
                _mainViewModel.EditCarVM.SelectedCar = car;
                _mainViewModel.CurrentView = _mainViewModel.EditCarVM;
            }
        }

        public void AddCar(object parameter)
        {
            _mainViewModel.CurrentView = _mainViewModel.AddCarVM;
        }

        public void ApplySearch()
        {
            if (!string.IsNullOrWhiteSpace(_mainViewModel.SearchText))
            {
                Cars = new ObservableCollection<Car>(_unitOfWork.CarRepository.GetData().Where(car => car.Brand.ToLower().Contains(_mainViewModel.SearchText.ToLower())));
            }
            else
            {
                LoadCars();
            }
        }
    }
}
