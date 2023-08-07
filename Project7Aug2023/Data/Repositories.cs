using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project7Aug2023.Models;

namespace Project7Aug2023.Data
{
    public class Repositories : IRepositories
    {
        private readonly IConfiguration _config;
        private readonly DataContext _context;

        public Repositories(
            DataContext context,
            IConfiguration configuration)
        {
            _config = configuration;
            _context = context;
        }

        public List<Employee> Employees()
        {
            try
            {
                var data = _context.Employee
                           .OrderBy(x => x.Name)
                           .ToList() ?? new List<Employee>();

                return data;
            }
            catch (Exception e)
            {
                throw e;
            }
            

        }

        public Employee GetEmployeesById(int id)
        {
            var data = _context.Employee.FirstOrDefault(x => x.Id == id) ?? new Employee();
            return data;
        }

        public async Task<Employee> AddOrUpdateEmployee(Employee obj)
        {
            try
            {
                var data = await _context.Employee.FirstOrDefaultAsync(x => x.Id == obj.Id) ?? new Employee();
                data.Name = obj.Name;
                data.Department = obj.Department;

                if(data.Id == 0) _context.Employee.Add(data);

                await _context.SaveChangesAsync();
                return data;
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        public async Task<bool> DeleteEmployee(int Id)
        {
            var data = await _context.Employee.FirstOrDefaultAsync(x => x.Id == Id) ?? new Employee();
            _context.Employee.Remove(data);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}

