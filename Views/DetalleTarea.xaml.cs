using System;
using ToDoListaDeTareas.Models;
using Microsoft.Maui.Controls;

namespace ToDoListaDeTareas.Views;

public partial class DetalleTarea : ContentPage
{
	private Tarea tarea;
    private TareaRepository tareaRepository;
    private VistaTareas VistaTareas;
	public DetalleTarea(Tarea tarea, TareaRepository tareaRepository, VistaTareas vistaTareas)
	{
		InitializeComponent();
		this.tarea = tarea;
        this.tareaRepository = tareaRepository;
        this.VistaTareas = vistaTareas;

        BindingContext = tarea;

        btnCancelarTarea.IsVisible = false;
        btnGuardarTarea.IsVisible = false;
    }

    private void btnEditarTarea_Clicked(object sender, EventArgs e)
    {
        btnEditarTarea.IsVisible = false;
        btnEliminarTarea.IsVisible = false;
        btnCancelarTarea.IsVisible = true;
        btnGuardarTarea.IsVisible = true;

        // Habilitar la edición de los campos, para editarlos
        entryNombre.IsEnabled = true;
        entryDescripcion.IsEnabled = true;
        entryFechaEjecucion.IsEnabled = true;
        entryEstado.IsEnabled = true;
        entryPrioridad.IsEnabled = true;
    }

    private void btnCancelarTarea_Clicked(object sender, EventArgs e)
    {
        btnEditarTarea.IsVisible = true;
        btnEliminarTarea.IsVisible = true;
        btnCancelarTarea.IsVisible = false;
        btnGuardarTarea.IsVisible = false;
    }

    private async void btnGuardarTarea_Clicked(object sender, EventArgs e)
    {
        tareaRepository.ActualizarTarea(tarea);

        // Restaurar campos a modo de solo lectura
        btnEditarTarea.IsVisible = true;
        btnEliminarTarea.IsVisible = true;
        btnCancelarTarea.IsVisible = false;
        btnGuardarTarea.IsVisible = false;

        // Notificar a la vista de VistaTareas sobre la actualización
        if (VistaTareas != null)
        {
            // Esperar a que la tarea se actualice antes de notificar y volver
            await Task.Delay(500); // Puedes ajustar el tiempo de espera según sea necesario
            VistaTareas.ActualizarListaTareas();
        }

        // Navegar de regreso a la vista de tareas
        await Navigation.PopAsync();
    }

    private async void btnEliminarTarea_Clicked(object sender, EventArgs e)
    {
        bool confirmacion = await DisplayAlert("Eliminar Tarea", "¿Estás seguro que deseas eliminar esta tarea?", "Sí", "No");

        if (confirmacion)
        {
            // Eliminar la tarea
            tareaRepository.EliminarTarea(tarea);

            // Notificar a la vista de VistaTareas sobre la actualización
            VistaTareas.ActualizarListaTareas();

            // Regresar a la vista anterior
            await Navigation.PopAsync();
        }
    }
}