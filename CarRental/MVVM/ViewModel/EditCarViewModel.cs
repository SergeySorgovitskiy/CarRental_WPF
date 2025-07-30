using CarRental.MainFolder;
using CarRental.MVVM.Model;
using CarRental.MVVM.Model.UW;
using System.Diagnostics;
using System.Windows.Forms;

namespace CarRental.MVVM.ViewModel
{
    public class EditCarViewModel : ObservableObject
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly MainViewModel _mainViewModel;

        private Car _selectedCar;

        public RelayCommand ChooseImageCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }

        public Car SelectedCar
        {
            get { return _selectedCar; }
            set
            {
                _selectedCar = value;
                Brand = _selectedCar.Brand;
                Model = _selectedCar.Model;
                Description = _selectedCar.Description;
                Year = _selectedCar.Year;
                BodyType = _selectedCar.BodyType;
                Price = _selectedCar.Price;
                ImagePath = _selectedCar.ImagePath;
                OnPropertyChanged(nameof(SelectedCar));
            }
        }

        private string _brand;
        public string Brand
        {
            get { return _brand; }
            set { _brand = value; OnPropertyChanged(nameof(Brand)); }
        }

        private string _model;
        public string Model
        {
            get { return _model; }
            set
            {
                _model = value; OnPropertyChanged(nameof(Model));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        private int _year;
        public int Year
        {
            get { return _year; }
            set { _year = value; OnPropertyChanged(nameof(Year)); }
        }

        private string _bodyType;
        public string BodyType
        {
            get { return _bodyType; }
            set { _bodyType = value; OnPropertyChanged(nameof(BodyType)); }
        }

        private int _price;
        public int Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(nameof(Price)); }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; OnPropertyChanged(nameof(ImagePath)); }
        }

        public EditCarViewModel(UnitOfWork unitOfWork, MainViewModel mainViewModel)
        {
            _unitOfWork = unitOfWork;
            _mainViewModel = mainViewModel;
            ChooseImageCommand = new RelayCommand(ChooseImage);
            SaveCommand = new RelayCommand(Save, CanSave);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void ChooseImage(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImagePath = openFileDialog.FileName;
            }
        }

        private bool CanSave(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Brand) && !string.IsNullOrWhiteSpace(Model) &&
                   !string.IsNullOrWhiteSpace(Description) && Year > 0 &&
                   !string.IsNullOrWhiteSpace(BodyType) && Price > 0 &&
                   !string.IsNullOrWhiteSpace(ImagePath);
        }

        private void Save(object parameter)
        {
            var updatedCar = new Car
            {
                CarID = SelectedCar.CarID,
                Brand = Brand,
                Model = Model,
                Description = Description,
                Year = Year,
                BodyType = BodyType,
                Price = Price,
                ImagePath = ImagePath
            };

            bool success = _unitOfWork.CarRepository.ChangeData(SelectedCar.CarID, updatedCar);

            if (success)
            {
                System.Windows.MessageBox.Show("Информация об автомобиле успешно обновлена!");
                _mainViewModel.CarManageVM.LoadCars();
                ClearFields();
            }
            else
            {
                System.Windows.MessageBox.Show("Ошибка при обновлении информации об автомобиле");
            }
        }

        private void Cancel(object parameter)
        {
            ClearFields();
            _mainViewModel.CurrentView = _mainViewModel.CarManageVM;
        }

        private void ClearFields()
        {
            Brand = string.Empty;
            Model = string.Empty;
            Description = string.Empty;
            Year = 0;
            BodyType = string.Empty;
            Price = 0;
            ImagePath = string.Empty;
        }
    }
}
