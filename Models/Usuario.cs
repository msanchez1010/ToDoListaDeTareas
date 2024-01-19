using SQLite;
using System;
using System.Collections.Generic;

namespace ToDoListaDeTareas.Models
{
    // CREACION DE TABLA USUARIO
    [SQLite.Table("usuario")]
    public class Usuario
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }

        [SQLite.MaxLength(50), SQLite.NotNull]
        public string Nombre { get; set; }

        [SQLite.MaxLength(100), SQLite.Indexed(Unique = true), SQLite.NotNull]
        public string Correo { get; set; }

        [SQLite.MaxLength(100), SQLite.NotNull]
        public string Password { get; set; }

        [SQLite.Ignore]
        public List<Tarea> Tareas { get; set; }
    }
}