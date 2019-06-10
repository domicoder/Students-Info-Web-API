
using Microsoft.EntityFrameworkCore;
using MODEL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PERSISTENCE
{
    public class StudentDbContext : DbContext
    {

        public DbSet<Student> Student { get; set; }

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }

    }
}
