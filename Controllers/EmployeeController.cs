using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTaurantAPI.DTOs;
using RESTaurantAPI.Services;

namespace RESTaurantAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly EmployeeService employeeService;

        public EmployeeController(EmployeeService employeeService, IMapper _mapper)
        {
            this._mapper = _mapper;
            this.employeeService = employeeService;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<List<EmployeeDto>>> GetAllEmployees(CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var employees = await this.employeeService.GetAllEmployees(cancellationToken);
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
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int employeeId, CancellationToken cancellationToken)
        {
            var employee = await employeeService.GetEmployeeById(employeeId, cancellationToken);
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
            var employee = await employeeService.GetEmployeesByRole(role, cancellationToken);
            var employeeDto = this._mapper.Map<EmployeeDto>(employee);
            if (employeeDto == null)
            {
                return NotFound();
            }

            return Ok(employeeDto);
        }

        [HttpGet("GetEmployeesByLastName/{lastName}")]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployeesByLastName(string lastName, CancellationToken cancellationToken)
        {
            var employee = await employeeService.GetEmployeesByLastName(lastName, cancellationToken);
            var employeeDto = this._mapper.Map<EmployeeDto>(employee);
            if (employeeDto == null)
            {
                return NotFound();
            }

            return Ok(employeeDto);
        }


    }
}
