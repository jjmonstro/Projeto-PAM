namespace AppXareu.Views.Usuarios;
using AppXareu.ViewModels.Usuarios;

public partial class MainView : ContentPage
{
    UsuarioViewModel usuarioViewModel;
    public MainView()
	{
		InitializeComponent();

        usuarioViewModel = new UsuarioViewModel();
        BindingContext = usuarioViewModel;
    }
}