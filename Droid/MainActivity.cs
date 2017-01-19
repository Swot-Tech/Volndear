using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using System.IO;
using Android;
using Volndear.Droid;

namespace Volndear
{
	[Activity(Label = "VOLNDEAR", MainLauncher = true)]
	public class MainActivity : Activity
	{

		System.Timers.Timer timer = new System.Timers.Timer();
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource);
			SetContentView(Droid.Resource.Layout.Main);
			timer.Interval = 2000;
			timer.Elapsed += OnTimedEvent;
			timer.Enabled = true;
		}
		private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
		{
			var LoginActivity = new Intent(this, typeof(LoginScreen));
			StartActivity(LoginActivity);
			timer.Enabled = false;
		}








	}


}

