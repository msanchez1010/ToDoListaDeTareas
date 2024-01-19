using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListaDeTareas
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string bdproyecto)
        {
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, bdproyecto);
        }

    }
}
