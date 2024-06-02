using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для RegisterUsersWindow.xaml
    /// </summary>
    public partial class RegisterUsersWindow : Window
    {
        public RegisterUsersWindow()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow avtorizWindow = new MainWindow();
            this.Close();
            avtorizWindow.ShowDialog();
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userNameTextBox.Text) || string.IsNullOrWhiteSpace(userTelephonTextBox.Text) || string.IsNullOrWhiteSpace(roleComboBox.Text))
            {
                MessageBox.Show("Заполните все поля для регистрации!!");
            }
            else
            {
                if (DataBaseManager.Registration(userNameTextBox.Text, userTelephonTextBox.Text, roleComboBox.Text))
                {
                    MessageBox.Show("Регистрация выполнена успешно!");
                    MainWindow avtorizWindow = new MainWindow();
                    this.Close();
                    avtorizWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Сервер не доступен, попробуйте позже!");
                }

            }
        }
    }
}
