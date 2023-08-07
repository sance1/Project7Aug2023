using Microsoft.EntityFrameworkCore;
using Project7Aug2023.Models;
using System;
using System.Collections.Generic;

namespace Project7Aug2023.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Employee> Employee { get; set; }
    }
}

