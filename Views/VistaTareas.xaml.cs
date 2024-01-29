using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using ToDoListaDeTareas.Models;

namespace ToDoListaDeTareas.Views
{
    public partial class VistaTareas : ContentPage
    {
        private TareaRepository tareaRepository;
        private ListView listViewTareas;
        private Button btnNuevaTarea;

        public VistaTareas(TareaRepository tareaRepository)
        {
            InitializeComponent();
            this.tareaRepository = tareaRepository;

            listViewTareas = new ListView();
            btnNuevaTarea = new Button
            {
            Text = "Nueva Tarea",
            BackgroundColor = Color.FromHex("#2ecc71"),
            TextColor = Color.FromHex("#FFFFFF")
            };

            btnNuevaTarea.Clicked += btnNuevaTarea_Clicked;

            mainstackLayout.Children.Add(listViewTareas);
            mainstackLayout.VerticalOptions = LayoutOptions.FillAndExpand;
            mainstackLayout.Children.Add(btnNuevaTarea);

            MostrarTareas();
        }

        private async void MostrarTareas()
        {
            int usuarioId = App.usuarioRepo.GetUsuarioIdActual();
            List<Tarea> tareas = tareaRepository.GetTareasByUsuarioId(usuarioId);
            listViewTareas.ItemsSource = null;
            listViewTareas.ItemsSource = tareas;

        
            listViewTareas.ItemTapped += async (sender, e) =>
            {
                Tarea tareaSeleccionada = e.Item as Tarea;
                if (tareaSeleccionada != null)
                {
                    // Navega a la vista de detalle con la tarea seleccionada
                    await Navigation.PushAsync(new DetalleTarea(tareaSeleccionada, tareaRepository, this));
                }
            };
        }

        private async void btnNuevaTarea_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NuevaTarea(tareaRepository, this));
        }

        private async void listViewTareas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Tarea tareaSeleccionada = e.Item as Tarea;
            if (tareaSeleccionada != null)
            {
                // Navegar a la vista de detalle con la tarea seleccionada
                await Navigation.PushAsync(new DetalleTarea(tareaSeleccionada, tareaRepository, this));
            }
        }

        public async void ActualizarListaTareas()
        {
            int usuarioId = App.usuarioRepo.GetUsuarioIdActual();
            List<Tarea> tareas = tareaRepository.GetTareasByUsuarioId(usuarioId);
            listViewTareas.ItemsSource = null;
            listViewTareas.ItemsSource = tareas;

            //Agregar una pequeña espera para permitir que la lista se actualice antes de continuar
            await Task.Delay(100);
        }
        

        
    }
}