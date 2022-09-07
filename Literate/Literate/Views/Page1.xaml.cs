using Literate.ImagePickers.Android;
using Literate.Services;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Literate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private async void SelectPhotos_Clicked(object sender, EventArgs e)
        {
            
            //Check users permissions.
            var storagePermissions = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            var photoPermissions = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);
            if (storagePermissions == PermissionStatus.Granted && photoPermissions == PermissionStatus.Granted)
            {

                //If we are on Android, call IMediaService.
                if (Device.RuntimePlatform == Device.Android)
                {

                    DependencyService.Get<IMediaService>().OpenGallery();

                    //MessagingCenter.Unsubscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedAndroid");
                    MessagingCenter.Subscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedAndroid", (s, images) =>
                    {
                        //If we have selected images, put them into the carousel view.
                        if (images.Count > 0)
                        {
                            ImgCarouselView.ItemsSource = images;

                        }
                    });
                }
            }
            else
            {
                await DisplayAlert("Permission Denied!", "\nPlease go to your app settings and enable permissions.", "Ok");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedAndroid");
            MessagingCenter.Unsubscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectediOS");
            GC.Collect();
        }
    }
}