using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees(IEnumerable<int> userIDs);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployee(int userID);
    }
}