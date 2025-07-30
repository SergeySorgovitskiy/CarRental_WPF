using CarRental.MainFolder;
using CarRental.MVVM.Model.UW;
using CarRental.MVVM.Model;
using System;
using System.Windows.Forms;

namespace CarRental.MVVM.ViewModel
{
    public class AddCarViewModel : ObservableObject
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly MainViewModel _mainViewModel;

        private string _brand;
        private string _model;
        private string _description;
        private int _year;
        private string _bodyType;
        private int _price;
        private string _imagePath;

        public RelayCommand ChooseImageCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }

        public string Brand
        {
            get { return _brand; }
            set { _brand = value; OnPropertyChanged(nameof(Brand)); }
        }

        public string Model
        {
            get { return _model; }
            set { _model = value; OnPropertyChanged(nameof(Model)); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        public int Year
        {
            get { return _year; }
            set { _year = value; OnPropertyChanged(nameof(Year)); }
        }

        public string BodyType
        {
            get { return _bodyType; }
            set { _bodyType = value; OnPropertyChanged(nameof(BodyType)); }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(nameof(Price)); }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; OnPropertyChanged(nameof(ImagePath)); }
        }

        public AddCarViewModel(UnitOfWork unitOfWork, MainViewModel mainViewModel)
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
            var newCar = new Car
            {
                Brand = Brand,
                Model = Model,
                Description = Description,
                Year = Year,
                BodyType = BodyType,
                Price = Price,
                ImagePath = ImagePath
            };

            bool success = _unitOfWork.CarRepository.AddData(newCar);

            if (success)
            {
                System.Windows.MessageBox.Show("Автомобиль успешно добавлен!");
                _mainViewModel.CarManageVM.LoadCars();
                _mainViewModel.HomeVM.LoadCars();
                ClearFields();
            }
            else
            {
                System.Windows.MessageBox.Show("Ошибка при добавлении автомобиля");
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
