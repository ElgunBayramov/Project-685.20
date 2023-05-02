using Microsoft.EntityFrameworkCore;
using Project.WebUI.Models.Entities;

namespace Project.WebUI.Models.DataContexts
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Permission> Permissions { get; set; }




    }
}
