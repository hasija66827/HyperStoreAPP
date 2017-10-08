﻿//*********************************************************
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

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            QuickCreateSplitter.IsPaneOpen = true;
            EntityFrame.Navigate(typeof(CustomerFormCC));
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            QuickCreateSplitter.IsPaneOpen = true;
            EntityFrame.Navigate(typeof(ProductBasicFormCC), null);
        }

        private void AddWholeSellerBtn_Click(object sender, RoutedEventArgs e)
        {
            QuickCreateSplitter.IsPaneOpen = true;
            EntityFrame.Navigate(typeof(SupplierFormCC));
        }

        public void UpdateCustomer(TCustomer customer)
        {
            QuickCreateSplitter.IsPaneOpen = true;
            EntityFrame.Navigate(typeof(CustomerFormCC), customer);
        }

        public void UpdateSupplier(TSupplier supplier)
        {
            QuickCreateSplitter.IsPaneOpen = true;
            EntityFrame.Navigate(typeof(SupplierFormCC), supplier);
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
            {
                if (s.ClassType == typeof(Settings.SettingsCC))
                {
                    MainFrame.Navigate(s.ClassType);
                }
                else
                {
                    MainFrame.Navigate(typeof(CommonPage), s);
                }
                if (Window.Current.Bounds.Width < 640)
                {
                    Splitter.IsPaneOpen = false;
                }
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
