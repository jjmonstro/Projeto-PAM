using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projeto_PAM.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Projeto_PAM.Utils;

namespace Projeto_PAM.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

         public DbSet<Usuario> TB_USUARIOS {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("TB_USUARIOS");
          

            modelBuilder.Entity<Usuario>();

            Usuario user = new Usuario();
            Criptografia.CriarPasswordHash("123456", out byte[] hash, out byte[] salt);
            user.Id = 1;
            user.Username = "UsuarioAdmin";
            user.PasswordString = string.Empty;
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.Foto = "https://thvnext.bing.com/th/id/OIP.fU7XmhYQvxJs89FnvKwgigHaEk?cb=thvnext&rs=1&pid=ImgDetMain";
            user.Email = "seuEmail@example.com";
            user.Seguidores = new Usuario[] { };
            user.Seguindo = new Usuario[] { };
           
            modelBuilder.Entity<Usuario>().HasData(user);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("Varchar").HaveMaxLength(200);
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
              warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

    }
}