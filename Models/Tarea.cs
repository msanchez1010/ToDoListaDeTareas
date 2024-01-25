using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListaDeTareas.Models
{
    [SQLite.Table("tarea")]
    public class Tarea
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Tarea_Id { get; set; }

        [SQLite.MaxLength(50), SQLite.NotNull]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime? FechaEjecucion { get; set; }

        public string Estado { get; set; }

        public string Prioridad { get; set; }

        [SQLite.Column("usuario_id")]
        public int UsuarioId { get; set; }

        [SQLite.Ignore]
        public Usuario Usuario { get; set; }

        public override string ToString()
        {
            return $"{Nombre} - {Descripcion}"; // O cualquier formato que desees mostrar
        }
    }

}