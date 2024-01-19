namespace ToDoListaDeTareas.Views;

public partial class VistaTareas : ContentPage
{
	private TareaRepository tareaRepository;
	public VistaTareas()
	{
		InitializeComponent();
		this.tareaRepository = new TareaRepository(App.usuarioRepo.GetDbPath());

        Button btnNuevaTarea = new Button
        {
            Text = "Nueva Tarea",
            BackgroundColor = Color.FromHex("#2ecc71"),  // Verde
            TextColor = Color.FromHex("#FFFFFF")
        };

    }

    private async void btnNuevaTarea_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NuevaTarea(tareaRepository));
    }
}