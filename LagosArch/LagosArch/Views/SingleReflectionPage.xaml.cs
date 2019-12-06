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
    public partial class SingleReflectionPage : ContentPage
    {
        ReflectionDetailViewModel viewModel;
        public SingleReflectionPage(int id, ObservableCollection<Reflection> reflections)
        {
            InitializeComponent();
            BindingContext = viewModel = new ReflectionDetailViewModel(id, reflections);
        }

        async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection?.FirstOrDefault() == null)
                return;
            Reflection item = e.CurrentSelection.FirstOrDefault() as Reflection;
            await Navigation.PushAsync(new SingleReflectionPage(item.Id, viewModel.ReflectionsFromParent));

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