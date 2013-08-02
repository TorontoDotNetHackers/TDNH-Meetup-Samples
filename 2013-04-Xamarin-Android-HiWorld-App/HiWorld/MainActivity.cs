using System;
using System.Xml;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HiWorld
{
	[Activity (Label = "HiWorld", MainLauncher = true)]
	public class Activity1 : Activity
	{
		int count = 1;
		string[] items;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);		

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			items = new string[] { "Vegetables","Fruits","Flower Buds","Legumes","Bulbs","Tubers" };
			ArrayAdapter la = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);						
			ListView lview = FindViewById<ListView> (Resource.Id.listView1);
			lview.Adapter = la;

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);

				ProgressBar pbar1 = FindViewById<ProgressBar> (Resource.Id.progressBar1);
				pbar1.Visibility = ViewStates.Visible;

			};

			ProgressBar pbar = FindViewById<ProgressBar> (Resource.Id.progressBar1);
			pbar.Visibility = ViewStates.Gone;

		}
	}
}


