using Literate.Services;
using System;
using System.IO;
using System.Threading;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Literate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraView : Page2
    {
        public CameraView()
        {
            InitializeComponent();
        }
		void CameraView_OnAvailable(object sender, bool e)
		{
		}

        void CameraView_MediaCaptured(object sender, MediaCapturedEventArgs e)
        {
            if (xctCameraView.IsAvailable == true)
            {
                
                if (xctCameraView.IsEnabled == true)
                {
                    previewImage.IsVisible = true;
                    previewImage.Rotation = e.Rotation;
                    previewImage.Source = e.Image;
                    doCameraThings.IsEnabled = true;
                }
                else
                {
                    doCameraThings.IsEnabled = false;
                    doCameraThings.Text = "Something went wrong, please try again";
                }
            }
            else
            {
            }

        }

        private void DoCameraThings_Clicked(object sender, EventArgs e)
        {
            xctCameraView.Shutter();
            doCameraThings.Text = xctCameraView.CaptureMode == CameraCaptureMode.Video
                ? "Stop Recording"
                : "Snap Picture";
        }

        private async void xctCameraView_MediaCaptured(object sender, Xamarin.CommunityToolkit.UI.Views.MediaCapturedEventArgs e)
        {
            //await DisplayAlert("Location", Android.OS.Environment.ExternalStorageDirectory.AbsolutePath.ToString(), "Ok");

            //Find unix timestamp (seconds since 01/01/1970)
            long ticks = DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
            ticks /= 10000000; //Convert windows ticks to seconds
            string timestamp = ticks.ToString() + ".jpg";


            CapturedImage1.Source = e.Image;
            CapturedImage1.Rotation = e.Rotation;
            string facts = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Test";
            if (!Directory.Exists(facts))
                Directory.CreateDirectory(facts);
            string picPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
            string FilePath = Path.Combine(facts, timestamp);
            File.WriteAllBytes(FilePath, e.ImageData);
            await DisplayAlert("Saved", FilePath, "ok");

            //try
            //{
            //    byte[] image = e.ImageData;

            //    DependencyService.Get<ISaveService>().SaveFile(timestamp, image);

            //    string picPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures));
            //    await DisplayAlert("Saved", picPath, "Ok");
            //}
            //catch (Exception ex)
            //{
            //    await DisplayAlert("Error", ex.Message, "ok");
            //}

        }

        void Flash_Toggled(object sender, ToggledEventArgs e)
            => xctCameraView.FlashMode = e.Value ? CameraFlashMode.Torch : CameraFlashMode.Off;

        private void SavePhoto_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}