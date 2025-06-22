using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppXareu.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordString { get; set; } = string.Empty;
        
        public string Foto { get; set; }
        public string Email { get; set; } = string.Empty;
        

        public Usuario[] Seguidores { get; set; } = Array.Empty<Usuario>();
        public Usuario[] Seguindo { get; set; } = Array.Empty<Usuario>();

    }
}
