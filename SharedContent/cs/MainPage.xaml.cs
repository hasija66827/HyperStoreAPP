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

using Models;
using SDKTemplate.BasePages;
using SDKTemplate.DTO;
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
            if (BaseURI.User.IsRegisteredUser)
                UnregisteredUserTB.Visibility = Visibility.Collapsed;
            // This is a static public property that allows downstream pages to get a handle to the MainPage instance
            // in order to call methods that are in this class.
            Current = this;
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            QuickCreateSplitter.IsPaneOpen = true;
            EntityFrame.Navigate(typeof(PersonFormCC), EntityType.Customer);
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            QuickCreateSplitter.IsPaneOpen = true;
            EntityFrame.Navigate(typeof(ProductBasicFormCC), null);
        }

        private void AddWholeSellerBtn_Click(object sender, RoutedEventArgs e)
        {
            QuickCreateSplitter.IsPaneOpen = true;
            EntityFrame.Navigate(typeof(PersonFormCC), EntityType.Supplier);
        }

        public void UpdateCustomer(Person customer)
        {
            QuickCreateSplitter.IsPaneOpen = true;
            EntityFrame.Navigate(typeof(PersonFormCC), customer);
        }

        public void UpdateSupplier(Person supplier)
        {
            QuickCreateSplitter.IsPaneOpen = true;
            EntityFrame.Navigate(typeof(PersonFormCC), supplier);
        }

        //TODO: Bad coding practice.
        public void CloseSplitPane()
        {
            QuickCreateSplitter.IsPaneOpen = false;
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
        public void ActivateProgressRing()
        {
            MainFrame.IsEnabled = false;
            progressRing.IsActive = true;
        }

        public void DeactivateProgressRing()
        {
            progressRing.IsActive = false;
            MainFrame.IsEnabled = true;
        }

        /// <summary>
        /// Called whenever the user changes selection in the scenarios list.  This method will navigate to the respective
        /// sample scenario page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScenarioControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox scenarioListBox = sender as ListBox;
            Scenario s = scenarioListBox.SelectedItem as Scenario;
            if (s != null)
                RefreshPage(s.ScenarioType);
        }

        public List<Scenario> Scenarios
        {
            get { return this.scenarios; }
        }

        public static void RefreshPage(ScenarioType scenarioType)
        {
            if (scenarioType == ScenarioType.Logout)
            {
                RootPage.Current.Navigate(typeof(Login.LoginCC));
            }
            else if (scenarioType == ScenarioType.Settings)
            {
                Current.MainFrame.Navigate(typeof(Settings.SettingsCC));
            }
            else if (scenarioType == ScenarioType.CustomerBilling || scenarioType == ScenarioType.SupplierBilling)
            {
                Current.MainFrame.Navigate(typeof(BillingPage), scenarioType);
            }
            else if (scenarioType == ScenarioType.Dashboard)
            {
                Current.MainFrame.Navigate(typeof(Dashboard));
            }
            else if (scenarioType == ScenarioType.Products)
            {
                Current.MainFrame.Navigate(typeof(ProductPage));
            }
            else
            {
                Current.MainFrame.Navigate(typeof(CommonPage), scenarioType);
            }
            if (Window.Current.Bounds.Width < 640)
            {
                Current.Splitter.IsPaneOpen = false;
            }
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
                //UpdateStatus(strMessage, type);
            }
            else
            {
                //TODO: Take care of this.
                // var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => UpdateStatus(strMessage, type));
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
