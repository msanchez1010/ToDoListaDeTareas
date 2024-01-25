using System;
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
            mainstackLayout.Children.Add(btnNuevaTarea);

            MostrarTareas();
        }

        private void MostrarTareas()
        {
            int usuarioId = App.usuarioRepo.GetUsuarioIdActual();
            List<Tarea> tareas = tareaRepository.GetTareasByUsuarioId(usuarioId);
            listViewTareas.ItemsSource = tareas;

        
            listViewTareas.ItemTapped += (sender, e) =>
            {
                
            };
        }

        private async void btnNuevaTarea_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NuevaTarea(tareaRepository, this));
        }

        public void ActualizarListaTareas()
        {
            MostrarTareas();
        }
    }
}