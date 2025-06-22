using AppXareu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppXareu.Services.Usuarios
{
    public class UsuarioService : Request
    {
        private readonly Request _request;
        private const string _apiUrlBase = "http://localhost:5135/Usuarios";
        
        public UsuarioService()
        {
            _request = new Request();
        }

        private string _token = string.Empty;

        public UsuarioService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<Usuario> PostRegistrarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Registrar";
            u.Id = await _request.PostReturnIntAsync(_apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }

        public async Task<Usuario> PostAutenticarUsuarioAsync(Usuario u)
        {
            try
            {
                string urlComplementar = "/Autenticar";
                u = await _request.PostAsync(_apiUrlBase+ urlComplementar, u, string.Empty);
                System.Diagnostics.Debug.WriteLine(u);
                return u;
            }catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Informação"+
                        ex.Message + " Detalhes: " + ex.InnerException, "Ok");
                return u;
            }
            
        }

        public async Task<Usuario> PutAlterarSenhaAsync(Usuario u)
        {
            string urlComplementar = "/AlterarSenha";
            u = await _request.PutAsync(_apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }

        public async Task<Usuario> PutSeguirAsync(Usuario u, int idSeguidor, int idSeguido)
        {
            string urlComplementar = "/Seguir/"+idSeguidor+"/"+idSeguido;
            u = await _request.PostAsync(_apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }

        public async Task<Usuario> DeleteDeletarAsync(Usuario u)
        {
            try
            {
                string urlComplementar = "/Deletar/" + u.Id;
                await _request.DeleteAsync<string>(_apiUrlBase + urlComplementar, u, string.Empty);
                return u;
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
                return u;
            }
            
        }
    }
}
