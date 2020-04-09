using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDA _employeeDA;
        public EmployeeService(IEmployeeDA employeeDA)
        {
            _employeeDA = employeeDA;
        }

        public async Task<IEnumerable<Employee>> GetEmployees(IEnumerable<int> userIDs = null)
        {
            var result = await _employeeDA.GetEmployees(userIDs);

            return result;
        }

        public async Task<Employee> GetEmployee(int userID)
        {
            var result = await _employeeDA.GetEmployee(userID);

            return result;
        }

    }
}
