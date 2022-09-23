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

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Name,Email")] Employee employee)
        {
            string query = "insert into (EmployeeId, Name, Email) values (@EmployeeId, @Name, @Id)";

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
                            Employee employee1 = new Employee();
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
            return View(employee);
        }

        public IActionResult Delete()
        {
            string query = "delete from Employee where EmployeeId = @EmployeeId";

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

    }
}
