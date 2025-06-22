using AppXareu.Models;
using AppXareu.Services.Usuarios;
using AppXareu.Views.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppXareu.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {
        private UsuarioService _uService;
        public ICommand AutenticarCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand DirecionarCadastroCommand { get; set; }

        public ICommand EditarCommand { get; set; }
        public ICommand VaiParaEditCommand { get; set; }

        public ICommand DeletarCommand { get; set; }
        public UsuarioViewModel()
        {
            _uService = new UsuarioService();
            InicializarCommands();
        }

        public void InicializarCommands()
        {
            AutenticarCommand = new Command(async () => await AutenticarUsuario());
            RegistrarCommand = new Command(async () => await RegistrarUsuario());
            DirecionarCadastroCommand = new Command(async () => await DirecionarParaCadastro());
            VaiParaEditCommand = new Command(async () => await VaiParaEdit());
            EditarCommand = new Command(async () => await Editar());
            DeletarCommand = new Command(async () => await Deletar());
        }



        #region AtributosPropriedades
        private string login = string.Empty;
        private string senha = string.Empty;
        private string newSenha = string.Empty;
        private int idvm = 0;


        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }
        public string Senha
        {
            get => senha;
            set
            {
                senha = value;
                OnPropertyChanged();
            }
        }
        public string NewSenha
        {
            get => newSenha;
            set
            {
                newSenha = value;
                OnPropertyChanged();
            }
        }

        public int Idvm
        {
            get => idvm;
            set
            {
                idvm = value;
                OnPropertyChanged();
            }
        }

        
        #endregion

        #region Metodos

        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;
        public async Task AutenticarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                System.Diagnostics.Debug.WriteLine($"Senhaaaaaaaaaaaaaaaaaaaaaaaa: {Senha}");
                u.Username = Login;
                Login = Login;
                u.PasswordString = Senha;
                Usuario uAutenticado = await _uService.PostAutenticarUsuarioAsync(u);
                System.Diagnostics.Debug.WriteLine($"Loginnnnnnnnnnnnnnn: {Login}");

                if (uAutenticado.Id != 0)
                {
                    string mensagem = $"Bem vindo {u.Username}";
                    
                    Preferences.Set("UsuarioId", uAutenticado.Id);
                    Preferences.Set("UsuarioUsername", uAutenticado.Username);
                    

                    await Application.Current.MainPage
                        .DisplayAlert("Informação", mensagem, "Ok");

                    Application.Current.MainPage = new MainView();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("usuarioOOOOOOOOOOOOOOOOOOOOOOO "+u.Id+"  nome: "+u.Username+"uuuuuuuuu "+u.PasswordString);
                   await Application.Current.MainPage
                        .DisplayAlert("Informação", "Dados incorretos :(", "Ok");
                }
            }
            catch (Exception ex)
            {
                
                await Application.Current.MainPage.DisplayAlert("Informação",
                        ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task RegistrarUsuario()//Método para registrar um usuário     
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = Login;
                u.PasswordString = Senha;

                Usuario uRegistrado = await _uService.PostRegistrarUsuarioAsync(u);

                if (uRegistrado.Id != 0)
                {
                    string mensagem = $"Usuário Id {uRegistrado.Id} registrado com sucesso.";
                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                     Application.Current.MainPage = new LoginView();
                    Preferences.Set("UsuarioPerfil", mensagem, "Ok");
                    Application.Current.MainPage = new LoginView();

                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task DirecionarParaCadastro()//Método para exibição da view de Cadastro      
        {
            try
            {
                Application.Current.MainPage = new CadastroView();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task Editar() //método para editar senha do usuario
        {
            try
            {
                
                Usuario u = new Usuario();
                u.Id = Preferences.Get("UsuarioId", 0);
                u.Username = Login;
                u.PasswordString = Senha;
                
                Usuario uEditado = await _uService.PutAlterarSenhaAsync(u);
             
                if (uEditado.Id != 0)
                {
                    string mensagem = $"Usuário Id {uEditado.Id} editado com sucesso.";
                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");
                     
                    Preferences.Set("UsuarioPerfil ", mensagem, "Ok");
                    
                }
                Application.Current.MainPage = new MainView();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private async Task Deletar()//método para deletar o usuario
        {
            try
            {
                Usuario u = new Usuario();
                u.Id = Preferences.Get("UsuarioId", 0);
                System.Diagnostics.Debug.WriteLine($"foi Ate aquiiiiiii111{u.Id}");
                await _uService.DeleteDeletarAsync(u);
                System.Diagnostics.Debug.WriteLine($"foi Ate aquiiiiiii2222");
                await Application.Current.MainPage.DisplayAlert("Já era", "Usuário deletado", "Ok");
                Application.Current.MainPage = new LoginView();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private async Task VaiParaEdit()
        {
            Application.Current.MainPage = new EditarView();
        }
        //android:icon="@mipmap/appicon" android:roundIcon="@mipmap/appicon_round"



        #endregion

    }
}
