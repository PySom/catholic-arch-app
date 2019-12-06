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
    class NewsDetailViewModel : BaseViewModel
    {
        string mainTitle = String.Empty;

        HtmlWebViewSource html = new HtmlWebViewSource ();

        public HtmlWebViewSource HtmlContent
        {
            get { return html; }
            set { SetProperty(ref html, value); }
        }
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

        string relatedNews = String.Empty;
        public string RelatedNews
        {
            get { return relatedNews; }
            set { SetProperty(ref relatedNews, value); }
        }

        string content = String.Empty;
        public string Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }
        public ObservableCollection<News> News { get; set; }
        public ObservableCollection<News> NewsFromParent { get; set; }
        public Command LoadItemsCommand { get; set; }

        public NewsDetailViewModel(int id, ObservableCollection<News> news)
        {
            NewsFromParent = news;
            News = new ObservableCollection<News>();
            LoadItemsCommand = new Command(() => ExecuteLoadItemsCommand(id));
        }

        void ExecuteLoadItemsCommand(int id)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                News.Clear();
                var items = NewsFromParent;
                if (items != null && items.Count() > 0)
                {
                    var firstItem = items.Where(n => n.Id == id).FirstOrDefault();
                    MainTitle = firstItem.Title;
                    Title = firstItem.Title;
                    Content = firstItem.Content;
                    Date = firstItem.DatePosted.ToLongDateString();
                    Image = firstItem.Image;
                    RelatedNews = "Other news";

                    foreach (var item in items.Where(n => n.Id != id))
                    {
                        item.Image = item.Image;
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
