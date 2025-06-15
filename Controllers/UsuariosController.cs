using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_PAM.Data;
using Projeto_PAM.Models;
using Projeto_PAM.Utils;


namespace Projeto_PAM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;
        
        public UsuariosController(DataContext context)
        {
            _context = context;
        }

        private async Task<bool> UsuarioExistente(string username)
        {
            if(await _context.TB_USUARIOS.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            
            return false;
        }

        [HttpPost("Registrar")] 
        public async Task<IActionResult> RegistrarUsuario (Usuario user) 
        { 
            try 
            { 
                if (await UsuarioExistente(user.Username)) 
                throw new System.Exception("Nome de usuário já existe"); 

                Criptografia.CriarPasswordHash (user.PasswordString, out byte[] hash, out byte[] salt); 
                user.PasswordString = string.Empty; 
                user.PasswordHash = hash; 
                user.PasswordSalt = salt; 
                await _context.TB_USUARIOS.AddAsync (user); 
                await _context.SaveChangesAsync(); 
                return Ok (user.Id); 
            } 
            catch (System.Exception ex) 
            { 
                return BadRequest(ex.Message); 
            }
        }

        // [HttpPost("Autenticar")] 
        // public async Task<IActionResult> AutenticarUsuario(Usuario credenciais) 
        // { 
        //     try 
        //     { 
        //         Usuario? usuario = await _context.TB_USUARIOS.FirstOrDefaultAsync(x => x. Username. ToLower().Equals(credenciais.Username.ToLower())); 
                
        //         if (usuario == null) 
        //         { 
        //             throw new System.Exception("Usuário não encontrado."); 
        //         } 
                
        //         else if (!Criptografia.VerificarPasswordHash(credenciais.PasswordString, usuario.PasswordHash, usuario.PasswordSalt)) 
        //         { 
        //             throw new System.Exception("Senha incorreta."); 
        //         } 
        //         else 
        //         { 
                    
        //             _context.TB_USUARIOS.Update(usuario);
        //             await _context.SaveChangesAsync();
        //             return Ok(usuario);
        //         } 
        //     } 
        //     catch (System.Exception ex) 
        //     { 
        //         return BadRequest(ex.Message); 
        //     } 
        // }

        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha(Usuario usuario)
        {
            try
            {
                Usuario? usuarioAchado = await _context.TB_USUARIOS.FirstOrDefaultAsync(u => u.Id == usuario.Id);
                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                Criptografia.CriarPasswordHash(usuario.PasswordString, out byte[] hash, out byte[] salt);
                usuario.PasswordHash = hash;
                usuario.PasswordSalt = salt;

                _context.TB_USUARIOS.Update(usuario);
                await _context.SaveChangesAsync();

                return Ok("Senha alterada com sucesso.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                List<Usuario> usuarios = await _context.TB_USUARIOS.ToListAsync();
                if (usuarios == null || usuarios.Count == 0)
                {
                    return NotFound("Nenhum.");
                }
                return Ok(usuarios);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message+ " - " + ex.InnerException);
            }
        }

        [HttpPut("Seguir/{idSeguidor}/{idSeguido}")]
        public async Task<IActionResult> Seguir(int idSeguidor, int idSeguido)
        {
            try
            {
                //|esse é o cara que vai ser seguido
                Usuario? usuarioSeguido = await _context.TB_USUARIOS.FirstOrDefaultAsync(x => x.Id == idSeguido);
                if (usuarioSeguido == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                Usuario? usuarioSeguidor = await _context.TB_USUARIOS.FirstOrDefaultAsync(x => x.Id == idSeguidor);
                if (usuarioSeguidor == null)
                {
                    return NotFound("Usuário seguidor não encontrado.");
                }

                if(usuarioSeguidor.Seguindo?.Any(x => x.Id == idSeguido) == true)
                {
                    return BadRequest("Você já está seguindo este usuário.");
                }
                
                usuarioSeguidor.Seguindo?.Append(usuarioSeguido);
                usuarioSeguido.Seguidores?.Append(usuarioSeguidor);

                return Ok("Usuário seguido com sucesso.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
