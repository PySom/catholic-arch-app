using LagosArch.Customs;
using LagosArch.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LAEntry), typeof(LAEntryRenderer))]
namespace LagosArch.iOS.Renderers
{
    public class LAEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // do whatever you want to the UITextField here!
                Control.BorderStyle = UITextBorderStyle.RoundedRect;
                Control.TintColor = UIColor.FromRGBA(123, 123, 123, 10);
            }
        }
    }
}