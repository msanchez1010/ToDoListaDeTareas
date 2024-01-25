using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListaDeTareas.Models;

namespace ToDoListaDeTareas
{
    public class TareaRepository
    {
        string dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        public void Init()
        {
            if (conn is not null)
                return;
            conn = new(dbPath);
            conn.CreateTable<Tarea>();
        }

        public TareaRepository(string dbPath)
        {
            this.dbPath = dbPath;
        }

        public void AddNewTarea(string nombre, string descripcion, DateTime? fechaEjecucion, string estado, string prioridad, int usuarioId)
        {
            int result = 0;

            try
            {
                Init();

                Tarea tarea = new Tarea()
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    FechaEjecucion = fechaEjecucion,
                    Estado = estado,
                    Prioridad = prioridad,
                    UsuarioId = usuarioId
                };

                result = conn.Insert(tarea);
                StatusMessage = string.Format("Tarea creada: {0}", nombre);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al crear tarea: {0}", ex.Message);
            }
        }

        public List<Tarea> GetTareasByUsuarioId(int usuarioId)
        {
            List<Tarea> tareas = new List<Tarea>();

            try
            {
                Init();

                // Consulta para obtener las tareas asociadas al usuarioId
                tareas = conn.Table<Tarea>().Where(t => t.UsuarioId == usuarioId).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al obtener tareas: {0}", ex.Message);
            }

            return tareas;
        }
        
    }
}
