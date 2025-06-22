using AppXareu.ViewModels.Usuarios;

namespace AppXareu.Views.Usuarios;

public partial class CadastroView : ContentPage
{
    UsuarioViewModel usuarioViewModel;
    public CadastroView()
	{
        InitializeComponent();

        usuarioViewModel = new UsuarioViewModel();
        BindingContext = usuarioViewModel;
    }    
   

}