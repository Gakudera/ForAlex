using Guschin.GraduateProject.Entities;
using Guschin.GraduateProject.ViewModels;
using Microsoft.EntityFrameworkCore;
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

        public СonsultationWindow(Product? product, User user)
        {
            InitializeComponent();
            //_viewModel = new ConsultationViewModel(product);
            //DataContext = _viewModel;
            _viewModel = (ConsultationViewModel)DataContext;
            _viewModel.Product = product;
            Title = $"{product.ProductName}";

            using (var ctx = new ApplicationDbContext())
            {
                var chatIsExist = ctx.Chats.Any(c => c.Product == product);
                var chat = Guid.Empty;
                if (chatIsExist)
                    chat = ctx.Chats.Single(c => c.Product == product).Id;
                else
                {
                    var newChat = new Chat
                    {
                        Date = DateTime.Now,
                        Status = true,
                        ProductId = product.Id,
                        Id = Guid.NewGuid()
                    };
                    ctx.Chats.Add(newChat);
                    ctx.SaveChanges();
                    chat = newChat.Id;
                }

                _viewModel.ChatId = chat;
            }

            _viewModel.User = user;
        }

        private void MessageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (isMessageEmpty(MessageTextBox.Text))
                MessageBox.Show("Поле сообщение пусто!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var message = new Message
                    {
                        Text = MessageTextBox.Text,
                        Date = DateTime.Now,
                        Id = Guid.NewGuid(),
                        ChatId = _viewModel.ChatId,
                        UserId = _viewModel.User.Id,
                    };
                    ctx.Messages.Add(message);
                    ctx.SaveChanges();
                }
                _viewModel.UpdateList();
            }    
        }
        private bool isMessageEmpty(string message) => string.IsNullOrEmpty(message);
    }
}
