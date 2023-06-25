using Guschin.GraduateProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guschin.GraduateProject.ViewModels
{
    public class ConsultationViewModel : ViewModelBase
    {
        private Product? _product;
        private List<Message> _messages;
        private User _user;
        private Guid _chat;
        private string _logo = @"\Resources\picture.png";

        public Product? Product
        {
            get => _product;
            set
            {
                Set(ref _product, value, nameof(Product));
                Messages = GetMessages();
                if (value!=null)
                    Logo = value.Logo;
            }
        }
        public List<Message> Messages
        {
            get => _messages;
            set => Set(ref _messages, value, nameof(Messages));
        }
        public User User 
        { 
            get => _user;
            set => Set(ref _user, value, nameof(User));
        }
        public Guid ChatId 
        { 
            get => _chat;
            set => Set(ref _chat, value, nameof(ChatId));
        }
        public string Logo 
        { 
            get => _logo;
            set => Set(ref _logo, value, nameof(Logo));
        }

        public ConsultationViewModel()
        {
            Product = null;
            Messages = GetMessages();
        }
        public ConsultationViewModel(Product? product)
        {
            Product = product;
            Messages = GetMessages();
        }

        private List<Message> GetMessages()
        {
            using (ApplicationDbContext context = new())
            {
                return context.Messages
                    .Include(m => m.User)
                    .ThenInclude(m => m.Person)
                    .Include(m => m.Chat)
                    .Where(m => m.Chat.Product == Product)
                    .OrderBy(m => m.Date)
                    .ToList();
            }
        }
        public void UpdateList() => Messages = GetMessages();
    }
}
