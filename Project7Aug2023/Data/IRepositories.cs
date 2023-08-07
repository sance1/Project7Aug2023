using System;
using Project7Aug2023.Models;

namespace Project7Aug2023.Data
{
    public interface IRepositories
    {
        List<Employee> Employees();
        Employee GetEmployeesById(int id);
        Task<Employee> AddOrUpdateEmployee(Employee obj);
        Task<bool> DeleteEmployee(int Id);
    }
}

