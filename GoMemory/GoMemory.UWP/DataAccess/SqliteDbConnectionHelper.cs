using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Storage;

namespace GoMemory.UWP.DataAccess
{
    public class SqliteDbConnectionHelper
    {
        public static string GetLocalDbPath(string filename)
        {
            string path = ApplicationData.Current.LocalFolder.Path;
            return System.IO.Path.Combine(path, filename);
        }
    }
}