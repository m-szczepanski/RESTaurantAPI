using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTaurantAPI.DTOs;
using RESTaurantAPI.Services;

namespace RESTaurantAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._employeeService = employeeService;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<List<EmployeeDto>>> GetAllEmployees(CancellationToken cancellationToken,
            int? skip = null, int? limit = null)
        {
            var employees = await this._employeeService.GetAllEmployees(cancellationToken);
            var employeesDto = this._mapper.Map<List<EmployeeDto>>(employees);

            if (skip.HasValue)
            {
                employeesDto = employeesDto.Skip(skip.Value).ToList();
            }

            if (limit.HasValue)
            {
                employeesDto = employeesDto.Take(limit.Value).ToList();
            }

            return Ok(employeesDto);
        }

        [HttpGet("GetEmployeeById/{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int employeeId,
            CancellationToken cancellationToken)
        {
            var employee = await this._employeeService.GetEmployeeById(employeeId, cancellationToken);
            var employeeDto = this._mapper.Map<EmployeeDto>(employee);
            if (employeeDto == null)
            {
                return NotFound();
            }

            return Ok(employeeDto);
        }

        [HttpGet("GetEmployeesByRole/{role}")]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployeesByRole(string role, CancellationToken cancellationToken)
        {
            var employees = await this._employeeService.GetEmployeesByRole(role, cancellationToken);
            var employeesDto = this._mapper.Map<List<EmployeeDto>>(employees);

            if (employeesDto == null || employeesDto.Count == 0)
            {
                return NotFound();
            }

            return Ok(employeesDto);
        }

        [HttpGet("GetEmployeesByLastName/{lastName}")]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployeesByLastName(string lastName, CancellationToken cancellationToken)
        {
            var employees = await this._employeeService.GetEmployeesByLastName(lastName, cancellationToken);
            var employeesDto = this._mapper.Map<List<EmployeeDto>>(employees);

            if (employeesDto == null || employeesDto.Count == 0)
            {
                return NotFound();
            }

            return Ok(employeesDto);
        }

        [HttpPost("AddEmployee")]
        public async Task<ActionResult<EmployeeDto>> AddEmployee(string firstName, string lastName, string role,
            string email, string phoneNumber, CancellationToken cancellationToken)
        {
            var employee = await this._employeeService.AddEmployee(firstName, lastName, role, email, phoneNumber,
                    cancellationToken);
            var employeeDto = this._mapper.Map<EmployeeDto>(employee);

            return Ok(employeeDto);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, string firstName, string lastName, string role,
            string email, string phoneNumber,
            CancellationToken cancellationToken)
        {
            await this._employeeService.UpdateEmployee(id, firstName, lastName, role, email, phoneNumber, cancellationToken);

            return Ok("Employee updated successfully.");
        }

        [HttpPut("UpdateRole/{id}")]
        public async Task<ActionResult> UpdateRole(int id, string role, CancellationToken cancellationToken)
        {
            await this._employeeService.UpdateRole(id, role, cancellationToken);

            return Ok("Employee's role updated successfully.");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            await this._employeeService.DeleteEmployee(id, cancellationToken);

            return Ok("Employee has been deleted successfully");
        }
    }
}
