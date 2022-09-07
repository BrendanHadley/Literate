using Literate.ViewModels;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Plugin.Media;
using System;
using System.ComponentModel;
using System.IO;
using Xamarin.Forms;

namespace Literate.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        static string subscriptionKey = "4d741d90f02c499487c8fd0e455da2aa";
        static string endpoint = "https://literate.cognitiveservices.azure.com/";
        string file_name = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "book.txt");

        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();           

        }

        private async void OCR_Clicked(object sender, System.EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

            });

            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey));
            client.Endpoint = endpoint;

            var myfile = file.Path.ToString();
            var Image1 = file.GetStream();
            string myFile = myfile;
            var result = client.RecognizePrintedTextInStreamAsync(false, Image1);
            result.Wait();

            var rst = result.Result;
            string Final = "";
            foreach (var r in rst.Regions)
            {
                foreach (var t in r.Lines)
                {
                    foreach (var w in t.Words)
                    {
                        Final = Final + " " + w.Text;
                        if (File.Exists(file_name))
                        {
                            OCResult.Text = File.ReadAllText(file_name); 
                        }
                        OCResult.Text = Final;
                    }
                }
            }

            if (file == null)
                return;
        }
    }
}