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
        public Product r = new Product();
        public ProductViewModel ViewModel { get; set; }
        // Discount Percentage and Discount Text Box names
        public static readonly string DISCOUNTPERCENTAGE = "DiscountPercentage";
        public static readonly string DISCOUNT = "Discount";
        public static readonly string COSTPRICE = "CostPrice";
        public static readonly string NETVALUE = "NetValue";
        public static readonly string SELLINGPRICE = "SellingPrice";
        public static readonly string QUANTITY = "Quantity";
        enum Modified
        {
            Discount,
            DiscountPercentage,
            Quantity
        };
        public BillingScenario()
        {
            this.InitializeComponent();
            this.ViewModel = new ProductViewModel();
            this.textBox.LostFocus += _temp_LostFocus;
        }
        private void DiscountPercentage_LostFocus(object sender, RoutedEventArgs e)
        {
            this.ResetValuesOfIteminList(sender, Modified.DiscountPercentage);
        }
        private void Discount_LostFocus(object sender, RoutedEventArgs e)
        {
            this.ResetValuesOfIteminList(sender, Modified.Discount);
        }
        void ResetValuesOfIteminList(object sender, Modified m)
        {
            TextBox textBox = sender as TextBox;
            Windows.UI.Xaml.Controls.StackPanel parent = textBox.Parent as StackPanel;
            // Retrieving the costprice of the product from costprice control.
            TextBlock CostpriceControl = parent.Children.OfType<TextBlock>().First(x => x.Name.Equals(COSTPRICE));
            decimal costPrice = Convert.ToDecimal(CostpriceControl.GetValue(TextBlock.TextProperty));
            // Retrieving the quantity of the product from quantity control.
            TextBox quantityControl = parent.Children.OfType<TextBox>().First(x => x.Name.Equals(QUANTITY));
            Int32 quantity = Convert.ToInt32(quantityControl.GetValue(TextBox.TextProperty));

            decimal discount = 0;
            decimal discountPercentage = 0;
            if (m == Modified.Discount)
            {
                discount = Convert.ToDecimal(textBox.GetValue(TextBox.TextProperty));
                // Setting discount to zero if the discount is greater than costprice.
                if (discount > costPrice)
                {
                    discount = 0;
                    textBox.SetValue(TextBox.TextProperty, "0");
                    //#alert the discount should be less than costprice.
                }
                // Computing the discount percentage.
                if (costPrice != 0)
                {
                    discountPercentage = (discount * 100) / costPrice;
                }
                // Setting the value of DiscountPercentage text box control.
                TextBox DiscountPercentageControl = parent.Children.OfType<TextBox>().First(x => x.Name.Equals(DISCOUNTPERCENTAGE));
                DiscountPercentageControl.SetValue(TextBox.TextProperty, discountPercentage.ToString());
            }
            else if (m == Modified.DiscountPercentage)
            {
                discountPercentage = Convert.ToDecimal(textBox.GetValue(TextBox.TextProperty));
                // Setting discount% to zero if the discount% is greater than 100.
                if (discountPercentage > 100)
                {
                    discountPercentage = 0;
                    textBox.SetValue(TextBox.TextProperty, "0");
                    //#alert the discount percentage should be number text box having value from 0 to 100
                }
                // Computing the discount.
                discount = (costPrice * discountPercentage) / 100;
                // Setting the value of discount text box control.
                TextBox DiscountControl = parent.Children.OfType<TextBox>().First(x => x.Name.Equals(DISCOUNT));
                DiscountControl.SetValue(TextBox.TextProperty, discount.ToString());
            }
           
            // Setting the value of selling price control.
            decimal sellingPrice = costPrice - discount;
            TextBlock SellingPriceControl = parent.Children.OfType<TextBlock>().First(x => x.Name.Equals(SELLINGPRICE));
            SellingPriceControl.SetValue(TextBlock.TextProperty, Math.Round(sellingPrice,2).ToString()+ "\u20B9");
            // Setting the value of net value control.
            decimal netValue = sellingPrice * quantity;
            TextBlock NetValueControl = parent.Children.OfType<TextBlock>().First(x => x.Name.Equals(NETVALUE));
            NetValueControl.SetValue(TextBlock.TextProperty, Math.Round(netValue,2).ToString()+ "\u20B9");         
        }

        private void _temp_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = sender as TextBox;
            Object xt = t.GetValue(TextBox.TextProperty);
            t.SetValue(TextBox.TextProperty, "100");
            foreach (var item in BillingListView.Items)
            {
                var container = BillingListView.ContainerFromItem(item);
                var children = Utility.AllChildren(container);

                TextBox DiscountPercentageControl = children.OfType<TextBox>().First(x => x.Name.Equals(DISCOUNTPERCENTAGE));
                DiscountPercentageControl.LostFocus += DiscountPercentage_LostFocus;
                TextBox Discount = children.OfType<TextBox>().First(x => x.Name.Equals(DISCOUNT));
                Discount.LostFocus += Discount_LostFocus;
            }
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
