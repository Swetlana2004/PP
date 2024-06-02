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
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private DataBaseManager dataBaseManager;
        public AddEmployeeWindow()
        {
            InitializeComponent();
            dataBaseManager = new DataBaseManager();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (txtFirstName.Text == null || txtLastName.Text == null || txtEmail.Text == null || txtDepartment.Text == null || txtHide_date.Text == null)
            {
                MessageBox.Show("Заполните все поля!!!");
            }
            else
            {
                string first_name = txtFirstName.Text;
                string last_name = txtLastName.Text;
                string email = txtEmail.Text;
                string department = txtDepartment.Text;
                try
                {
                    DateTime hire_date = DateTime.Parse(txtHide_date.Text);
                    dataBaseManager.AddEmployee(first_name, last_name, email, department, hire_date);
                    MessageBox.Show("Спасибо, сотрудник добавлен!!");
                    EmployeeWindow employeeWindow = new EmployeeWindow();
                    this.Hide();
                    employeeWindow.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка, проверьте корректность введенных данных!"); 
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow employeeWindow = new EmployeeWindow();
            this.Hide();
            employeeWindow.ShowDialog();
        }
    }
}
