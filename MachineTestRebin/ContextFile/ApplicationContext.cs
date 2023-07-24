using MachineTestRebin.Models;
using Microsoft.EntityFrameworkCore;

namespace MachineTestRebin.ContextFile
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
    }
}
