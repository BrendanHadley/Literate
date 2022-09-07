using Literate.Models;
using Literate.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Literate.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
            string file_name = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "book.txt");

        }

        //public async void takePhoto_Clicked(object sender, EventArgs e)
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
        //}

        private void takePhoto_Clicked_1(object sender, EventArgs e)
        {

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}