using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.SignUp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CompleteUserInformationCC : Page
    {
        string GeneratedHTML = "";
        public CompleteUserInformationCC()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var geoLocator = new Geolocator();
            geoLocator.DesiredAccuracy = PositionAccuracy.High;
            Geoposition pos = await geoLocator.GetGeopositionAsync();
            string latitude = "Latitude: " + pos.Coordinate.Point.Position.Latitude.ToString();
            string longitude = "Longitude: " + pos.Coordinate.Point.Position.Longitude.ToString();
            location.Text = latitude + " " + longitude;
            this.LoadData();
            MapWebView.NavigateToString(GeneratedHTML);
            base.OnNavigatedTo(e);
        }
        private async void LoadData()
        {
            try
            {
                await _LoadJsonLocalnew("googleMap.html", "");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task _LoadJsonLocalnew(string url, object ClassName)
        {
            try
            {
                StorageFolder folder = await Package.Current.InstalledLocation.GetFolderAsync("htmlPage");
                StorageFile file = await folder.GetFileAsync(url);
                Stream stream = await file.OpenStreamForReadAsync();
                StreamReader reader = new StreamReader(stream);
                String html = reader.ReadToEnd();
                GeneratedHTML = html.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
