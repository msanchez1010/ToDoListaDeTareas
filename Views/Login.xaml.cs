using System;
using ToDoListaDeTareas.Views;
using Microsoft.Maui.Controls;

namespace ToDoListaDeTareas.Views;

public partial class Login : ContentPage
{
    private UsuarioRepository usuarioRepository;
    

    // Inyecta UsuarioRepository a trav�s del constructor
    public Login(UsuarioRepository usuarioRepository)
    {
        InitializeComponent();
        this.usuarioRepository = usuarioRepository;
        

        txtCorreo.TextChanged += Entry_TextChanged;
        txtContrase�a.TextChanged += Entry_TextChanged;

        btnInicioSesion.IsEnabled = false;
        btnInicioSesion.BackgroundColor = Color.FromHex("#808080");  // Gris
        btnInicioSesion.TextColor = Color.FromHex("#FFFFFF");  // Blanco
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Habilitar el bot�n si ambos campos tienen texto, de lo contrario, deshabilitar
        bool isButtonEnabled = !string.IsNullOrWhiteSpace(txtCorreo.Text) && !string.IsNullOrWhiteSpace(txtContrase�a.Text);

        btnInicioSesion.IsEnabled = isButtonEnabled;
        btnInicioSesion.BackgroundColor = isButtonEnabled ? Color.FromHex("#3498db") : Color.FromHex("#808080");
    }

    private void btnInicioSesion_Clicked(object sender, EventArgs e)
    {
        string correo = txtCorreo.Text;
        string password = txtContrase�a.Text;

        if (usuarioRepository.AuthenticateUsuario(correo, password))
        {
            // Usuario autenticado, redirigir a la p�gina de Tareas
            Navigation.PushAsync(new Views.VistaTareas());
        }
        else
        {
            // Mostrar mensaje de error
            DisplayAlert("Error", "Correo o contrase�a incorrectos", "OK");
        }
    }

    private async void btnRegistro_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.Registro(usuarioRepository));
    }
}