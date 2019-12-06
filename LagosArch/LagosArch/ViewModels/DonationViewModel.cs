using LagosArch.Models;
using LagosArch.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LagosArch.ViewModels
{
    public class DonationViewModel : BaseViewModel
    {
        public IManager<Donation> DonationStore => DependencyService.Get<IManager<Donation>>();
        public Donation Interest { get; set; }
        string image = String.Empty;
        public string Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }
        string content = String.Empty;
        public string Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }
        decimal amount = 100;
        public decimal Amount
        {
            get { return amount; }
            set
            {
                SetProperty(ref amount, value);
                IsValid = Checker();
            }
        }
        string name = String.Empty;
        public string Name
        {
            get { return name; }
            set { 
                SetProperty(ref name, value);
                IsValid = Checker();
            }
        }

        string email = String.Empty;
        public string Email
        {
            get { return email; }
            set { 
                SetProperty(ref email, value);
                IsValid = Checker();
            }
        }

        string phone = String.Empty;
        public string Phone
        {
            get { return phone; }
            set { 
                SetProperty(ref phone, value);
                IsValid = Checker();
            }
        }

        bool isValid;
        public bool IsValid
        {
            get { return isValid; }
            set { SetProperty(ref isValid, value); }
        }

        public ICollection<Donation> Donations { get; set; }
        public ObservableCollection<string> DonationTitles { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command ItemSelectedCommand { get; set; }

        public DonationViewModel()
        {
            Title = "Donation";
            Interest = new Donation();
            Donations = new List<Donation>();
            DonationTitles = new ObservableCollection<string>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemSelectedCommand = new Command((object title) => ExecuteItemSelectedCommand(title));
        }

        bool Checker()
        {
            return !(String.IsNullOrEmpty(Phone) || String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(Name));
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Donations.Clear();
                Image = String.Empty;
                Content = String.Empty;
                DonationTitles.Clear();
                var items = await DonationStore.GetItemsAsync("api/donations", true);
                if (items != null && items.Count() > 0)
                {
                    foreach (var item in items)
                    {
                        Donations.Add(item);
                        DonationTitles.Add(item.Title);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        void ExecuteItemSelectedCommand(object obj)
        {
            if(obj is string title)
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                Interest = Donations.Where(d => d.Title == title).FirstOrDefault();
                Title = Interest.Title;
                Image = AppConstant.BaseUrl + Interest.Image;
                Content = Interest.Content;
                IsBusy = false;
            }
            
        }
    }
}
