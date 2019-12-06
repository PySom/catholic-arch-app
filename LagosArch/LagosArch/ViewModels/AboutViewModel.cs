using LagosArch.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;

using Xamarin.Forms;

namespace LagosArch.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string Address { get; } 
        public string Email { get; }
        public HtmlWebViewSource Map { get; set; }
        readonly EmailService _email = new EmailService();
        private readonly string mapField = @"<html><body>
                                      <div>
                                            <iframe src='https://maps.google.com/maps?q=Holy%20Cross%20Cathedral%20&amp;t=&amp;z=13&amp;ie=UTF8&amp;iwloc=&amp;output=embed' width='600' height='300' frameborder='0' style='border:0' allowfullscreen></iframe>
                                        </div>
                                      </body></html>";
        public AboutViewModel()
        {
            Title = "About";
            Address = @"<p title='address'>
                            <address>Catholic Archdiocese of Lagos</address><br/>
                            <address>19, Catholic Mission Street</address><br/>
                            <address>P.O. Box 8, Marina GPO</address><br/>
                            <address>Lagos Nigeria</address><br/>
                            <address title='tel'>P: 08038714611</address><br/>
                        </p>";
            Email = "contact@infomall.ng";
            Map = new HtmlWebViewSource
            {
                Html = mapField
            };
            OpenEmailCommand = new Command( async () => await _email.SendEmail("All to Jesus", "", new List<string> { Email }));
        }

        public ICommand OpenWebCommand { get; }
        public ICommand OpenEmailCommand { get; }

        
    }
}