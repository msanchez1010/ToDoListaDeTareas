using Microsoft.Extensions.Logging;

namespace ToDoListaDeTareas
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //llamado a la ruta que voy a utilizar
            string dbPath = FileAccessHelper.GetLocalFilePath("usuario.db3");
            //compile el cambio en el dispositivo, ejecucion del metodo
            builder.Services.AddSingleton<UsuarioRepository>(s=> ActivatorUtilities.CreateInstance<UsuarioRepository>(s, dbPath));

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
