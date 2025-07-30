using CarRental.MainFolder;
using CarRental.MVVM.Model.UW;
using System.Windows;
using System.Collections.ObjectModel;

namespace CarRental.MVVM.ViewModel
{
    public class LogViewModel : ObservableObject
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly MainViewModel _mainViewModel;
        private string _login;
        private string _password;

        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged(nameof(Login)); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public RelayCommand LoginCommand { get; }

        public RelayCommand RegCommand { get; }

        public LogViewModel(UnitOfWork unitOfWork, MainViewModel mainViewModel)
        {
            _unitOfWork = unitOfWork;
            _mainViewModel = mainViewModel;
            LoginCommand = new RelayCommand(LoginCom, CanLogin);
            RegCommand = new RelayCommand(o => { _mainViewModel.CurrentView = _mainViewModel.RegVM; });
        }

        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
        }

        private void LoginCom(object parameter)
        {
            var users = _unitOfWork.UserRepository.GetData();

            foreach (var user in users)
            {
                if (user.UserName == Login && user.Password == Password)
                {
                    MessageBox.Show("Успешный вход!");
                    _mainViewModel.IsMainViewVisible = true;
                    _mainViewModel.IsAdmin = user.IsAdmin;
                    _mainViewModel.CurrentView = _mainViewModel.HomeVM;
                    _mainViewModel.CurrentUser = user;
                    _mainViewModel.BookingsVM.Bookings = new ObservableCollection<Model.Booking>(_unitOfWork.BookingRepository.GetDataWithUserId(_mainViewModel.CurrentUser.UserID));
                    return;
                }
            }
            MessageBox.Show("Неверное имя пользователя или пароль!");
        }
    }
}
