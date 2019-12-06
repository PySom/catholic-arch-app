using LagosArch.Extensions;
using LagosArch.Models;
using LagosArch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LagosArch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeaneryPage : ContentPage
    {
        DeaneryViewModel viewModel;
        public DeaneryPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new DeaneryViewModel();
        }

        async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection?.FirstOrDefault() == null)
                return;

            Parish item = e.CurrentSelection.FirstOrDefault() as Parish;
            await Navigation.PushAsync(new ParishViewPage(item));
            // Manually deselect item.
            ((CollectionView)sender).SelectedItem = null;
        }

        void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            var tappedSender = (StackLayout)sender;
            
            dynamic label = tappedSender.Children[0];
            string labelText = label.Text;
            var deaner = viewModel.dummyDeaneries.Where(d => d.Name == labelText).FirstOrDefault();
            //DeaneryGroup deanery = viewModel.DeaneryGroups.Where(d => d.Name == labelText).FirstOrDefault();
            viewModel.dummyDeaneries = viewModel.dummyDeaneries.Select(d => d.Name == labelText ? new Deanery { Id = d.Id, IsSet = !d.IsSet, Name = d.Name, Parishes = d.Parishes } : d).ToList();
            DeaneryGroup dump1 = new DeaneryGroup(deaner.Name, !deaner.IsSet ? deaner.Parishes.ToList() : new List<Parish>());

            //var dump = deanery.Select(d => new DeaneryGroup(
            //                            d.Name,
            //                            deanery.Parishes.
            //                                Select(p => {
            //                                    var pE = p;
            //                                    pE.IsVisible = !p.IsVisible;
            //                                    return pE;
            //                                }).ToList()
            //                             )).ToList();
            //viewModel.Deaneries = viewModel.Deaneries.Select(d => d.Id == item.Id ? item : d).ToObservableCollection();
            if (viewModel.DeaneryGroups != null && viewModel.DeaneryGroups.Count() > 0)
            {
                var dummy = viewModel.DeaneryGroups.Select(d => d.Name == deaner.Name ? dump1 : new DeaneryGroup(d.Name, new List<Parish>())).ToList();
                viewModel.DeaneryGroups = dummy;
                var item = viewModel.DeaneryGroups.Where(d => d.Name == labelText).FirstOrDefault();
                DenearyList.ScrollTo(item, animate: false, position: ScrollToPosition.Start);
            }

            // watch the monkey go from color to black&white!
        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.DeaneryGroups.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(null);
            }

        }
    }

    
}