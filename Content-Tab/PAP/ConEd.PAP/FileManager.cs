using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConEd.PAP
{
    public static class FileManager
    {
        public static async Task SaveFileAsync(string fileName, MemoryStream inputStream)
        {
            try
            {            
                //fileName = "/Users/mothukuris-ehs/Library/Developer/CoreSimulator/Devices/694950AA-2EAF-4799-ABA5-6F05EE24DF60/data/Containers/Bundle/Application/20E84FAD-87CC-40C7-949A-3184712D776F/ConEd.PAP.iOS.app/Content/Critical Correspondence Procedure.pdf";
                fileName = "test1.pdf";
                var file = await FileSystem.Current.LocalStorage.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite))
                {
                    inputStream.WriteTo(stream);
                }

                //string path = FileSystem.Current.LocalStorage.Name+"/test.pdf";
                //IFile file = new IFile();
                //var check= await FileSystem.Current.LocalStorage.CheckExistsAsync("test.pdf");
                //if(ExistenceCheckResult.FileExists==check)
                //{
                //    string ss = "";

                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> Isfavourite(string fileName)
        {
            bool isFavorite = false;
            var check = await FileSystem.Current.LocalStorage.CheckExistsAsync("test.pdf");
            if (ExistenceCheckResult.FileExists == check)
            {
                isFavorite = true;

            }
            return isFavorite;
        }
    }
}
