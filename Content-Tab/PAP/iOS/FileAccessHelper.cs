using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Foundation;

namespace ConEd.PAP.iOS
{
    public class FileAccessHelper
    {       
        public static string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            string dbPath = Path.Combine(libFolder, filename);

            CopyDatabaseIfNotExists(dbPath);

            return dbPath;
        }

        private static void CopyDatabaseIfNotExists(string dbPath)
        {
            //File.Delete(dbPath);
            if (!File.Exists(dbPath))
            {
                //var existingDb = NSBundle.MainBundle.PathForResource ("people", "db3");
                var existingDb = NSBundle.MainBundle.PathForResource("PAP", "db3");
                File.Copy(existingDb, dbPath);                
        
            }
        }

        //public static void CopyFromDevice()
        //{

        //    //var existingDb = NSBundle.MainBundle.PathForResource("PAP", "db3");
        //    string DeviceDB = "";
        //    string SolutionDB = "";
        //    File.Copy(DeviceDB, SolutionDB);
        //}
        
    }
}
