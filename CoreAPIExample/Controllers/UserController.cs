using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace CoreAPIExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IEmployeeService _employeeService;

        public UserController(
            IEmployeeService employeeService,
            ILogger<UserController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
            _logger.LogDebug($"{nameof(UserController)} initialized");
        }

        [HttpGet]
        [Route("employee/{userID}")]
        [ProducesResponseType(typeof(Employee), 200)]
        public async Task<IActionResult> GetEmployee([FromRoute] int userID)
        {
            Employee result = default;
            try
            {
                result = await _employeeService.GetEmployee(userID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetEmployee)} has failed", userID);
                throw;
            }
            return new JsonResult(result);
        }

        [HttpGet]
        [Route("employee")]
        [ProducesResponseType(typeof(IEnumerable<Employee>), 200)]
        public async Task<IActionResult> GetEmployees([FromQuery] IEnumerable<int> userIDs)
        {
            IEnumerable<Employee> result = default;
            try
            {
                result = await _employeeService.GetEmployees(userIDs.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetEmployees)} has failed", userIDs.Select(id => id.ToString() + ", "));
                throw;
            }
            return new JsonResult(result);
        }

        [HttpGet]
        [Route("employee/all")]
        [ProducesResponseType(typeof(IEnumerable<Employee>), 200)]
        public async Task<IActionResult> GetAllEmployees()
        {
            IEnumerable<Employee> result = default;
            try
            {
                result = await _employeeService.GetAllEmployees();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetAllEmployees)} has failed.");
                throw;
            }
            return new JsonResult(result);
        }
    }
}
