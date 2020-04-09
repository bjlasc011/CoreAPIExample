using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IEmployeeDA
    {
        Task<IEnumerable<Employee>> GetEmployees(IEnumerable<int> userIDs);
        Task<Employee> GetEmployee(int userID);
        Task<IEnumerable<Employee>> GetAllEmployees();
    }
}
