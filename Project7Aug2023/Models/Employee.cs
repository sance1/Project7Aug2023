using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project7Aug2023.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }
}

