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

using SDKTemplate.Settings;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SDKTemplate
{
    public partial class MainPage : Page
    {
        public const string FEATURE_NAME = "Hyper Store";

        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { FontIcon=new FontIcon(){ Glyph="\uE7BF" }, Title = "Customer Billing", ClassType = typeof(CustomerProductListCC) },
            new Scenario() { FontIcon=new FontIcon(){ Glyph="\uE125"}, Title = "Customers", ClassType = typeof(CustomersCCF) },
            new Scenario() { FontIcon=new FontIcon(){ Glyph="\uE912"}, Title = "Customer Orders", ClassType = typeof(CustomerOrderListCCF) },
            new Scenario() { FontIcon=new FontIcon(){ Glyph="\uE80F"}, Title = "Products", ClassType = typeof(ProductInStock) },
            new Scenario() {FontIcon=new FontIcon(){ Glyph="\uE7BF"}, Title = "Supplier Billing", ClassType = typeof(SupplierPurchasedProductListCC) },
            new Scenario() { FontIcon=new FontIcon(){ Glyph="\uE125"},Title = "Suppliers", ClassType = typeof(SupplierCCF) },
            new Scenario() {FontIcon=new FontIcon(){ Glyph="\uE912"}, Title = "Supplier Orders", ClassType = typeof(SupplierOrderCCF) },
            new Scenario() {FontIcon=new FontIcon(){ Glyph="\uE713"}, Title = "Settings", ClassType = typeof(SettingsCC) },
        };
    }

    public class Scenario
    {
        public SymbolIcon SymbolIcon { get; set; }
        public FontIcon FontIcon { get; set; }
        public string Title { get; set; }
        public Type ClassType { get; set; }
    }
}
