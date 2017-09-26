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

using SDKTemp.Data;
using System;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;
        public MainPage()
        {
            this.InitializeComponent();

            // This is a static public property that allows downstream pages to get a handle to the MainPage instance
            // in order to call methods that are in this class.
            Current = this;
            //SampleTitle.Text = FEATURE_NAME;
           
        }
        public void NavigateNewsFeedFrame(Type sourcePageType, object parameter)
        {
            NewsFeedFrame.Navigate(sourcePageType, parameter);
        }
        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            NewsFeedFrame.Navigate(typeof(CustomerFormCC));
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            NewsFeedFrame.Navigate(typeof(ProductBasicFormCC), null);
        }

        private void AddWholeSellerBtn_Click(object sender, RoutedEventArgs e)
        {
            NewsFeedFrame.Navigate(typeof(SupplierFormCC));
        }
        private void AddTagBtn_Click(object sender, RoutedEventArgs e)
        {
            NewsFeedFrame.Navigate(typeof(TagFormCC));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Populate the scenario list from the SampleConfiguration.cs file
            ScenarioControl.ItemsSource = scenarios;
            if (Window.Current.Bounds.Width < 640)
            {
                ScenarioControl.SelectedIndex = -1;
            }
            else
            {
                ScenarioControl.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Called whenever the user changes selection in the scenarios list.  This method will navigate to the respective
        /// sample scenario page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScenarioControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Clear the status block when navigating scenarios.
            NotifyUser(String.Empty, NotifyType.StatusMessage);

            ListBox scenarioListBox = sender as ListBox;
            Scenario s = scenarioListBox.SelectedItem as Scenario;
            if (s != null)
            {
                if (s.ClassType == typeof(CustomerProductListCC))
                {
                    HeaderFrame.Navigate(typeof(CustomerASBCC));
                    SearchBoxFrame.Navigate(typeof(ProductASBCC));
                    ScenarioFrame.Navigate(s.ClassType);
                    NewsFeedFrame.Navigate(typeof(RecommendedProductCC));
                    RightBottomFrame.Navigate(typeof(BillingSummaryCC));
                    SummaryFrame.Navigate(typeof(BlankPage));
                    ChangeLayout(false);
                }
                else if (s.ClassType == typeof(CustomerOrderListCCF))
                {
                    HeaderFrame.Navigate(typeof(CustomerASBCC));
                    SearchBoxFrame.Navigate(typeof(FilterOrderCC));
                    RightBottomFrame.Navigate(typeof(BlankPage));
                    ScenarioFrame.Navigate(s.ClassType);
                    NewsFeedFrame.Navigate(typeof(BlankPage));
                    SummaryFrame.Navigate(typeof(OrderSummaryCC));
                    ChangeLayout(true);

                }
                else if (s.ClassType == typeof(CustomersCCF))
                {
                    HeaderFrame.Navigate(typeof(CustomerASBCC));
                    SearchBoxFrame.Navigate(typeof(FilterPersonCC), Person.Customer);
                    ScenarioFrame.Navigate(typeof(CustomersCCF));
                    RightBottomFrame.Navigate(typeof(CustomerTrendCC));
                    NewsFeedFrame.Navigate(typeof(BlankPage));
                    SummaryFrame.Navigate(typeof(CustomerSummaryCC));
                    ChangeLayout(false);

                }
                else if (s.ClassType == typeof(ProductInStock))
                {
                    HeaderFrame.Navigate(typeof(ProductASBCC), ProductPage.SearchTheProduct);
                    SearchBoxFrame.Navigate(typeof(FilterProductByTagCC));
                    SummaryFrame.Navigate(typeof(FilterProductCC));
                    ScenarioFrame.Navigate(s.ClassType);
                    RightBottomFrame.Navigate(typeof(ProductConsumptionPer));
                    NewsFeedFrame.Navigate(typeof(BlankPage));
                    ChangeLayout(false);

                }
                else if (s.ClassType == typeof(SupplierCCF))
                {
                    HeaderFrame.Navigate(typeof(SupplierASBCC));
                    SearchBoxFrame.Navigate(typeof(FilterPersonCC), Person.Supplier);
                    RightBottomFrame.Navigate(typeof(BlankPage));
                    ScenarioFrame.Navigate(typeof(SupplierCCF));
                    NewsFeedFrame.Navigate(typeof(BlankPage));
                    SummaryFrame.Navigate(typeof(SupplierSummaryCC));
                    ChangeLayout(false);

                }
                else if (s.ClassType == typeof(SupplierPurchasedProductListCC))
                {
                    HeaderFrame.Navigate(typeof(SupplierASBCC));
                    SearchBoxFrame.Navigate(typeof(ProductASBCC));
                    ScenarioFrame.Navigate(typeof(SupplierPurchasedProductListCC));
                    NewsFeedFrame.Navigate(typeof(BlankPage));
                    RightBottomFrame.Navigate(typeof(SupplierBillingSummaryCC));
                    SummaryFrame.Navigate(typeof(BlankPage));
                    ChangeLayout(false);
                }

                else if (s.ClassType == typeof(SupplierOrderCCF))
                {
                    HeaderFrame.Navigate(typeof(SupplierASBCC));
                    SearchBoxFrame.Navigate(typeof(FilterSupplierOrderCC));
                    RightBottomFrame.Navigate(typeof(BlankPage));
                    ScenarioFrame.Navigate(typeof(SupplierOrderCCF));
                    NewsFeedFrame.Navigate(typeof(BlankPage));
                    SummaryFrame.Navigate(typeof(SupplierOrderSummary));
                    ChangeLayout(true);
                }
                else
                {
                    ScenarioFrame.Navigate(s.ClassType);
                }
                if (Window.Current.Bounds.Width < 640)
                {
                    Splitter.IsPaneOpen = false;
                }
            }
        }

        private void ChangeLayout(bool Expanded)
        {
            if (Expanded)
            {
                ScenarioFrame.Width = 1200;
                NewsFeedFrame.Visibility = Visibility.Collapsed;
                RightBottomFrame.Visibility = Visibility.Collapsed;
            }
            else
            {
                ScenarioFrame.Width = 830;
                NewsFeedFrame.Visibility = Visibility.Visible;
                RightBottomFrame.Visibility = Visibility.Visible;
            }
        }


        public List<Scenario> Scenarios
        {
            get { return this.scenarios; }
        }

        /// <summary>
        /// Display a message to the user.
        /// This method may be called from any thread.
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="type"></param>
        public void NotifyUser(string strMessage, NotifyType type)
        {
            // If called from the UI thread, then update immediately.
            // Otherwise, schedule a task on the UI thread to perform the update.
            if (Dispatcher.HasThreadAccess)
            {
                UpdateStatus(strMessage, type);
            }
            else
            {
                var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => UpdateStatus(strMessage, type));
            }
        }

        private void UpdateStatus(string strMessage, NotifyType type)
        {
            switch (type)
            {
                case NotifyType.StatusMessage:
                    StatusBorder.Background = new SolidColorBrush(Windows.UI.Colors.LawnGreen);
                    break;
                case NotifyType.ErrorMessage:
                    StatusBorder.Background = new SolidColorBrush(Windows.UI.Colors.PaleVioletRed);
                    break;
            }
            StatusBlock.Text = strMessage;

            // Collapse the StatusBlock if it has no text to conserve real estate.
            StatusBorder.Visibility = (StatusBlock.Text != String.Empty) ? Visibility.Visible : Visibility.Collapsed;
            if (StatusBlock.Text != String.Empty)
            {
                StatusBorder.Visibility = Visibility.Collapsed;
                StatusPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                StatusBorder.Visibility = Visibility.Collapsed;
                StatusPanel.Visibility = Visibility.Collapsed;
            }
        }

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Splitter.IsPaneOpen = !Splitter.IsPaneOpen;
        }


    }
    public enum NotifyType
    {
        StatusMessage,
        ErrorMessage
    };

    public class ScenarioBindingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Scenario s = value as Scenario;
            return "   " + s.Title;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return true;
        }
    }
    public class ScenarioSymbolBindingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Scenario s = value as Scenario;
            if (s.SymbolIcon != null)
            {
                return s.SymbolIcon.Symbol;
            }
            return Symbol.More;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return true;
        }
    }

    public class ScenarioFontIconBindingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Scenario s = value as Scenario;
            if (s.FontIcon != null)
            {
                return s.FontIcon.Glyph;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return true;
        }
    }

}
