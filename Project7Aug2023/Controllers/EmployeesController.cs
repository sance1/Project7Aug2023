using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Project7Aug2023.Data;
using Project7Aug2023.Models;
using Newtonsoft.Json;

namespace Project7Aug2023.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IRepositories _repository;

        public EmployeesController(
            IRepositories repository)
        {
            _repository = repository;
            
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Employee()
        {
            var employee = _repository.Employees();
            return Content(JsonConvert.SerializeObject(employee), "application/json");
        }

        public IActionResult GetEmployeeById(int id)
        {
            var employee = _repository.GetEmployeesById(id);
            return Content(JsonConvert.SerializeObject(employee), "application/json");
        }
        public async Task<IActionResult> AddOrUpdateEmployee(Employee obj)
        {

            var message = false;
            var employee = await _repository.AddOrUpdateEmployee(obj);
            if (employee != null) message = true;
            var data = new { employee, message};
            return Content(JsonConvert.SerializeObject(data), "application/json");
        }

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _repository.DeleteEmployee(id);
            return Content(JsonConvert.SerializeObject(employee), "application/json");
        }



    }
}

