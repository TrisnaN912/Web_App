using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using DTSMCC_Web_App.Models;
using System.Threading.Tasks;
using System.Collections;

namespace DTSMCC_Web_App.Controllers
{
    public class EmployeeController : Controller
    {

        SqlConnection sqlConnection;
        string connectionString = "Data Source=NARUTO;Initial Catalog=DTSMCC01;User ID=Admin;Password=1234567890;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IActionResult Index()
        {
            string query = "select * from Employee";

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            List<Employee> Employee = new List<Employee>();

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Employee employee = new Employee();
                            employee.EmployeeId = Convert.ToInt32(sqlDataReader[0]);
                            employee.Name = sqlDataReader[1].ToString();
                            employee.Email = sqlDataReader[2].ToString();
                            Employee.Add(employee);

                        }
                    }
                    sqlDataReader.Close();
                }

                sqlConnection.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.InnerException);
            }

            return View(Employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                try
                {
                    sqlCommand.CommandText =
                        "INSERT INTO Employee " +
                        "(EmployeeId, Name, Email) " +
                        $"VALUES ({employee.EmployeeId},'{employee.Name}','{employee.Email}')";

                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();

                    Console.WriteLine($"Data berhasil diisi");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return View();

            }
        }

        public IActionResult Edit(int Id)
        {
            string query = $"select * from Employee where EmployeeId = {Id}";

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            Employee employee = new Employee();

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            employee.EmployeeId = Convert.ToInt32(sqlDataReader[0]);
                            employee.Name = sqlDataReader[1].ToString();
                            employee.Email = sqlDataReader[2].ToString();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                try
                {
                    sqlCommand.CommandText =
                        "update Employee SET " +
                        $"Name = '{employee.Name}', " +
                        $"Email = '{employee.Email}'" +
                        $"where EmployeeId = '{employee.EmployeeId}'";


                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Console.WriteLine($"Data berhasil diedit!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return View(employee);
            }
        }

        public IActionResult Delete(int Id)
        {
            string query = $"select * from Employee where EmployeeId = {Id}";
            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            Employee employee = new Employee();
            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            employee.EmployeeId = Convert.ToInt32(sqlDataReader[0]);
                            employee.Name = sqlDataReader[1].ToString();
                            employee.Email = sqlDataReader[2].ToString();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                try
                {
                    sqlCommand.CommandText =
                        "DELETE FROM Employee " +
                        $"WHERE EmployeeId = {employee.EmployeeId}";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Console.WriteLine($"Data berhasil dihapus!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return View();
        }

    }
}
