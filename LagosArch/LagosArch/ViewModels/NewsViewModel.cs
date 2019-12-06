using LagosArch.Models;
using LagosArch.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace LagosArch.ViewModels
{
    class NewsViewModel : BaseViewModel
    {
        public IManager<News> NewsStore => DependencyService.Get<IManager<News>>();

        string mainTitle = String.Empty;
        public string MainTitle
        {
            get { return mainTitle; }
            set { SetProperty(ref mainTitle, value); }
        }

        string date = String.Empty;
        public string Date
        {
            get { return date; }
            set { SetProperty(ref date, value); }
        }

        string image = String.Empty;
        public string Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }

        string brief = String.Empty;
        public string Brief
        {
            get { return brief; }
            set { SetProperty(ref brief, value); }
        }
        public ObservableCollection<News> News { get; set; }
        public Command LoadItemsCommand { get; set; }

        public NewsViewModel()
        {
            Title = "News";
            News = new ObservableCollection<News>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                News.Clear();
                var items = await NewsStore.GetItemsAsync("api/news", true);
                if(items != null && items.Count() > 0)
                {
                    var firstItem = items.Last();
                    MainTitle = firstItem.Title;
                    Brief = firstItem.Brief;
                    Date = firstItem.DatePosted.ToLongDateString();
                    Image = AppConstant.BaseUrl + firstItem.Image;
                    
                    foreach (var item in items)
                    {
                        item.Image = AppConstant.BaseUrl + item.Image;
                        item.Date = firstItem.DatePosted.ToLongDateString();
                        News.Add(item);
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
    }
}
