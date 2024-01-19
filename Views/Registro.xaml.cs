using System;
using ToDoListaDeTareas.Views;
using Microsoft.Maui.Controls;

namespace ToDoListaDeTareas.Views;

public partial class Registro : ContentPage
{
    private UsuarioRepository usuarioRepository;
    public Registro(UsuarioRepository usuarioRepository)
    {
        InitializeComponent();
        this.usuarioRepository = usuarioRepository;

        txtNombre.TextChanged += Entry_TextChanged;
        txtCorreo.TextChanged += Entry_TextChanged;
        txtPassword.TextChanged += txtPassword_TextChanged;

        btnRegistrarse.IsEnabled = false;
    }


    // Habilitar el botón de registrarse si todos los campos están llenos
    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        btnRegistrarse.IsEnabled = !string.IsNullOrWhiteSpace(txtNombre.Text) &&
                                   !string.IsNullOrWhiteSpace(txtCorreo.Text) &&
                                   !string.IsNullOrWhiteSpace(txtPassword.Text);
    }




    private void txtCorreo_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Validar el campo correo
        string correo = txtCorreo.Text;
        bool isCorreoValido = !string.IsNullOrWhiteSpace(correo) && EsCorreoValido(correo);

        // Puedes mostrar un mensaje de error si el correo no es válido
        if (!isCorreoValido)
        {
            lblCorreoMensaje.Text = "Correo no válido";
            btnRegistrarse.IsEnabled = false;
        }
        else
        {
            lblCorreoMensaje.Text = "";
            btnRegistrarse.IsEnabled = !string.IsNullOrWhiteSpace(txtNombre.Text) &&
                                       !string.IsNullOrWhiteSpace(txtPassword.Text) &&
                                       isCorreoValido;
        }
    }



    private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Validar que la contraseña tenga al menos 6 caracteres
        string password = txtPassword.Text;
        bool isPasswordValid = !string.IsNullOrWhiteSpace(password) && password.Length >= 6;

        // Puedes mostrar un mensaje de error si la contraseña no es válida
        if (!isPasswordValid)
        {
            lblContraseñaMensaje.Text = "Contraseña debe tener al menos 6 caracteres";
            btnRegistrarse.IsEnabled = false;
        }
        else
        {
            lblContraseñaMensaje.Text = "";
            btnRegistrarse.IsEnabled = !string.IsNullOrWhiteSpace(txtNombre.Text) &&
                                       !string.IsNullOrWhiteSpace(txtCorreo.Text) &&
                                       isPasswordValid; // Agregado
        }
    }





    private async void btnRegistrarse_Clicked(object sender, EventArgs e)
    {
        string nombre = txtNombre.Text;
        string correo = txtCorreo.Text;
        string password = txtPassword.Text;

        // Verificar que la contraseña tenga al menos 6 caracteres
        if (password.Length < 6)
        {
            await DisplayAlert("Error", "Contraseña muy corta. Ingrese al menos 6 caracteres.", "OK");
            return; // Salir del método si la contraseña es muy corta
        }

        usuarioRepository.AddNewUsuario(nombre, correo, password);

        if (usuarioRepository.StatusMessage.Contains("Usuario creado"))
        {
            // Mostrar mensaje de éxito
            await DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");

            // Redirigir a la vista de inicio de sesión
            await Navigation.PopAsync(); // Esto te llevará de vuelta a la página anterior (Login)
        }
        else
        {
            // Mostrar mensaje de error
            await DisplayAlert("Error", usuarioRepository.StatusMessage, "OK");
        }
    }

    //verificacion de correo valido
    private bool EsCorreoValido(string correo)
    {
        // Validar que el correo contenga un símbolo '@'
        return correo.Contains("@");
    }



    private void btnInicioSesion_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.Login(usuarioRepository));
    }
}