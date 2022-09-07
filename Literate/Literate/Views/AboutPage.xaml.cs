using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Plugin.Permissions;
using Xamarin.Essentials;
using Plugin.Permissions.Abstractions;
using System.Collections.Generic;
using Literate.ImagePickers.Android;
using System;
using Xamarin.CommunityToolkit.UI.Views;

namespace Literate.Views
{
    public partial class AboutPage : ContentPage
    {

        // Add your Computer Vision subscription key and endpoint
        static string subscriptionKey = "4d741d90f02c499487c8fd0e455da2aa";
        static string endpoint = "https://literate.cognitiveservices.azure.com/";

        //private readonly ITesseractApi _tesseract;
        //private readonly ITesseractApi _tesseractApi;

        //public static ComputerVisionClient Authenticate(string endpoint, string key)
        //{
        //    ComputerVisionClient client =
        //      new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
        //      { Endpoint = endpoint };
        //    return client;
        //}

        [System.Obsolete]
        private async void SelectImagesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1());

            
        }
        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new CameraView());
        }
       
        public AboutPage()
        {
            //ComputerVisionClient client = Authenticate(endpoint, subscriptionKey);
            InitializeComponent();
            //_tesseract = Resolver.Resolve<ITesseractApi>();

            //takePhoto.Clicked += async (sender, args) =>
            //{


            //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            //    {
            //        DisplayAlert("No Camera", ":( No camera available.", "OK");
            //        return;
            //    }

            //    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //    {
            //        Directory = "Test",
            //        SaveToAlbum = true,
            //        CompressionQuality = 75,
            //        CustomPhotoSize = 50,
            //        PhotoSize = PhotoSize.MaxWidthHeight,
            //        MaxWidthHeight = 2000,
            //        DefaultCamera = CameraDevice.Front
            //    });

            //    if (file == null)
            //        return;


            //    //DisplayAlert("File Location", file.Path, "OK");

            //    image.Source = ImageSource.FromStream(() =>
            //    {
            //        var stream = file.GetStream();
            //        file.Dispose();
            //        return stream;
            //    });
            //};

            

            //pickPhoto.Clicked += async (sender, args) =>
            //{
            //    //Check users permissions.
            //    try
            //    {       
            //        DependencyService.Get<IMediaService>().OpenGallery();
            //        try
            //        {
            //            MessagingCenter.Unsubscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedAndroid");
            //            MessagingCenter.Subscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedAndroid", async (s, images) =>
            //            {
            //                //If we have selected images, put them into the carousel view.
            //                if (images.Count > 0)
            //                {
            //                    ImgCarouselView.ItemsSource = images;
            //                }
            //                else
            //                {
            //                    await DisplayAlert("Hell nah slime", "your beat", "ok");
            //                }
            //            });
            //        }
            //        catch(Exception ex)
            //        {
            //            await DisplayAlert("Permission FUCK YOU!", ex.Message.ToString() + "UNo", "Ok");
            //        }

            //    }
            //    catch(Exception e)
            //    {
            //        await DisplayAlert("Permission FUCK YOU!", e.Message.ToString(), "Ok");
            //    }
                
                ////if (!_tesseractApi.Initialized)
                ////    await _tesseractApi.Init("eng");

                //if (!CrossMedia.Current.IsPickPhotoSupported)
                //{
                //    DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                //    return;
                //}
                //var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                //{
                //    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                //});

                //var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey));
                //client.Endpoint = endpoint;

                //var myfile = file.Path.ToString();
                //var Image1 = file.GetStream();
                //string myFile = myfile;
                //var result = client.RecognizePrintedTextInStreamAsync(false, Image1);
                //result.Wait();

                //var rst = result.Result;
                //string Final = "";
                //foreach (var r in rst.Regions)
                //{
                //    foreach (var t in r.Lines)
                //    {
                //        foreach (var w in t.Words)
                //        {
                //            Final = Final + " " + w.Text;
                //            Result.Text = Final;
                //        }
                //    }
                //}

                //if (file == null)
                //    return;

                //image.Source = ImageSource.FromStream(() =>
                //{
                //    var stream = file.GetStream();
                //    file.Dispose();

                //    return stream;
                //});


                ////var tessresult = await _tesseract.SetImage(file.Path);
                ////if (tessresult)
                ////{
                ////    Result.Text = _tesseract.Text;
                ////}
            

            
        }
        

    }
    
}