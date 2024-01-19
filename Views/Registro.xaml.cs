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


    // Habilitar el bot�n de registrarse si todos los campos est�n llenos
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

        // Puedes mostrar un mensaje de error si el correo no es v�lido
        if (!isCorreoValido)
        {
            lblCorreoMensaje.Text = "Correo no v�lido";
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
        // Validar que la contrase�a tenga al menos 6 caracteres
        string password = txtPassword.Text;
        bool isPasswordValid = !string.IsNullOrWhiteSpace(password) && password.Length >= 6;

        // Puedes mostrar un mensaje de error si la contrase�a no es v�lida
        if (!isPasswordValid)
        {
            lblContrase�aMensaje.Text = "Contrase�a debe tener al menos 6 caracteres";
            btnRegistrarse.IsEnabled = false;
        }
        else
        {
            lblContrase�aMensaje.Text = "";
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

        // Verificar que la contrase�a tenga al menos 6 caracteres
        if (password.Length < 6)
        {
            await DisplayAlert("Error", "Contrase�a muy corta. Ingrese al menos 6 caracteres.", "OK");
            return; // Salir del m�todo si la contrase�a es muy corta
        }

        usuarioRepository.AddNewUsuario(nombre, correo, password);

        if (usuarioRepository.StatusMessage.Contains("Usuario creado"))
        {
            // Mostrar mensaje de �xito
            await DisplayAlert("�xito", "Usuario registrado correctamente", "OK");

            // Redirigir a la vista de inicio de sesi�n
            await Navigation.PopAsync(); // Esto te llevar� de vuelta a la p�gina anterior (Login)
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
        // Validar que el correo contenga un s�mbolo '@'
        return correo.Contains("@");
    }



    private void btnInicioSesion_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.Login(usuarioRepository));
    }
}