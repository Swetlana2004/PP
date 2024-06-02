using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdminSphere
{
    public class DataBaseManager
    {
        private const string _connectionString = @"Data Source=desktop-n8d6724; Initial Catalog=AdministrationDB; Integrated Security=True;";
        private static SqlConnection connectionSql = new SqlConnection(_connectionString);
        public Users AuthenticateUser(string userName, string phone)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Users WHERE UserName = @UserName AND Phone = @Phone", connection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Phone", phone);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Users
                    {
                        UsersID = (int)reader["UsersID"],
                        UserName = (string)reader["UserName"],
                        Phone = (string)reader["Phone"],
                        Status = (string)reader["Status"]
                    };
                }
                return null;
            }

        }
        public static bool Registration(string userName, string phone, string status)
        {
            connectionSql.Open();
            string selectNamePass = $"select UserName from Users where UserName='{userName}';";
            string insertNewUser = $"insert into Users values ('{userName}','{phone}','{status}');";
            SqlCommand command = new SqlCommand(selectNamePass, connectionSql);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    connectionSql.Close();
                    return false;
                }
            }
            try
            {
                new SqlCommand(insertNewUser, connectionSql).ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Не удалось зарегистрироваться, попробуйте позже!");
            }
            connectionSql.Close();
            return true;
        }

        public DataTable GetEmployees()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Employees", connection);
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                connection.Close();
            }
            return dt;
        }

        public void AddEmployee(string first_name, string last_name, string email, string department, DateTime hire_date)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Employees (First_name, Last_name, Email, Department, Hire_date) " +
                    "VALUES (@First_name, @Last_name, @Email, @Department, @Hire_date)", connection);
                command.Parameters.AddWithValue("@First_name", first_name);
                command.Parameters.AddWithValue("@Last_name", last_name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Department", department);
                command.Parameters.AddWithValue("@Hire_date", hire_date);
                command.ExecuteNonQuery();
            }
        }

        public DataTable GetDocuments()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Document", connection);
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                connection.Close();
            }
            return dt;
        }

        public void AddDocument(string document_name, string document_type, DateTime document_date)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Document (Document_name, Document_type, Document_date) " +
                    "VALUES (@Document_name, @Document_type, @Document_date)", connection);
                command.Parameters.AddWithValue("@Document_name", document_name);
                command.Parameters.AddWithValue("@Document_type", document_type);
                command.Parameters.AddWithValue("@Document_date", document_date);
                command.ExecuteNonQuery();
            }
        }
    }
}
