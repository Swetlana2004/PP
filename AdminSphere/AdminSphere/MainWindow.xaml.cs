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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminSphere
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AvtorizCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = (Storyboard)this.FindResource("RedBorderAnimation");
            storyboard.Begin(txtUserLogin);
            storyboard.Begin(txtUserPassword);
            var databaseManager = new DataBaseManager();
            var user = databaseManager.AuthenticateUser(txtUserLogin.Text, txtUserPassword.Text);
            if (user != null)
            {
                Users currentUser = databaseManager.AuthenticateUser(txtUserLogin.Text, txtUserPassword.Text);
                if (currentUser != null)
                {
                    if (currentUser.Status == "Admin")
                    {
                        EmployeeWindow employeeWindow = new EmployeeWindow();
                        Application.Current.MainWindow = employeeWindow;
                        this.Close();
                        employeeWindow.Show();
                    }
                    else
                    {
                        EmployeeWindowToUser employeesToUser = new EmployeeWindowToUser();
                        Application.Current.MainWindow = employeesToUser;
                        this.Close();
                        employeesToUser.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Неверное имя пользователя или пароль.");
                }
                
            }
            else
            {
                MessageBox.Show("Проверьте данные и попробуйте снова!");
            }
        }

        private void RegisterCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterUsersWindow secondWindow = new RegisterUsersWindow();
            this.Close();
            secondWindow.ShowDialog();
        }

        private void txtUserPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (chkShowPassword.IsChecked == true)
            {
                string text = txtUserPassword.Text;
                text = text.ToUpper();
                txtUserPassword.Text = text;
            }
        }
        private void chkUsePassword_Checked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Visibility = Visibility.Collapsed;
            txtUserPassword.Text = PasswordBox.Password;
            txtUserPassword.Visibility = Visibility.Visible;
        }
        private void chkUsePassword_Unchecked(object sender, RoutedEventArgs e)
        {
            txtUserPassword.Visibility = Visibility.Collapsed;
            PasswordBox.Visibility = Visibility.Visible; PasswordBox.Password = txtUserPassword.Text;
        }
    }
}
