using LagosArch.Models;
using LagosArch.Services;
using LagosArch.ViewModels;
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
    public partial class DonationPage : ContentPage
    {
        DonationViewModel viewModel;
        
        public DonationPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new DonationViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Donations.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(null);
            }

        }

        private void PickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                viewModel.ItemSelectedCommand.Execute(picker.ItemsSource[selectedIndex]);
            }
        }

        private async void PayWithRave(object sender, EventArgs e)
        {
            string[] names = viewModel.Name.Split(' ');
            (string firstName, string lastName) = names.Length > 2 ? (names[0], names[1]) : (names[0], "");
            RaveServiceManager payService = new RaveServiceManager();
            var raveService = payService.InitiatePay(new Payment
            {
                Amount = viewModel.Amount,
                CustomDescription = viewModel.Content,
                CustomerEmail = viewModel.Email,
                CustomerFirstname = firstName,
                CustomerLastname = lastName,
                CustomerPhone = viewModel.Phone,
                CustomTitle = viewModel.Title
            });
            string url = raveService.RenderHtml().Replace("ajax-loader.gif", "https://infomall.ng/images/489ajax-loader.gif");
            string title = "Donation for " + viewModel.Title;
            await Navigation.PushModalAsync(new NavigationPage(new WebViewPage(url, title, raveService)));

            raveService.SuccessEvent += ((object raveSender, SuccessEventArgs raveEvent) => {

                //triggered on a successful payment
                var r = raveEvent;
            });

            raveService.FailedEvent += ((object reveSender, FailedEventArgs eventArgs) => {
                //triggered on a failed payment
            });


            raveService.InitEvent += ((object reveSender, InitEventArgs eventArgs) => {
                //triggered when you render html
            });

            raveService.RequeryEvent += ((object reveSender, RequeryEventArgs eventArgs) => {
                //triggered when a verification (requery) request is created
            });
        }


    }
}