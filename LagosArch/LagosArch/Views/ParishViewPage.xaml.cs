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
    public partial class ParishViewPage : ContentPage
    {

        ParishViewModel viewModel;
        public ParishViewPage(Parish parish)
        {
            InitializeComponent();
            BindingContext = viewModel = new ParishViewModel(parish);
        }
    }
}