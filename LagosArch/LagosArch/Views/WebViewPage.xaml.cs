using RaveDotNet;
using RaveDotNet.Models.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LagosArch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebViewPage : ContentPage
    {
        public WebViewPage(string uri, string title, RaveService raveService)
        {
            InitializeComponent();
            HtmlWebViewSource html = new HtmlWebViewSource
            {
                Html = uri
            };
            webView.Source = html;
            Title = title;

            raveService.SuccessEvent += ((object sender, SuccessEventArgs e) => {
                //triggered on a successful payment
            });

            raveService.FailedEvent += ((object sender, FailedEventArgs e) => {
                //triggered on a failed payment
            });


            raveService.InitEvent += ((object sender, InitEventArgs e) => {
                //triggered when you render html
            });

            raveService.RequeryEvent += ((object sender, RequeryEventArgs e) => {
                //triggered when a verification (requery) request is created
            });

            raveService.RequeryErrorEvent += ((object sender, RequeryErrorEventArgs e) => {
                //triggered when verification fails
            });

            raveService.TimeoutEvent += ((object sender, TimeoutEventArgs e) => {
                //triggered during verification request timeout
            });
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                string result = await webView.EvaluateJavaScriptAsync($"getTransactionString()");
                if (!string.IsNullOrEmpty(result))
                {
                    var a = result;
                    //
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("{0}", ex);
            }
            
        }
    }
}