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
    /// Interaction logic for FAQEditWindow.xaml
    /// </summary>
    public partial class FAQEditWindow : Window
    {
        private Guid _id;
        private bool _isEdit = false;

        public FAQEditWindow(Faq? item)
        {
            InitializeComponent();

            if(item == null)
            {
                Title = "Добавить ответ на вопрос";
                _isEdit = false;
            }
            else
            {
                Title = "Редактировать ответ на вопрос";
                _isEdit = true;
                _id = item.Id;
                DateTB.Text = item.Date.ToShortDateString();
                QuestionTB.Text = item.Question;
                AnswerTB.Text = item.Answer;
            }
        }

        private void Add_Save_Click(object sender, RoutedEventArgs e)
        {
            Faq item = new Faq();

            item.Id = _id;
            item.Question = QuestionTB.Text;
            item.Answer = AnswerTB.Text;
            item.Date = Convert.ToDateTime(DateTB.Text);

            using (var db = new ApplicationDbContext())
            {
                if (_isEdit == true)
                {
                    var faq = db.Faqs.SingleOrDefault(f => f.Id == item.Id);

                    if (faq == null)
                        return;

                    faq.Date = item.Date;
                    faq.Question = item.Question;
                    faq.Answer = item.Answer;

                    db.Faqs.Update(faq);
                }
                else
                {
                    item.Id = Guid.NewGuid();
                    db.Faqs.Add(item);
                }

                db.SaveChanges();
            }

            this.Close();
        }
    }
}
