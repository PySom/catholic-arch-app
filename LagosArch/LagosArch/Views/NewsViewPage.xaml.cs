using LagosArch.Models;
using LagosArch.Services;
using LagosArch.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LagosArch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsViewPage : ContentPage
    {
        NewsViewModel viewModel;
        public NewsViewPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NewsViewModel();
        }

        async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection?.FirstOrDefault() == null)
                return;
            News item = e.CurrentSelection.FirstOrDefault() as News;
            await Navigation.PushAsync(new SingleNewsPage(item.Id, viewModel.News));

            // Manually deselect item.
            ((CollectionView)sender).SelectedItem = null;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.News.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(null);
            }
            
        }
    }
}
