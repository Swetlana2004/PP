using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        private DataBaseManager databaseManager;
        public EmployeeWindow()
        {
            InitializeComponent();
            databaseManager = new DataBaseManager();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            DataTable employees = databaseManager.GetEmployees();
            employeesGrid.ItemsSource = employees.DefaultView;
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            this.Hide();
            addEmployeeWindow.ShowDialog();
        }

        private void txtSeartch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchQuery = txtSeartch.Text.ToLower();
            DataTable table = databaseManager.GetEmployees();
            DataView dataView = table.DefaultView;
            dataView.RowFilter = "First_name LIKE '%" + searchQuery + "%'";
            employeesGrid.ItemsSource = dataView;
        }

        private void txtfamily_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchQuery = txtfamily.Text.ToLower();
            DataTable table = databaseManager.GetEmployees();
            DataView dataView = table.DefaultView;
            dataView.RowFilter = "Last_name LIKE '%" + searchQuery + "%'";
            employeesGrid.ItemsSource = dataView;
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchQuery = txtEmail.Text.ToLower();
            DataTable table = databaseManager.GetEmployees();
            DataView dataView = table.DefaultView;
            dataView.RowFilter = "Email LIKE '%" + searchQuery + "%'";
            employeesGrid.ItemsSource = dataView;
        }

        private void txtDepartment_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchQuery = txtDepartment.Text.ToLower();
            DataTable table = databaseManager.GetEmployees();
            DataView dataView = table.DefaultView;
            dataView.RowFilter = "Department LIKE '%" + searchQuery + "%'";
            employeesGrid.ItemsSource = dataView;
        }

        private void btnDocuments_Click(object sender, RoutedEventArgs e)
        {
            DocumentWindow documentWindow = new DocumentWindow();
            this.Hide();
            documentWindow.ShowDialog();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.FileName = "Файл_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
            if (saveFileDialog.ShowDialog() == true)
            {
                string connectionString = @"Data Source=desktop-n8d6724;Initial Catalog=AdministrationDB; Integrated Security=True;";
                string query = "SELECT * FROM Employees";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            writer.Write(reader.GetName(i));
                            if (i < reader.FieldCount - 1)
                                writer.Write(",");
                        }
                        writer.WriteLine();
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                writer.Write(reader.GetValue(i));
                                if (i < reader.FieldCount - 1)
                                    writer.Write(",");
                            }
                            writer.WriteLine();
                        }
                    }
                    connection.Close();
                }
                MessageBox.Show("Файл сохранен " + saveFileDialog.FileName);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Hide();
            mainWindow.ShowDialog();
        }
    }
}
