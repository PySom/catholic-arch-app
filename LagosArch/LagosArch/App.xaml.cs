using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LagosArch.Services;
using LagosArch.Views;
using LagosArch.Models;

namespace LagosArch
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<ModelManager<News>>();
            DependencyService.Register<ModelManager<Reflection>>();
            DependencyService.Register<ModelManager<Donation>>();
            DependencyService.Register<ModelManager<Deanery>>();
            
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
