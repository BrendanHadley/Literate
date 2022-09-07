using Android.OS;
using Literate.Droid;
using Literate.Services;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SaveService))]
namespace Literate.Droid
{
    public class SaveService : ISaveService
    {
        void ISaveService.SaveFile(string fileName, byte[] data)
        {
            string picPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures));
            //string picPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
            string filePath = Path.Combine(picPath, fileName);
            File.WriteAllBytes(filePath, data);
        }
    }

}