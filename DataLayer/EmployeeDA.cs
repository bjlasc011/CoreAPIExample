using DataAccessLayer.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EmployeeDA : IEmployeeDA
    {
        // TODO: create local DB for demo and DA class and replace hardcoded test data.
        private Employee InitTestEmployee(int userID)
        {
            try
            {
                return new Employee()
                {
                    UserID = userID,
                    Status = EmployeeStatus.Active,
                    JobID = 8,
                    JobTitle = "Software Engineer",
                    FirstName = "Ben",
                    LastName = "Lascurain",
                    HireDate = new DateTimeOffset(),
                    EmployeeType = EmployeeType.FullTime,
                    Email = "bjlasc01@gmail.com",
                    PhoneNumber = "(502)555-6937",
                    Birthday = new DateTimeOffset(1992, 3, 14, 6, 34, 0, new TimeSpan(-7, 0, 0))
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployees(IEnumerable<int> userIDs)
        {
            // TODO: make actual data connection and return to client
            return await Task.Run(() =>
            {
                var employees = new List<Employee>();

                userIDs.Distinct().ToList().ForEach(id => employees.Add(InitTestEmployee(id)));
                
                return employees;
            });
        }

        public async Task<Employee> GetEmployee(int userID)
        {
            // TODO: get user by ID from DB
            return await Task.Run(() => InitTestEmployee(userID));
        }
    }
}
