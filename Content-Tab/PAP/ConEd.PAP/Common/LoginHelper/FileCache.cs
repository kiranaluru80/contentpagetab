using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.IO;
using System.Security.Cryptography;
using PCLStorage;


namespace ConEd.PAP.Common.LoginHelper.NativeAppEmulator
{
    internal class FileCache : TokenCache
    {
        private string CacheFilePath;
        private static readonly object Filelock = new object();

        public FileCache(string filepath=@".\TokenCache.dat")
        {
            CacheFilePath = filepath;
            this.AfterAccess = AfterAccessNotification;
            this.BeforeAccess = BeforAccessNotification;

            var file = FileSystem.Current.LocalStorage.CreateFileAsync(filepath, CreationCollisionOption.ReplaceExisting);
            lock (Filelock)
            {
                //ReadFile(file);
            }

        }

        private async void ReadFile()
        {
            //this.Deserialize(File.Exists(CacheFilePath) ? ProtectedData.Unprotect(
            //                    File.ReadAllBytes(CacheFilePath), null, DataProtectionScope.CurrentUser) : null);

            //var check = await FileSystem.Current.LocalStorage.CheckExistsAsync("TokenCache.dat");
            //if (ExistenceCheckResult.FileExists == check)
            //{


            //   // this.Deserialize()
            //}
        }




        public override void Clear()
        {
            base.Clear();
            //File.Delete(CacheFilePath);
        }

        void BeforAccessNotification(TokenCacheNotificationArgs args)
        {

            lock (Filelock)
            {
                this.Deserialize(File.Exists(CacheFilePath) ? ProtectedData.Unprotect(
                    File.ReadAllBytes(CacheFilePath), null, DataProtectionScope.CurrentUser) : null);
            }

        }

        void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            // if the access operation resulted in a cache update
            if (this.HasStateChanged)
            {
                lock (Filelock)
                {
                    // reflect changes in the persistent store
                    File.WriteAllBytes(CacheFilePath, ProtectedData.Protect(this.Serialize(), null, DataProtectionScope.CurrentUser));
                    // once the write operation took place, restore the HasStateChanged bit to false
                    this.HasStateChanged = false;
                }
            }

        }
    }
}