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

        public Product? Product
        {
            get => _product;
            set => Set(ref _product, value, nameof(Product));
        }
        public List<Message> Messages
        {
            get => _messages;
            set => Set(ref _messages, value, nameof(Messages));
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
                    .ToList();
            }
        }
    }
}
