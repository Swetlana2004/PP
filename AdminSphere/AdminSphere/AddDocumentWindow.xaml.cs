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

namespace AdminSphere
{
    /// <summary>
    /// Логика взаимодействия для AddDocumentWindow.xaml
    /// </summary>
    public partial class AddDocumentWindow : Window
    {
        private DataBaseManager dataBaseManager;
        public AddDocumentWindow()
        {
            InitializeComponent();
            dataBaseManager = new DataBaseManager();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            DocumentWindow docWindow = new DocumentWindow();
            this.Close();
            docWindow.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == null || txtType.Text == null || txtDate.Text == null)
            {
                MessageBox.Show("Заполните все поля!!!");
            }
            else
            {
                string document_name = txtName.Text;
                string document_type = txtType.Text;
                try
                {
                    DateTime document_date = DateTime.Parse(txtDate.Text);
                    dataBaseManager.AddDocument(document_name, document_type, document_date);
                    MessageBox.Show("Спасибо, документ добавлен!!");
                    Close();
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка, проверьте корректность введенных данных!");
                }
            }
            DocumentWindow docWindow = new DocumentWindow();
            this.Close();
            docWindow.ShowDialog();
        }
    }
}
