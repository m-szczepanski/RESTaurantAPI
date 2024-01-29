using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RESTaurantAPI.Data;
using Microsoft.EntityFrameworkCore;
using RESTaurantAPI.Models;

namespace RESTaurantAPI.Services
{
    public class EmployeeService
    {
        private readonly APIDbContext dbContext;

        public EmployeeService(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Employee>> GetAllEmployees(CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var employees = await dbContext.Employees
                .ToListAsync(cancellationToken);

            return employees == null ? throw new ApplicationException("No employees are in the database right now.") : employees;
        }

        public async Task<Employee> GetEmployeeById(int employeeId, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.Where(x => x.Id == employeeId).FirstOrDefaultAsync(cancellationToken);

            return employee == null ? throw new ApplicationException("No employee found") : employee;
        }
        
        public async Task<List<Employee>> GetEmployeesByRole(string employeeRole, CancellationToken cancellationToken)
        {
            var employees = await dbContext.Employees.Where(x => x.Role == employeeRole).ToListAsync(cancellationToken);

            return employees == null ? throw new ApplicationException("No employee with that role was found") : employees;
        }

        public async Task<List<Employee>> GetEmployeesByLastName(string lastName, CancellationToken cancellationToken)
        {
            var employees = await dbContext.Employees.Where(x => x.LastName == lastName).ToListAsync(cancellationToken);

            return employees == null ? throw new ApplicationException("No employee with that last name was found") : employees;
        }

        public async Task<Employee> AddEmployee(string firstName, string lastName, string role, string email, string phoneNumber, CancellationToken cancellationToken)
        {
            var newEmployee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Role = role,
                Email = email,
                PhoneNumber = phoneNumber,
            };

            dbContext.Employees.Add(newEmployee);
            await dbContext.SaveChangesAsync(cancellationToken);

            return newEmployee;
        }

        public async Task UpdateEmployee(int employeeId, string firstName, string lastName, string role, string email, string phoneNumber, CancellationToken cancellationToken)
        {
            Employee employee = await dbContext.Employees.FirstOrDefaultAsync(s => s.Id == employeeId, cancellationToken);

            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.Role = role;
            employee.Email = email;
            employee.PhoneNumber = phoneNumber;

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRole(int employeeId, string role, CancellationToken cancellationToken)
        {
            Employee employee = await dbContext.Employees.FirstOrDefaultAsync(s => s.Id == employeeId, cancellationToken);


            employee.Role = role;

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteEmployee(int employeeId, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId, cancellationToken);

            if (employee == null)
            {
                throw new ApplicationException("Employee with that id doesn't exists.");
            }

            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
