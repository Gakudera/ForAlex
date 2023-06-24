using Guschin.GraduateProject.Entities;
using Guschin.GraduateProject.ViewModels;
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
using System.Windows.Shapes;

namespace Guschin.GraduateProject.Views
{
    /// <summary>
    /// Логика взаимодействия для СonsultationWindow.xaml
    /// </summary>
    public partial class СonsultationWindow : Window
    {
        private ConsultationViewModel? _viewModel = null;

        public СonsultationWindow(Product? product)
        {
            InitializeComponent();
            //_viewModel = new ConsultationViewModel(product);
            //DataContext = _viewModel;
            _viewModel = (ConsultationViewModel)DataContext;
            _viewModel.Product = product;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
