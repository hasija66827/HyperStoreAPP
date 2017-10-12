﻿using SDKTemplate.DTO;
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
    public class Cordinates
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CompleteUserInformationCC : Page
    {
        public static CompleteUserInformationCC Current;
        public CompleteUserInformationViewModel _CUIV { get; set; }
        private Cordinates _Cordinates;
        string GeneratedHTML = "";
        public CompleteUserInformationCC()
        {
            Current = this;
            this.InitializeComponent();
            this._CUIV = new CompleteUserInformationViewModel()
            {
                HSAV = new HyperStoreAccountViewModel(),
                PCV = new ProfileCompletionViewModel(),
                BIV = new BusinessInformationViewModel(),
            };
        }

        public void Current_HSAFormNavigatedEvent(HyperStoreAccountViewModel HSAV)
        {
            Utility.CopyPropertiesTo<HyperStoreAccountViewModel, HyperStoreAccountViewModel>(HSAV, this._CUIV.HSAV);
            this._CUIV.OnALLPropertyChanged();
        }

        public void Current_BIFNavigatedEvent(BusinessInformationViewModel BIV)
        {
            Utility.CopyPropertiesTo<BusinessInformationViewModel, BusinessInformationViewModel>(BIV, this._CUIV.BIV);
            this._CUIV.OnALLPropertyChanged();
        }

        public async void Current_PCFNavigatedEvent(ProfileCompletionViewModel PCV)
        {
            this._CUIV.PCV = PCV;
            var IsVerified = await _InitiateOTPVerificationAsync();
            bool IsUserCreated = false;
            if (IsVerified)
            {
                IsUserCreated = await _CreateUser();
                if (IsUserCreated == true)
                    RootPage.Current.Navigate(typeof(Login.LoginCC));
            }
        }

        private async Task<bool> _InitiateOTPVerificationAsync()
        {
            var SMSContent = OTPVConstants.SMSContents[OTPScenarioType.User_Registration];
            var fomattedSMSContent = String.Format(SMSContent, this._CUIV.PCV.FirstName, OTPVConstants.OTPLiteral);
            var OTPVerificationDTO = new OTPVerificationDTO()
            {
                UserID = Guid.Empty,
                ReceiverMobileNo = this._CUIV.HSAV.MobileNo,
                SMSContent = fomattedSMSContent,
            };
            var IsVerified = await OTPDataSource.VerifyTransactionByOTPAsync(OTPVerificationDTO);
            return IsVerified;
        }

        private async Task<bool> _CreateUser()
        {
            var personalInformationDTO = new PersonalInformationDTO()
            {
                FirstName = _CUIV.PCV.FirstName,
                LastName = _CUIV.PCV.LastName,
                EmailId = _CUIV.PCV.EmailId,
                DateOfBirth = _CUIV.PCV.DateOfBirth,
                MobileNo = _CUIV.HSAV.MobileNo,
                Password = _CUIV.HSAV.Password,
                Passcode = _CUIV.PCV.Passcode,
            };

            var businessInformationDTO = new BusinessInformationDTO()
            {
                BusinessName = _CUIV.BIV.BusinessName,
                BusinessType = _CUIV.BIV.SelectedBusinessType,
                GSTIN = _CUIV.BIV.GSTIN,
                AddressLine = _CUIV.BIV.AddressLine,
                City = _CUIV.BIV.City,
                PinCode = _CUIV.BIV.PinCode,
                State = _CUIV.BIV.SelectedState,
                Cordinates = this._Cordinates,
            };

            var user = new UserDTO()
            {
                PI = personalInformationDTO,
                BI = businessInformationDTO,
                DeviceId = Utility.GetHardwareId()
            };

            var authenticationToken = await UserDataSource.CreateNewUserAsync(user);
            if (authenticationToken != null)
            {
                BaseURI.User = authenticationToken.User;
                return true;
            }
            return false;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var geoLocator = new Geolocator();
            geoLocator.DesiredAccuracy = PositionAccuracy.High;
            Geoposition pos = null;
            try
            {
               pos = await geoLocator.GetGeopositionAsync();   
            }
            catch (Exception )
            {
                //Unable to access the location: reason: System has not turned on location settings.
            }
            this._Cordinates = new Cordinates()
            {
                Latitude = pos?.Coordinate.Point.Position.Latitude.ToString(),
                Longitude = pos?.Coordinate.Point.Position.Longitude.ToString()
            };
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
