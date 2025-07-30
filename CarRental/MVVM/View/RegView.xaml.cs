using CarRental.MVVM.Model.DataContext;
using CarRental.MVVM.Model.UW;
using CarRental.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarRental.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для RegView.xaml
    /// </summary>
    public partial class RegView : UserControl
    {
        public RegView()
        {
            InitializeComponent();
            //DataContext = new RegViewModel(new UnitOfWork(new ApplicationDataContext())); 
        }
    }
}
