using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Volndear
{
	[Activity(Label = "Citizen Registration")]
	public class CitizenRegistrationActivity : Activity
	{
		EditText DateofBirth;
		TextView Birthdate;
		Button Register;
		EditText CitizenName;
		EditText CitizenAge;
		EditText NewPassword;
		EditText ConfirmPassword;
		EditText CitizenDateOfBirth;
		EditText CitizenMobileNumber;
		EditText CitizenEmailID;
		Citizen citizenEntity = new Citizen();
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Droid.Resource.Layout.CitizenRegistration);
			// Create your application here
			CitizenName = FindViewById<EditText>(Droid.Resource.Id.etxtPersonName);
			CitizenAge = FindViewById<EditText>(Droid.Resource.Id.etxtAge);
			CitizenDateOfBirth = FindViewById<EditText>(Droid.Resource.Id.etxtdate);
			CitizenMobileNumber = FindViewById<EditText>(Droid.Resource.Id.etxtMobileNumber);
			CitizenEmailID = FindViewById<EditText>(Droid.Resource.Id.etxtEmailAddress);
			DateofBirth = FindViewById<EditText>(Droid.Resource.Id.etxtdate);
			Register = FindViewById<Button>(Droid.Resource.Id.btnCtznRegister);
			NewPassword = FindViewById<EditText>(Droid.Resource.Id.etxtPassword);
			ConfirmPassword = FindViewById<EditText>(Droid.Resource.Id.etxtConfirmPassword);

			DateofBirth.Click += (sender, e) =>
			{
				DateTime today = DateTime.Today;
				DatePickerDialog dialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
				dialog.DatePicker.MinDate = today.Millisecond;
				dialog.Show();
			};
			DateofBirth.SetOnKeyListener(null);

			Birthdate = FindViewById<TextView>(Droid.Resource.Id.tvwBirthdate);

			Register.Click += delegate
			{
				if (NewPassword.Text == ConfirmPassword.Text)
				{
					citizenEntity.CitizenName = CitizenName.Text;
					citizenEntity.CitizenAge = CitizenAge.Text;
					citizenEntity.CitizenMobileNumber = CitizenMobileNumber.Text;
					citizenEntity.CitizenEmailId = CitizenEmailID.Text;
					citizenEntity.CitizenDOB = CitizenDateOfBirth.Text;
					citizenEntity.Password = ConfirmPassword.Text;
					AppDatabase.citizenRegistrationDatabase.InsertCitizen(citizenEntity);
				}
				else
				{
					Toast.MakeText(this, "Passwords didnt match", ToastLength.Short).Show();
				}
			};
		}

		void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
		{
			DateofBirth.Text = e.Date.ToLongDateString();
		}






	}


}
