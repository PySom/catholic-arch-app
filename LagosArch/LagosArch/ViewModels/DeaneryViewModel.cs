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
    public class DeaneryViewModel : BaseViewModel
    {
        public IManager<Deanery> DeaneryStore => DependencyService.Get<IManager<Deanery>>();

        ObservableCollection<Deanery> deaneries;
        public List<Deanery> dummyDeaneries;

        List<DeaneryGroup> deaneryGroups;
        public List<DeaneryGroup> DeaneryGroups
        {
            get { return deaneryGroups; }
            set { SetProperty(ref deaneryGroups, value); }
        }

        public ObservableCollection<Deanery> Deaneries
        {
            get { return deaneries; }
            set { SetProperty(ref deaneries, value); }
        }
        public Command LoadItemsCommand { get; set; }
        public DeaneryViewModel()
        {
            Title = "Deaneries";
            dummyDeaneries = new List<Deanery>();
            DeaneryGroups = new List<DeaneryGroup>();
            Deaneries = new ObservableCollection<Deanery>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var items = await DeaneryStore.GetItemsAsync("api/deaneries", true);
                if (items != null && items.Count() > 0)
                {
                    List<DeaneryGroup> dummy = new List<DeaneryGroup>();
                    foreach (var item in items)
                    {
                        List<Parish> parishes = item.Parishes.Select(p =>
                        {
                            p.Image = AppConstant.BaseUrl + p.Image;
                            return p;
                        }).ToList();
                        dummy.Add(new DeaneryGroup(item.Name, new List<Parish>()));
                        item.Parishes = parishes.ToList();
                        dummyDeaneries.Add(item);
                    }
                    DeaneryGroups = dummy;
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
