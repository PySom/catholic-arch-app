using LagosArch.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LagosArch.ViewModels
{
    public class ParishViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; set; }
        public Command ViewMapCommand { get; set; }
        public string Confession { get; set; }
        public string Mass { get; set; }

        public ParishViewModel(Parish parish)
        {
            //Parish = parish;
            parish.Confessions = parish.Confessions.Select(c =>
            {
                var cE = c;
                cE.DayOfWeek = Enum.GetName(typeof(DayOfWeek), cE.Day);
                cE.EndTimeString = c.EndTime.ToLongTimeString();
                cE.StartTimeString = c.StartTime.ToLongTimeString();
                return cE;
            }).ToList();
            parish.Masses = parish.Masses.Select(m =>
            {
                var mE = m;
                mE.DayOfWeek = Enum.GetName(typeof(DayOfWeek), mE.Day);
                mE.EndTimeString = m.EndTime.ToLongTimeString();
                mE.StartTimeString = m.StartTime.ToLongTimeString();
                return mE;
            }).ToList();
            Parish = parish;
            Confession = Parish.Confessions.Count() > 0 ? "Confession Time" : "";
            Mass = Parish.Masses.Count() > 0 ? "Mass Time" : "";
            Title = Parish.Name;
            ViewMapCommand = new Command(async () => await ExecuteViewMapCommand());
        }
        Parish parish;
        public Parish Parish
        {
            get { return parish; }
            set { SetProperty(ref parish, value); }
        }

        
        async Task ExecuteViewMapCommand()
        {
            await Map.OpenAsync(Parish.Latitude, Parish.Longitude);
            
        }
    }
}
