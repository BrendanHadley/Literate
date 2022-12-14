using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Database;
using Android.Provider;
using Android.Widget;
using Xamarin.Forms;
using System.Collections.Generic;
using Android.Content;
using CarouselView.FormsPlugin.Droid;
using System.Threading.Tasks;
using System.IO;
using Plugin.CurrentActivity;

namespace Literate.Droid
{
    [Activity(Label = "Literate", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }

        public static int OPENGALLERYCODE = 2;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Instance = this;
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        // Field, property, and method for Picture Picker
        public static readonly int PickImageId = 1000;

        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    List<string> images = new List<string>();

                    if(intent != null)
                    {
                        Android.Net.Uri uri = intent.Data;
                        Stream stream = ContentResolver.OpenInputStream(uri);

                        // Set the Stream as the completion of the Task
                        PickImageTaskCompletionSource.SetResult(stream);

                        //Separate all photos and get the path from them all individually.
                        //ClipData clipData = intent.ClipData;
                        //if (clipData != null)
                        //{
                        //    for (int i = 0; i < clipData.ItemCount; i++)
                        //    {
                        //        Android.Net.Uri uri = intent.Data;
                        //        Stream stream = ContentResolver.OpenInputStream(uri);

                        //        // Set the Stream as the completion of the Task
                        //        PickImageTaskCompletionSource.SetResult(stream);
                        //    }
                        //}
                        //else
                        //{
                        //    Android.Net.Uri uri = intent.Data;
                        //    var path = GetRealPathFromURI(uri);

                        //    if (path != null)
                        //    {
                        //        images.Add(path);
                        //    }
                        //}
                    }
                    //Android.Net.Uri uri = intent.Data;
                    //Stream stream = ContentResolver.OpenInputStream(uri);

                    //// Set the Stream as the completion of the Task
                    //PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }
        //protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        //{
        //    base.OnActivityResult(requestCode, resultCode, data);

        //    //If we are calling multiple image selection, enter into here and return photos and their filepaths.
        //    if (requestCode == OPENGALLERYCODE && resultCode == Result.Ok)
        //    {
        //        List<string> images = new List<string>();

        //        if (data != null)
        //        {
        //            //Separate all photos and get the path from them all individually.
        //            ClipData clipData = data.ClipData;
        //            if (clipData != null)
        //            {
        //                for (int i = 0; i < clipData.ItemCount; i++)
        //                {
        //                    ClipData.Item item = clipData.GetItemAt(i);
        //                    Android.Net.Uri uri = item.Uri;
        //                    var path = GetRealPathFromURI(uri);


        //                    if (path != null)
        //                    {
        //                        images.Add(path);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Android.Net.Uri uri = data.Data;
        //                var path = GetRealPathFromURI(uri);

        //                if (path != null)
        //                {
        //                    images.Add(path);
        //                }
        //            }

        //            //Send our images to the carousel view.
        //            MessagingCenter.Send<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedAndroid", images);
        //        }
        //    }
        //}

        ///// <summary>
        /////     Get the real path for the current image passed.
        ///// </summary>
        //[Obsolete]
        ///// <summary>
        /////     Get the real path for the current image passed.
        ///// </summary>
        public String GetRealPathFromURI(Android.Net.Uri contentURI)
        {
            try
            {
                ICursor imageCursor = null;
                string fullPathToImage = "";

                imageCursor = ContentResolver.Query(contentURI, null, null, null, null);
                imageCursor.MoveToFirst();
                int idx = imageCursor.GetColumnIndex(MediaStore.Images.ImageColumns.Data);

                if (idx != -1)
                {
                    fullPathToImage = imageCursor.GetString(idx);
                }
                else
                {
                    ICursor cursor = null;
                    var docID = DocumentsContract.GetDocumentId(contentURI);
                    var id = docID.Split(':')[1];
                    var whereSelect = MediaStore.Images.ImageColumns.Id + "=?";
                    var projections = new string[] { MediaStore.Images.ImageColumns.Data };

                    cursor = ContentResolver.Query(MediaStore.Images.Media.InternalContentUri, projections, whereSelect, new string[] { id }, null);
                    if (cursor.Count == 0)
                    {
                        cursor = ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri, projections, whereSelect, new string[] { id }, null);
                    }
                    var colData = cursor.GetColumnIndexOrThrow(MediaStore.Images.ImageColumns.Data);
                    cursor.MoveToFirst();
                    fullPathToImage = cursor.GetString(colData);
                }
                return fullPathToImage;
            }
            catch (Exception ex)
            {
                Toast.MakeText(Android.App.Application.Context, "Unable to get path", ToastLength.Long).Show();
            }
            return null;
        }
    }
}