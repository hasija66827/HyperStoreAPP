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
using System.Threading.Tasks;
using Windows.Media.Playlists;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;

namespace SDKTemplate
{
    public partial class MainPage : Page
    {
        public const string FEATURE_NAME = "Hyper Store";

        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { Title = "Customer Billing", ClassType = typeof(CustomerProductListCC) },
            new Scenario() { Title = "Customer Orders", ClassType = typeof(CustomerOrderListCCF) },
            new Scenario() { Title = "Customers", ClassType = typeof(CustomersCCF) },
            new Scenario() { Title = "Products", ClassType = typeof(ProductInStock) },
            new Scenario() { Title = "Suppliers", ClassType = typeof(WholeSellersCCF) },
            new Scenario() { Title = "Supplier Billing", ClassType = typeof(SupplierPurchasedProductListCC) },
            new Scenario() { Title = "Supplier Orders", ClassType = typeof(WholeSellerOrderCC) },
        };
    }

    public class Scenario
    {
        public string Title { get; set; }
        public Type ClassType { get; set; }
    }
}
