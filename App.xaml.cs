namespace ToDoListaDeTareas
{
    public partial class App : Application
    {
        //variable de Usuariorepository
        public static UsuarioRepository usuarioRepo {  get; set; }
        

        public App(UsuarioRepository usuarioRepository )
        {
            InitializeComponent();

            MainPage = new NavigationPage (new Views.Login(usuarioRepository));
            usuarioRepo = usuarioRepository;
        }

       

    }
}
