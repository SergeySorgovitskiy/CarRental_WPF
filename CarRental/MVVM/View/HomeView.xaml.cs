using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CarRental.MVVM.View
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();

            // Добавляем обработчики событий для текстовых полей
            TextBoxMinPrice.GotFocus += TextBox_GotFocus;
            TextBoxMaxPrice.GotFocus += TextBox_GotFocus;
            TextBoxYear.GotFocus += TextBox_GotFocus;
            TextBoxBodyType.GotFocus += TextBox_GotFocus;

            TextBoxMinPrice.LostFocus += TextBox_LostFocus;
            TextBoxMaxPrice.LostFocus += TextBox_LostFocus;
            TextBoxYear.LostFocus += TextBox_LostFocus;
            TextBoxBodyType.LostFocus += TextBox_LostFocus;

            // Устанавливаем текст-подсказку для всех текстовых полей
            SetPlaceholderText(TextBoxMinPrice);
            SetPlaceholderText(TextBoxMaxPrice);
            SetPlaceholderText(TextBoxYear);
            SetPlaceholderText(TextBoxBodyType);

            // Добавляем обработчики событий ввода текста для всех текстовых полей
            TextBoxMinPrice.PreviewTextInput += TextBox_PreviewTextInput;
            TextBoxMaxPrice.PreviewTextInput += TextBox_PreviewTextInput;
            TextBoxYear.PreviewTextInput += TextBox_PreviewTextInput;
            TextBoxBodyType.PreviewTextInput += TextBox_PreviewTextInput;
        }

        // Метод для установки текста-подсказки для текстового поля
        private void SetPlaceholderText(TextBox textBox)
        {
            textBox.Text = GetPlaceholderText(textBox.Name);
            textBox.Foreground = Brushes.Gray; // Цвет текста-подсказки
        }

        // Метод для определения текста-подсказки по имени текстового поля
        private string GetPlaceholderText(string textBoxName)
        {
            return textBoxName == "TextBoxBodyType" ? "Search" : "0";
        }

        // Обработчик события GotFocus для текстовых полей
        // Обработчик события GotFocus для текстовых полей
        // Обработчик события GotFocus для текстовых полей
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == GetPlaceholderText(textBox.Name))
            {
                textBox.Text = "";
            }
            textBox.Foreground = Brushes.White; // Изменяем цвет текста на черный при фокусировке
        }

        // Обработчик события LostFocus для текстовых полей
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = GetPlaceholderText(textBox.Name); // Восстанавливаем текст-подсказку
                textBox.Foreground = Brushes.Gray; // Возвращаем серый цвет текста, если поле осталось пустым
            }
        }



        // Метод для проверки, является ли текст текстом-подсказкой
        private bool IsPlaceholderText(string text)
        {
            return text == "BodyType" || text == "0";
        }

        // Обработчик события PreviewTextInput для всех текстовых полей
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (((TextBox)sender).Name == "TextBoxMinPrice" || ((TextBox)sender).Name == "TextBoxMaxPrice")
            {
                // Для полей Min Price и Max Price проверяем, что введенные символы являются цифрами
                e.Handled = !IsNumeric(e.Text);
            }
            else if (((TextBox)sender).Name == "TextBoxYear")
            {
                // Для поля Year проверяем, что введенные символы являются цифрами и находятся в допустимом диапазоне
                e.Handled = !IsYearValid(e.Text);
            }
            else if (((TextBox)sender).Name == "TextBoxBodyType")
            {
                // Для поля Body Type проверяем, что введенные символы являются буквами
                e.Handled = !IsLetter(e.Text);
            }
        }

        // Метод для проверки, что строка состоит только из цифр
        private bool IsNumeric(string input)
        {
            return input.All(char.IsDigit);
        }

        // Метод для проверки, что строка состоит только из букв
        private bool IsLetter(string input)
        {
            return input.All(char.IsLetter);
        }

        // Метод для проверки, что год в допустимом диапазоне от 1 до 2024
        private bool IsYearValid(string input)
        {
            if (!IsNumeric(input))
                return false;

            int year;
            if (!int.TryParse(input, out year))
                return false;

            return year >= 0 && year <= 2024;
        }
    }
}
