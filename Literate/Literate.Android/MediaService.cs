using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Xamarin.Forms;
using Literate.Droid;
using System.IO;
using Plugin.CurrentActivity;
using Literate.ImagePickers.Android;
using Android.Provider;

[assembly: Xamarin.Forms.Dependency(typeof(MediaService))]
namespace Literate.Droid
{
    public class MediaService : Java.Lang.Object, IMediaService
    {

        public static int OPENGALLERYCODE = 2;
        public void OpenGallery()
        {
            try
            {
                var imageIntent = new Intent(Intent.ActionPick);
                imageIntent.SetType("*/*");
                imageIntent.PutExtra(Intent.ExtraAllowMultiple, true);
                imageIntent.SetAction(Intent.ActionGetContent);
                ((Activity)Forms.Context).StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), OPENGALLERYCODE);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        ///     Call this when you want to delete our temporary images.
        ///     Recommendation: Call this after successfully uploading images to Azure Blob Storage.
        /// </summary>
		void IMediaService.ClearFileDirectory()
        {
            string directory;
            if ((int)Android.OS.Build.VERSION.SdkInt >= 29)
            {
                directory = new Java.IO.File(Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryPictures), ImageHelpers.collectionName).Path.ToString();
            }
            else
            {
                directory = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), ImageHelpers.collectionName).Path.ToString();
            }

            if (Directory.Exists(directory))
            {
                var list = Directory.GetFiles(directory, "*");
                if (list.Length > 0)
                {
                    for (int i = 0; i < list.Length; i++)
                    {
                        File.Delete(list[i]);
                    }
                }
            }
        }


        /*
        Example of how to call ClearFileDirectory():
            if (Device.RuntimePlatform == Device.Android)
            {
                DependencyService.Get<IMediaService>().ClearFileDirectory();
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                GMMultiImagePicker.Current.ClearFileDirectory();
            }
        */
    }
}