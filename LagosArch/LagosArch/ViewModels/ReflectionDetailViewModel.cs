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
    class ReflectionDetailViewModel: BaseViewModel
    {

        public ObservableCollection<Reflection> Reflections { get; set; }
        public ObservableCollection<Reflection> ReflectionsFromParent { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ReflectionDetailViewModel(int id, ObservableCollection<Reflection> reflections)
        {
            Reflections = new ObservableCollection<Reflection>();
            ReflectionsFromParent = reflections;
            LoadItemsCommand = new Command(() => ExecuteLoadItemsCommand(id));
        }
        string mainTitle = String.Empty;
        public string MainTitle
        {
            get { return mainTitle; }
            set { SetProperty(ref mainTitle, value); }
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
        void ExecuteLoadItemsCommand(int id)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Reflections.Clear();
                var items = ReflectionsFromParent;
                if (items != null && items.Count() > 0)
                {
                    var firstItem = items.Where(n => n.Id == id).FirstOrDefault();
                    Title = firstItem.Title;
                    MainTitle = firstItem.Title;
                    Content = firstItem.Content;
                    Image = firstItem.Image;
                    RelatedNews = "Other Reflections";

                    foreach (var item in items.Where(n => n.Id != id))
                    {
                        item.Image = item.Image;
                        item.Content = String.Join(" ", firstItem.Content.Split(' ')) + "...";
                        Reflections.Add(item);
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
