using CarRental.MainFolder;
using CarRental.MVVM.Model;
using CarRental.MVVM.Model.UW;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CarRental.MVVM.ViewModel
{
    public class BookingsViewModel : ObservableObject
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly MainViewModel _mainViewModel;

        public ObservableCollection<Booking> Bookings { get; set; }

        public ICommand DeleteBookingCommand { get; }

        public BookingsViewModel(UnitOfWork unitOfWork, MainViewModel mainViewModel)
        {
            _unitOfWork = unitOfWork;
            _mainViewModel = mainViewModel;
            DeleteBookingCommand = new RelayCommand(DeleteBooking);
        }

/*        public void LoadBookings()
        {
            var bookingsFromDb = _unitOfWork.BookingRepository.GetDataWithUserId(_mainViewModel.CurrentUser.UserID);
            Bookings = new ObservableCollection<Booking>(bookingsFromDb);
        }*/

        private void DeleteBooking(object parameter)
        {
            Booking booking = parameter as Booking;

            if (booking != null)
            {
                bool success = _unitOfWork.BookingRepository.RemoveData(booking);

                if (success)
                {
                    // Установить IsAvailable в true
                    booking.Car.IsAvailable = false;

                    // Сохранить изменения в базе данных
                    _unitOfWork.CarRepository.ChangeData(booking.Car.CarID, booking.Car);

                    // Удалить бронирование из коллекции
                    Bookings.Remove(booking);
                    _mainViewModel.HomeVM.Cars = new ObservableCollection<Car>(_unitOfWork.CarRepository.GetData().Where(c => c.IsAvailable == false));

                    System.Windows.MessageBox.Show("Бронирование успешно удалено!");
                }
                else
                {
                    System.Windows.MessageBox.Show("Ошибка при удалении бронирования");
                }
            }
        }
    }
}
