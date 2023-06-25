using Guschin.GraduateProject.Entities;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            datePickerDateOfBirth.SelectedDate = DateTime.Now;
            comboBoxGender.ItemsSource = new List<string> { "мужской", "женский" };
            comboBoxGender.SelectedIndex = 0;
        }

        private void Button_Registration_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxEmail.Text;
            string pass = textBoxPass.Text;    
            string phone = textBoxPhone.Text;
            string firstName = textBoxFirstName.Text;
            string lastName = textBoxLastName.Text;
            string middleName = textBoxMiddleName.Text;
            DateTime? dateOfBirth = datePickerDateOfBirth.SelectedDate;
            string gender = comboBoxGender.SelectedItem.ToString();
             

            using (var ctx = new ApplicationDbContext())
            {
                if (isFieldsNull(login, pass, phone, firstName, lastName, middleName, gender))
                    MessageBox.Show("Поля должны быть заполнены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (isUserExist(login, pass, phone, ctx))
                    MessageBox.Show("Используйте другие учётные данные!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (isEmailExist(login, ctx))
                    MessageBox.Show("Используйте другой email!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (isPasswordExist(pass, ctx))
                    MessageBox.Show("Используйте другой email!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (isPhoneExist(phone, ctx))
                    MessageBox.Show("Используйте другой email!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    var person = new Person { DateOfBirth = dateOfBirth.Value.Date, FirstName = firstName, LastName = lastName, MiddleName = middleName, Gender = gender, Id = Guid.NewGuid() };
                    var user = new User { Id = Guid.NewGuid(), Email = login, EmailConfirmed=false, IsDeleted=false, PasswordHash = pass, Phone =phone, PhoneNumberConfirmed = false, Person = person};
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                    MessageBox.Show("Регистрация успешна!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
        }
        private bool isFieldsNull(string login, string pass, string phone, string firstName, string lastName, string middleName, string gender) => string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(middleName) || string.IsNullOrEmpty(gender);
        private bool isUserExist(string login, string pass, string phone, ApplicationDbContext ctx) => ctx.Users.Any(u => u.Email == login && u.PasswordHash == pass && u.Phone == phone);
        private bool isEmailExist(string login, ApplicationDbContext ctx) => ctx.Users.Any(u => u.Email == login);
        private bool isPasswordExist(string pass, ApplicationDbContext ctx) => ctx.Users.Any(u => u.PasswordHash == pass);
        private bool isPhoneExist(string phone, ApplicationDbContext ctx) => ctx.Users.Any(u => u.Phone == phone);

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new AuthorizationWindow();
            var current = Application.Current.MainWindow;

            window.Show();
            Application.Current.MainWindow = window;
            current.Close();
        }
    }
}
