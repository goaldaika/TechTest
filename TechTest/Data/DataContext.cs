using Microsoft.EntityFrameworkCore;
using TechTest.Model;

namespace TechTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
            
        }
        public virtual DbSet<Record> Record { get; set; }
    }
}
