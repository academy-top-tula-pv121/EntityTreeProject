using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTreeProject
{
    public class Catalog
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? ParentId { get; set; }
        public Catalog? Parent { get; set; }
        public List<Catalog> Children { get; set; } = new();
    }

    public class CatalogContext : DbContext
    {
        public DbSet<Catalog> Catalog { set; get; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Initial Catalog=CatalogDb;Integrated Security=True");
        }
    }
}
