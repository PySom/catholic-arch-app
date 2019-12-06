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
    public class ReflectionViewModel: BaseViewModel
    {
        public IManager<Reflection> ReflectionStore => DependencyService.Get<IManager<Reflection>>();

        public ObservableCollection<Reflection> Reflections { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ReflectionViewModel()
        {
            Reflections = new ObservableCollection<Reflection>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Reflections.Clear();
                var items = await ReflectionStore.GetItemsAsync("api/reflections", true);
                if (items != null && items.Count() > 0)
                {
                    foreach (var item in items)
                    {
                        item.Image = AppConstant.BaseUrl + item.Image;
                        item.Content = String.Join(" ", item.Content.Split(' ')) + "...";
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
