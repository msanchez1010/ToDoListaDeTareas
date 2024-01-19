using SQLite;
using System;
using System.Linq;
using ToDoListaDeTareas.Models;

namespace ToDoListaDeTareas
{
    public class UsuarioRepository
    {
        string dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        public void Init()
        {
            if (conn is not null)
                return;
            conn = new(dbPath);
            conn.CreateTable<Usuario>();
        }

        public UsuarioRepository(string dbPath1)
        {
            dbPath = dbPath1;
        }

        public string GetDbPath()
        {
            return dbPath;
        }

        public bool UsuarioExists(string correo)
        {
            Init();
            return conn.Table<Usuario>().Any(u => u.Correo == correo);
        }

        public bool AuthenticateUsuario(string correo, string password)
        {
            Init();
            return conn.Table<Usuario>().Any(u => u.Correo == correo && u.Password == password);
        }

        public void AddNewUsuario(string nombre, string correo, string password)
        {
            int result = 0;

            try
            {
                Init();

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(password))
                    throw new Exception("Nombre, correo y contraseña son requeridos");

                if (UsuarioExists(correo))
                    throw new Exception("El usuario ya existe");

                Usuario usuario = new Usuario() { Nombre = nombre, Correo = correo, Password = password };
                result = conn.Insert(usuario);
                StatusMessage = string.Format("Usuario creado: {0}", nombre);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al ingresar: {0}", ex.Message);
            }
        }
        public int GetUsuarioIdActual()
        {
            // Aquí debes implementar la lógica para obtener el Id del usuario actual.
            // Por ejemplo, si tienes la información del usuario actual almacenada en una propiedad, puedes devolverla.
            // Reemplaza esto con tu lógica real.
            return 1; // Cambia esto con tu implementación real.
        }
    }
}
