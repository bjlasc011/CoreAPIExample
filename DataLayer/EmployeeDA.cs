using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EmployeeDA : BaseDA<Employee>, IEmployeeDA
    {
        public EmployeeDA(IOptions<ConnectionStrings> connectionStrings) : base(connectionStrings.Value.EmployeeDb, "EmployeeDb")
        {
            
        }

        public async Task<IEnumerable<Employee>> GetEmployees(IEnumerable<int> userIDs)
        {
            var result = await GetListAsync();
            
            return result.Where(e => userIDs.Contains(e.UserID));
        }

        public async Task<Employee> GetEmployee(int userID)
        {
            var result = await GetListAsync(userID);
            
            return result.FirstOrDefault();
        }
    }
}
