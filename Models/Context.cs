using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
        :base(options) {}

        public DbSet<Project> Projects {get; set;}
        public DbSet<TodoItem> TodoItems {get; set;}
    }
}    