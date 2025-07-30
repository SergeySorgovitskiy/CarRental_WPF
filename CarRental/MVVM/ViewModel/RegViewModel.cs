using CarRental.MainFolder;
using CarRental.MVVM.Model;
using CarRental.MVVM.Model.UW;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace CarRental.MVVM.ViewModel
{
    public class RegViewModel : ObservableObject
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly MainViewModel _mainViewModel;

        private string _login;
        private string _email;
        private string _password;

        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged(nameof(Login)); }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public RelayCommand RegisterCommand { get; }

        public RegViewModel(UnitOfWork unitOfWork, MainViewModel mainViewModel)
        {
            _unitOfWork = unitOfWork;
            _mainViewModel = mainViewModel;
            RegisterCommand = new RelayCommand(Register, CanRegister);
        }

        private bool CanRegister(object parameter)
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password)
                && (Email.EndsWith("@mail.ru") || Email.EndsWith("@gmail.com"));
        }

        private void Register(object parameter)
        {
            var newUser = new User { UserName = Login, Email = Email, Password = Password };

            bool success = _unitOfWork.UserRepository.AddData(newUser);

            if (success)
            {
                MessageBox.Show("Регистрация успешна!");
                SendRegistrationEmail(); // Отправляем письмо после успешной регистрации
                Login = string.Empty;
                Email = string.Empty;
                Password = string.Empty;
                _mainViewModel.CurrentView = _mainViewModel.LogVM;
            }
            else
            {
                MessageBox.Show("Ошибка регистрации");
            }
        }

        private void SendRegistrationEmail()
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.mail.ru", 587)
                {
                    EnableSsl = true,
                    Timeout = 20000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("twsorgo@mail.ru", "ZXWWwnfhnku6YGi0anas")
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("twsorgo@mail.ru"),
                    Subject = "Прокат авто 'Car Rental'"
                };

                mailMessage.To.Add(Email); // Получатель

                string messageBody = "Добро пожаловать! Спасибо за регистрацию.";
                mailMessage.Body = messageBody;

                client.Send(mailMessage);
                MessageBox.Show("Письмо с подтверждением успешно отправлено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке письма: {ex.Message}");
            }
        }

    }
}
