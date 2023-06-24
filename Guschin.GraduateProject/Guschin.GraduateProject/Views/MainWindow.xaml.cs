using Guschin.GraduateProject.Entities;
using Guschin.GraduateProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _role = "";
        private MainWindowViewModel? _viewModel = null;

        public MainWindow(User? user)
        {
            InitializeComponent();
            _viewModel = (MainWindowViewModel)DataContext;

            var role = GetRole(user.Id);

            _role = role;
            if(role == "Администратор")
            {
                FAQButton.Visibility = Visibility.Visible;
            }
            else if(role == "Сотрудник")
            {
                FAQButton.Visibility = Visibility.Visible;
            }
            else if(role == "Клиент")
            {
                FAQButton.Visibility = Visibility.Collapsed;
            }
        }

        private string GetRole(Guid id)
        {
            using(var db = new ApplicationDbContext())
            {
                var userRole = db.UserRoles.Include(u => u.Role).SingleOrDefault(u => u.UserId == id);

                if (userRole == null)
                    return "Клиент";

                return userRole.Role.RoleName;
            }
        }

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_viewModel != null && _viewModel.SelectedProduct != null)
                new ProductWindow(_viewModel.SelectedProduct.Id).ShowDialog();
        }

        private void OpenFAQ_Click(object sender, RoutedEventArgs e)
        {
            new FAQWindow(_role).ShowDialog();
        }
    }
}
