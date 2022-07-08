using DevIO.UI.Site.Models;
using Microsoft.EntityFrameworkCore;

namespace DevIO.UI.Site.Data
{
    public class MeuDbContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }

        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options)
        {
            
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
