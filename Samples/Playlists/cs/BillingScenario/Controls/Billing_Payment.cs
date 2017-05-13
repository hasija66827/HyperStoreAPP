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

using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Media.Playlists;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using DatabaseModel;
namespace SDKTemplate
{
    public sealed partial class BillingScenario : Page
    { 
        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            PageNavigationParameter pageNavigationParameter = new PageNavigationParameter(
               this.BillingViewModel,
               BillingScenario.CustomerViewModel);
            this.Frame.Navigate(typeof(SelectPaymentMode), pageNavigationParameter);
        }
    }
}
