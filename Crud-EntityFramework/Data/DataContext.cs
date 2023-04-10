using Crud_EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_EntityFramework.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Curso> Cursos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            =>optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=Master;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True");
        
    }
}
