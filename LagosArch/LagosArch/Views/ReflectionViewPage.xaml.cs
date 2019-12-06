using LagosArch.Models;
using LagosArch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LagosArch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReflectionViewPage : ContentPage
    {
        ReflectionViewModel viewModel;
        public ReflectionViewPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ReflectionViewModel();
        }

        async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection?.FirstOrDefault() == null)
                return;
            Reflection item = e.CurrentSelection.FirstOrDefault() as Reflection;
            await Navigation.PushAsync(new SingleReflectionPage(item.Id, viewModel.Reflections));

            // Manually deselect item.
            ((CollectionView)sender).SelectedItem = null;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Reflections.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(null);
            }

        }
    }
}