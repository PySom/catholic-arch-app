using Android.Content;
using Android.Graphics.Drawables;
using LagosArch.Customs;
using LagosArch.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LAEntry), typeof(LAEntryRenderer))]

namespace LagosArch.Droid.Renderers
{
    public class LAEntryRenderer : EntryRenderer
    {
        public LAEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.White);
                gd.SetCornerRadius(10);
                gd.SetStroke(2, Android.Graphics.Color.LightGray);
                Control.Background = gd;
            }
        }
    }

}