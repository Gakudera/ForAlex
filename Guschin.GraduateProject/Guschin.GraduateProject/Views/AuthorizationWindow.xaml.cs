//using Guschin.GraduateProject.Data;
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
using Guschin.GraduateProject.Views;
using System.Windows.Navigation;
using Guschin.GraduateProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace Guschin.GraduateProject.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new RegistrationWindow());
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            User? user = null;
            string login = TxbLogin.Text;
            string password = TxbPassword.Text;

            using(var db = new ApplicationDbContext())
            {
                user = db.Users.AsNoTracking().SingleOrDefault(u => u.Email == login);
            }

            if (user == null)
            {
                MessageBox.Show("unknown user.");
                return;
            }

            if (user.PasswordHash != password)
            {
                MessageBox.Show("invalid login or password.");
                return;
            }

            //var role = GetRole(user.Id);

            OpenWindow(new MainWindow(user));

            /*if (role == "Клиент")
                OpenWindow(new MainWindow(user));
            else if (role == "Администратор")
                OpenWindow(new Window());
            else if (role == "Сотрудник")
                OpenWindow(new Window());
            else
                MessageBox.Show("unknown user.");*/
        }

        /*private string GetRole(Guid userId)
        {
            string result = "";

            using(var db = new ApplicationDbContext())
            {
                var userRole = db.UserRoles
                    .AsNoTracking()
                    .Include(r => r.Role)
                    .SingleOrDefault(u => u.UserId == userId);

                if (userRole == null)
                    return result;

                result = userRole.Role.RoleName;
            }

            return result;
        }*/

        private void OpenWindow(Window window)
        {
            Window current = Application.Current.MainWindow;

            window.Show();
            Application.Current.MainWindow = window;
            current.Close();
        }
    }
}
