using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GoMemory.iOS.DataAccess
{
    public class SqliteDbConnectionHelper
    {
        public static string GetLocalDbPath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(path, filename);
        }
    }
}