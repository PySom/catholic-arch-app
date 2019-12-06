using LagosArch.Models;
using LagosArch.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LagosArch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleNewsPage : ContentPage
    {
        NewsDetailViewModel viewModel;
        public SingleNewsPage(int id, ObservableCollection<News> news)
        {
            InitializeComponent();
            BindingContext = viewModel = new NewsDetailViewModel(id, news);
        }

        async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection?.FirstOrDefault() == null)
                return;
            News item = e.CurrentSelection.FirstOrDefault() as News;
            await Navigation.PushAsync(new SingleNewsPage(item.Id, viewModel.NewsFromParent));

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