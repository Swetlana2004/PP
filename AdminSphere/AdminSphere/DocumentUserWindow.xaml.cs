using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для DocumentUserWindow.xaml
    /// </summary>
    public partial class DocumentUserWindow : Window
    {
        private DataBaseManager _dataBaseManager;
        public DocumentUserWindow()
        {
            InitializeComponent();
            _dataBaseManager = new DataBaseManager();
            LoadDocuments();
        }

        private void LoadDocuments()
        {
            DataTable documents = _dataBaseManager.GetDocuments();
            documentsGrid.ItemsSource = documents.DefaultView;
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindowToUser employeeWindowToUser = new EmployeeWindowToUser();
            this.Hide();
            employeeWindowToUser.ShowDialog();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.FileName = "Файл_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
            if (saveFileDialog.ShowDialog() == true)
            {
                string connectionString = @"Data Source=desktop-n8d6724;Initial Catalog=AdministrationDB; Integrated Security=True;";
                string query = "SELECT * FROM Document";
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

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchQuery = txtName.Text.ToLower();
            DataTable table = _dataBaseManager.GetDocuments();
            DataView dataView = table.DefaultView;
            dataView.RowFilter = "Document_name LIKE '%" + searchQuery + "%'";
            documentsGrid.ItemsSource = dataView;
        }

        private void txtType_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchQuery = txtType.Text.ToLower();
            DataTable table = _dataBaseManager.GetDocuments();
            DataView dataView = table.DefaultView;
            dataView.RowFilter = "Document_type LIKE '%" + searchQuery + "%'";
            documentsGrid.ItemsSource = dataView;
        }
    }
}
