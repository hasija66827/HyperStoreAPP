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
            new Scenario() { SymbolIcon=new SymbolIcon(Symbol.Calculator), Title = "Customer Billing", ClassType = typeof(CustomerProductListCC) },
            new Scenario() { SymbolIcon=new SymbolIcon(Symbol.People), Title = "Customers", ClassType = typeof(CustomersCCF) },
            new Scenario() { SymbolIcon=new SymbolIcon(Symbol.GoToToday),Title = "Customer Orders", ClassType = typeof(CustomerOrderListCCF) },
            new Scenario() { SymbolIcon=new SymbolIcon(Symbol.Home), Title = "Products", ClassType = typeof(ProductInStock) },
            new Scenario() {SymbolIcon=new SymbolIcon(Symbol.Calculator), Title = "Supplier Billing", ClassType = typeof(SupplierPurchasedProductListCC) },
            new Scenario() { SymbolIcon=new SymbolIcon(Symbol.People),Title = "Suppliers", ClassType = typeof(SupplierCCF) },
            new Scenario() {SymbolIcon=new SymbolIcon(Symbol.GoToToday), Title = "Supplier Orders", ClassType = typeof(SupplierOrderCCF) },
        };
    }

    public class Scenario
    {
        public SymbolIcon SymbolIcon { get; set; }
        public string Title { get; set; }
        public Type ClassType { get; set; }
    }
}
