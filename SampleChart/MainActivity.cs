using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using MikePhil.Charting.Charts;

namespace SampleChart
{
    //[Activity(Label = "SampleChart", MainLauncher = true, Icon = "@drawable/icon")]
    [Activity(Label = "SampleChart")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            MikePhil.Charting.Util.Utils.Init(this);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
			
            button.Click += delegate
            {
                button.Text = string.Format("{0} clicks!", count++);

                    var i = new Intent(this, typeof(PieChartActivity));
                    StartActivity(i);
            };
        }
    }
}


