namespace AppXareu.Views.Usuarios;
using AppXareu.ViewModels.Usuarios;
public partial class EditarView : ContentPage
{
    UsuarioViewModel usuarioViewModel;
    public EditarView()
    {
        
        InitializeComponent();

        usuarioViewModel = new UsuarioViewModel();
        BindingContext = usuarioViewModel;
    }
}
