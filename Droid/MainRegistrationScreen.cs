
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Volndear.Droid
{
	[Activity(Label = "MainRegistrationScreen")]
	public class MainRegistrationScreen : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.MainRegistration);
			// Create your application here
			Button CitizenRegistration = FindViewById<Button>(Droid.Resource.Id.btnCitizenRegistration);
			CitizenRegistration.Click += delegate
			{
				var activity2 = new Intent(this, typeof(CitizenRegistrationActivity));
				//activity2.PutExtra(loginid,password);
				StartActivity(activity2);

			};

		}
	}
}
