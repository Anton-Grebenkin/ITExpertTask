using ITExpertTask.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITExpertTask.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Init();
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<RequestRespnseLog> RequestRespnseLogs { get; set; }

        private void Init()
        {
            Database.EnsureCreated();
        }
    }
}
