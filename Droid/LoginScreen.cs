
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
using Volndear.Droid;

namespace Volndear.Droid
{
	[Activity(Label = "Login")]
	public class LoginScreen : Activity
	{
		Citizen citizen = new Citizen();
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Login);
			// Create your application her.

			string loginid = string.Empty;
			string password = string.Empty;
			EditText txtLoginID = FindViewById<EditText>(Droid.Resource.Id.txtLoginId);
			EditText txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
			Button RegisterButton = FindViewById<Button>(Resource.Id.btnRegister);
			Button SignInButton = FindViewById<Button>(Resource.Id.btnSignIn);

			loginid = txtLoginID.Text;
			password = txtPassword.Text;
			RegisterButton.Click += delegate
			{
				var activity2 = new Intent(this, typeof(MainRegistrationScreen));
				//activity2.PutExtra(loginid,password);
				StartActivity(activity2);
			};

			SignInButton.Click += delegate
			{
				citizen.CitizenName = txtLoginID.Text;
				citizen.Password = txtPassword.Text;
				AppDatabase.citizenRegistrationDatabase.InsertCitizen(citizen);
				txtLoginID.Text = string.Empty;
				txtPassword.Text = string.Empty;
				Citizen citi = new Citizen();
				citi = AppDatabase.citizenRegistrationDatabase.GetCitizen();
				List<Citizen> ct = new List<Citizen>();
				ct = AppDatabase.citizenRegistrationDatabase.GetAllCitizen();
			};

			// Create your application here
		}
	}
}
