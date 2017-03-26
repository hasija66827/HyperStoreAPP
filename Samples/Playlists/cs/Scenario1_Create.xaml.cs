//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Media.Playlists;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SDKTemplate
{
    public sealed partial class BillingScenario : Page
    {
        private MainPage rootPage = MainPage.Current;
        public ProductViewModel ViewModel { get; set; }
        // Discount Percentage and Discount Text Box names
        public static readonly string DISCOUNTPERCENTAGE = "DiscountPercentage";
        public static readonly string DISCOUNT = "Discount";
        public static readonly string COSTPRICE = "CostPrice";
        public static readonly string NETVALUE = "NetValue";
        public static readonly string SELLINGPRICE = "SellingPrice";
        public static readonly string QUANTITY = "Quantity";
        public BillingScenario()
        {
            this.InitializeComponent();
            this.ViewModel = new ProductViewModel();
        }



        /// <summary>
        /// Creates a playlist with the audio picked by the user in the FilePicker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void PickAudioButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = MainPage.CreateFilePicker(MainPage.audioExtensions);
            IReadOnlyList<StorageFile> files = await picker.PickMultipleFilesAsync();

            if (files.Count > 0)
            {
                Playlist playlist = new Playlist();

                foreach (StorageFile file in files)
                {
                    playlist.Files.Add(file);
                }

                StorageFolder folder = KnownFolders.MusicLibrary;
                string name = "Sample";
                NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting;
                PlaylistFormat format = PlaylistFormat.WindowsMedia;

                try
                {
                    StorageFile savedFile = await playlist.SaveAsAsync(folder, name, collisionOption, format);
                    this.rootPage.NotifyUser(savedFile.Name + " was created and saved with " + files.Count + " files.", NotifyType.StatusMessage);
                }
                catch (Exception error)
                {
                    rootPage.NotifyUser(error.Message, NotifyType.ErrorMessage);
                }
            }
            else
            {
                rootPage.NotifyUser("No files picked.", NotifyType.ErrorMessage);
            }
        }
    }
}
