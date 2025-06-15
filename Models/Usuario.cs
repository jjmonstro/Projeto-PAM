using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_PAM.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Foto { get; set; }
        
        [NotMapped]
        public string PasswordString { get; set; } = string.Empty;
        
        public string? Email { get; set; } = string.Empty;

        public Usuario[]? Seguidores { get; set; } 

        public Usuario[]? Seguindo { get; set; } 
        
    }
}