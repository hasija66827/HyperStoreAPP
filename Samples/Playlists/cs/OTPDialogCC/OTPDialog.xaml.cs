﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OTPDialogCC : ContentDialog
    {
        private bool _IsVerified;
        public bool IsVerified { get { return this._IsVerified; } }
        private string _InputOTP { get; set; }
        private string _GeneratedOTP { get; set; }

        public OTPDialogCC(string mobileNumber, string generatedOTP)
        {
            this.InitializeComponent();
            TitleTB.Text = mobileNumber;
            _GeneratedOTP = generatedOTP;
            //TODO: Remove it when we have SMS API working
            _InputOTP = "123456";
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            _IsVerified = false;
            this.Hide();
        }

        private void VerifyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_InputOTP == _GeneratedOTP)
            {
                _IsVerified = true;
                this.Hide();
            }
            else
            {
                _IsVerified = false;
                ErrorTB.Text = "Invalid OTP";
            }
        }
    }
}
