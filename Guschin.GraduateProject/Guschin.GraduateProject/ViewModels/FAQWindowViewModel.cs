using Guschin.GraduateProject.Commands;
using Guschin.GraduateProject.Entities;
using Guschin.GraduateProject.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Guschin.GraduateProject.ViewModels
{
    public class FAQWindowViewModel : ViewModelBase
    {
        private List<Faq> _list;
        private int _index = -1;

        public List<Faq> List
        {
            get => _list;
            set => Set(ref _list, value, nameof(List));
        }
        public int Index
        {
            get => _index;
            set => Set(ref _index, value, nameof(Index));
        }

        public ICommand AddAnswer => new Command(a =>
        {
            new FAQEditWindow(null).ShowDialog();
            GetList();
        });
        public ICommand EditAnswer => new Command(a =>
        {
            if (Index == -1)
            {
                MessageBox.Show("Выберите вопрос для редактирования.");
                return;
            }

            new FAQEditWindow(_list[Index]).ShowDialog();
            GetList();
        });
        public ICommand DeleteAnswer => new Command(a =>
        {
            if (Index == -1)
            {
                MessageBox.Show("Выберите вопрос для удаления.");
                return;
            }

            Delete(_list[Index]);
            GetList();
        });

        public FAQWindowViewModel()
        {
            GetList();
        }

        private void GetList()
        {
            using(var db = new ApplicationDbContext())
            {
                List = db.Faqs.ToList();
            }
        }

        private void Delete(Faq? item)
        {
            if (item == null)
                return;

            using (var db = new ApplicationDbContext())
            {
                var faq = db.Faqs.SingleOrDefault(f => f.Id == item.Id);

                if (faq == null)
                    return;

                db.Faqs.Remove(faq);
                db.SaveChanges();
            }
        }
    }
}
