using System;
using ToDoListaDeTareas.Views;
using Microsoft.Maui.Controls;

namespace ToDoListaDeTareas.Views;

public partial class Login : ContentPage
{
    private UsuarioRepository usuarioRepository;


    // Inyecta UsuarioRepository a través del constructor
    public Login(UsuarioRepository usuarioRepository)
    {
        InitializeComponent();
        this.usuarioRepository = usuarioRepository;
       


        txtCorreo.TextChanged += Entry_TextChanged;
        txtContraseña.TextChanged += Entry_TextChanged;

        btnInicioSesion.IsEnabled = false;
        btnInicioSesion.BackgroundColor = Color.FromHex("#808080");  // Gris
        btnInicioSesion.TextColor = Color.FromHex("#FFFFFF");  // Blanco
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Habilitar el botón si ambos campos tienen texto, de lo contrario, deshabilitar
        bool isButtonEnabled = !string.IsNullOrWhiteSpace(txtCorreo.Text) && !string.IsNullOrWhiteSpace(txtContraseña.Text);

        btnInicioSesion.IsEnabled = isButtonEnabled;
        btnInicioSesion.BackgroundColor = isButtonEnabled ? Color.FromHex("#3498db") : Color.FromHex("#808080");
    }

    private void btnInicioSesion_Clicked(object sender, EventArgs e)
    {
        string correo = txtCorreo.Text;
        string password = txtContraseña.Text;

        if (usuarioRepository.AuthenticateUsuario(correo, password))
        {
            TareaRepository tareaRepository = ObtenerTareaRepositoryDeUsuario(usuarioRepository);
            Navigation.PushAsync(new Views.VistaTareas(tareaRepository));
        }
        else
        {
            // Mostrar mensaje de error
            DisplayAlert("Error", "Correo o contraseña incorrectos", "OK");
        }
    }

    private TareaRepository ObtenerTareaRepositoryDeUsuario(UsuarioRepository usuarioRepository)
    {
        // Implementa la lógica para obtener o crear un TareaRepository en base al usuario actual
        // Puedes retornar la instancia existente o crear una nueva según tus necesidades
        // Aquí un ejemplo simple
        return new TareaRepository(usuarioRepository.GetDbPath());
    }

    private async void btnRegistro_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.Registro(usuarioRepository));
    }
}