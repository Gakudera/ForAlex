using Guschin.GraduateProject.Entities;
using Guschin.GraduateProject.ViewModels;
using Guschin.GraduateProject.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Guschin.GraduateProject.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private ProductWindowViewModel? _viewModel = null;
        private User _user;
        public ProductWindow(Guid? productId, User user)
        {
            InitializeComponent();
            _viewModel = new ProductWindowViewModel(productId); 
            DataContext = _viewModel;
            _user = user;
        }

        private void Consultation_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
                new СonsultationWindow(_viewModel.Product, _user).ShowDialog();
        }
    }
}
