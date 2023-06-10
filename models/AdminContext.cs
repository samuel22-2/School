using Microsoft.EntityFrameworkCore;
//using Microsoft.Win32;
using WebApplication3.models;


namespace WebApplication3.models
{
    public class AdminContext: DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options)
      : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; } = default!;  // Admin is call entity while admins is the table name
        public DbSet<Registration> Registrations { get; set; } = default!;
        public DbSet<School> Schools { get; set; } = default!;
        public DbSet<Exam> Exams { get; set; } = default!;

       


        public DbSet<AdmittedStudent> AdmittedStudents { get; set; } = default!;
        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<Department> Departments { get; set; } = default!;
        public DbSet<Faculty> Faculties { get; set; } = default!;

        public DbSet<TokenTable> TokenTables { get; set; } = default!;

        public DbSet<Hostel> Hostels { get; set; } = default!;
        public DbSet<WorshipCenter> WorshipCenters { get; set; } = default!;






    }
}
