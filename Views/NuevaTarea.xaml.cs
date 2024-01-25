// NuevaTarea.xaml.cs
using System;
using ToDoListaDeTareas.Views;
using Microsoft.Maui.Controls;

namespace ToDoListaDeTareas.Views
{
    public partial class NuevaTarea : ContentPage
    {
        private TareaRepository tareaRepository;

        public NuevaTarea(TareaRepository tareaRepository, VistaTareas vistaTareas)
        {
            InitializeComponent();
            this.tareaRepository = tareaRepository;

            // Agrega los elementos de la interfaz de usuario necesarios para ingresar los detalles de la nueva tarea
        }

        private void btnGuarda_Clicked(object sender, EventArgs e)
        {
            // Obt�n los valores ingresados por el usuario
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            DateTime? fechaEjecucion = datePickerFechaEjecucion.Date;
            string estado = txtEstado.Text;
            string prioridad = txtPrioridad.Text;

            // Aseg�rate de que el usuarioId sea el del usuario actual (puedes obtenerlo desde App.UsuarioRepo)
            int usuarioId = App.usuarioRepo.GetUsuarioIdActual(); // Necesitas implementar este m�todo en UsuarioRepository

            // Agregar la nueva tarea
            tareaRepository.AddNewTarea(nombre, descripcion, fechaEjecucion, estado, prioridad, usuarioId);

            // Mostrar mensaje de �xito y regresar a la vista de tareas
            DisplayAlert("�xito", "Tarea creada correctamente", "OK");
            Navigation.PopAsync();
        }
    }
}
