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
    public enum ScenarioType
    {
        CustomerBilling,
        Customers,
        CustomerOrder,
        Products,
        SupplierBilling,
        Suppliers,
        SupplierOrder,
        Settings
    }

    public partial class MainPage : Page
    {
        public const string FEATURE_NAME = "Hyper Store";

        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { FontIcon=new FontIcon(){ Glyph="\uE7BF" }, Title = "Customer Billing", ScenarioType=ScenarioType.CustomerBilling },
            new Scenario() { FontIcon=new FontIcon(){ Glyph="\uE125"}, Title = "Customers",ScenarioType=ScenarioType.Customers },
            new Scenario() { FontIcon=new FontIcon(){ Glyph="\uE912"}, Title = "Customer Orders", ScenarioType=ScenarioType.CustomerOrder },
            new Scenario() { FontIcon=new FontIcon(){ Glyph="\uE80F"}, Title = "Products", ScenarioType=ScenarioType.Products },
            new Scenario() {FontIcon=new FontIcon(){ Glyph="\uE7BF"}, Title = "Supplier Billing", ScenarioType = ScenarioType.SupplierBilling },
            new Scenario() { FontIcon=new FontIcon(){ Glyph="\uE125"},Title = "Suppliers", ScenarioType=ScenarioType.Suppliers },
            new Scenario() {FontIcon=new FontIcon(){ Glyph="\uE912"}, Title = "Supplier Orders", ScenarioType=ScenarioType.SupplierOrder },
            new Scenario() {FontIcon=new FontIcon(){ Glyph="\uE713"}, Title = "Settings", ScenarioType=ScenarioType.Settings },
        };
    }

    public class Scenario
    {
        public FontIcon FontIcon { get; set; }
        public string Title { get; set; }
        public ScenarioType ScenarioType { get; set; }
    }
}
