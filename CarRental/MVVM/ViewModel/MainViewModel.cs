using CarRental.MainFolder;
using CarRental.MVVM.Model.UW;
using System.Windows;
using System.Windows.Input;
using CarRental.MVVM.Model.DataContext;
using CarRental.MVVM.Model;

namespace CarRental.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private bool _isMainViewVisible;
        private bool _isAdmin;
        private readonly UnitOfWork _unitOfWork;

        private string _searchText;//// Добавляем обновление данных в HomeViewModel при изменении SearchText

        private User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
                HomeVM.ApplySearch();
                CarManageVM.ApplySearch();
            }
        }

        public bool IsMainViewVisible
        {
            get { return _isMainViewVisible; }
            set
            {
                _isMainViewVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoggedIn;

        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                _isLoggedIn = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SignUpCommand { get; set; }
        public RelayCommand RegCommand { get; set; }
        public RelayCommand LogViewCommand { get; set; }
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand BookingsViewCommand { get; set; }
        public RelayCommand StatisticViewCommand { get; set; }
        public RelayCommand CarManageViewCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }

        public LogViewModel LogVM { get; set; }
        public RegViewModel RegVM { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public CarManageViewModel CarManageVM { get; set; }
        public BookingsViewModel BookingsVM { get; set; }
        public StatisticViewModel StatisticVM { get; set; }
        public AddCarViewModel AddCarVM { get; set; }

        public EditCarViewModel EditCarVM { get; set; }

        public ICommand CloseApplicationCommand { get; }
        public ICommand MinimizeApplicationCommand { get; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {

            CloseApplicationCommand = new RelayCommand(CloseApplication);
            MinimizeApplicationCommand = new RelayCommand(MinimizeApplication);
            LogoutCommand = new RelayCommand(Logout);

            ApplicationDataContext context = new ApplicationDataContext();
            _unitOfWork = new UnitOfWork(context);

            RegVM = new RegViewModel(_unitOfWork, this);
            LogVM = new LogViewModel(_unitOfWork, this);
            HomeVM = new HomeViewModel(_unitOfWork, this); // Передаем ссылку на MainViewModel в HomeViewModel
            CarManageVM = new CarManageViewModel(_unitOfWork, this);
            BookingsVM = new BookingsViewModel(_unitOfWork, this);
            StatisticVM = new StatisticViewModel(_unitOfWork);
            AddCarVM = new AddCarViewModel(_unitOfWork, this);
            EditCarVM = new EditCarViewModel(_unitOfWork, this);

            CurrentView = LogVM;

            LogViewCommand = new RelayCommand(o => { CurrentView = LogVM; });
            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });
            BookingsViewCommand = new RelayCommand(o => { CurrentView = BookingsVM; });
            StatisticViewCommand = new RelayCommand(o => { CurrentView = StatisticVM; });
            CarManageViewCommand = new RelayCommand(o => { CurrentView = CarManageVM; });
        }

        private void CloseApplication(object parameter)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeApplication(object parameter)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Logout(object parameter)
        {
            // Логика выхода из системы
            //_isMainViewVisible = false;
            IsMainViewVisible = false;
            IsAdmin = false;
            CurrentView = LogVM;
            IsLoggedIn = false; // Установка флага успешного входа в false
        }
    }
}
