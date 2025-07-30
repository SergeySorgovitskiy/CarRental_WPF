using CarRental.MVVM.Model.DataContext;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CarRental
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            new ApplicationDataContext();
            
        }
    }

}
