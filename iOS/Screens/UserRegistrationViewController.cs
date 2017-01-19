using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Volndear.iOS
{
	public partial class UserRegistrationViewController : UIViewController
	{

		UIView vwContainer = new UIView();
		UIScrollView svDetails = new UIScrollView();
		UITextField txtCitizenName = new UITextField();
		UITextField txtPrimaryMobile = new UITextField();
		UITextField txtEmail = new UITextField();
		UITextView txtBaseAddress = new UITextView();
		UITextField txtBaseAddressDistrict = new UITextField();
		UITextField txtPinCode = new UITextField();
		UITextField txtDOB = new UITextField();
		UISegmentedControl segmentControl = new UISegmentedControl();
		UITextField txtNewPassword = new UITextField();
		UITextField txtConfirmPassword = new UITextField();
		Citizen citizenTable = new Citizen();
		private nfloat scroll_amount = 0.0f;    // amount to scroll 
		private float bottom = 0.0f;           // bottom point
		private float offset = 0.0f;          // extra offset
		private bool moveViewUp = false;
		int heightScroll = 3;
		bool isSupplement = false;
		UIBarButtonItem Save = new UIBarButtonItem();
		string gender = string.Empty;
		public UserRegistrationViewController() : base("UserRegistrationViewController", null)
		{
		}

		public override void ViewWillAppear(bool animated)
		{
			if (TabBarController != null)
			{
				this.TabBarController.TabBar.Hidden = true;
			}

			base.ViewWillAppear(animated);
		}

		public override void ViewWillDisappear(bool animated)
		{
			if (TabBarController != null)
			{
				this.TabBarController.TabBar.Hidden = false;
			}
			base.ViewWillDisappear(animated);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			//Keyboard Up notification
			NSNotificationCenter.DefaultCenter.AddObserver
		(UIKeyboard.DidShowNotification, KeyBoardUpNotification);
			//Keyboard down notification
			NSNotificationCenter.DefaultCenter.AddObserver
		(UIKeyboard.WillHideNotification, KeyBoardDownNotification);
			DismissKeyboardOnBackgroundTap();
			//Initializing done button for edit
			this.NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(UIBarButtonSystemItem.Save, (sender, args) =>
					{
				if(txtCitizenName.Text != string.Empty && txtPrimaryMobile.Text!= string.Empty && txtDOB.Text != string.Empty && txtEmail.Text != string.Empty)
						 if (txtNewPassword.Text == txtConfirmPassword.Text)
						{
							if (!isValidMobileNumber(txtPrimaryMobile.Text.Trim()))
							{
								using (var alert = new UIAlertView("Warning", "Please enter a valid  Mobile Number.", null, "OK", null))
									alert.Show();
							}
							else if (!isValidEmail(txtEmail.Text.Trim()))
							{
								using (var alert = new UIAlertView("Warning", "Please enter a Valid Email id.", null, "OK", null))
									alert.Show();
							}

							else {

								citizenTable.CitizenName = txtCitizenName.Text;
								citizenTable.CitizenMobileNumber = txtPrimaryMobile.Text;
								citizenTable.CitizenEmailId = txtEmail.Text;
								citizenTable.CitizenDOB = txtDOB.Text;
								citizenTable.Password = txtConfirmPassword.Text;
								citizenTable.CitizenAddress = txtBaseAddress.Text;
								citizenTable.CitizenDistrict = txtBaseAddressDistrict.Text;
								citizenTable.CitizenPincode = txtPinCode.Text;
								citizenTable.Status = 1;
								citizenTable.CitizenGender = gender;
								AppDelegate.citizenRegistrationDatabase.InsertCitizen(citizenTable);

								NavigationController.PushViewController(new HomeScreenViewController(), true);
								AppGlobal.isFromRegistrationScreen = true;

							}
						}

						else
						{
							using (var alert = new UIAlertView("Warning", "Password didn't match.", null, "OK", null))
								alert.Show();

						}

					})
				, true);




			// Perform any additional setup after loading the view, typically from a nib.

			configureCitizenRegistrationView();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

	

	
		private bool isValidMobileNumber(string number)
		{
			if (number != null)
			{
				if (number != string.Empty)
				{
					if (number.Length == 10)
					{
						return true;
					}
					else return false;
				}
				else return false;
			}
			else return false;
		}

		protected void DismissKeyboardOnBackgroundTap()
		{
			var tap = new UITapGestureRecognizer { CancelsTouchesInView = false };
			tap.AddTarget(() => View.EndEditing(true));
			svDetails.AddGestureRecognizer(tap);
		}

		private void configureCitizenRegistrationView()
		{

			try
			{
				nfloat labelTextBoxPadding = 0;
				nfloat PaddingBetweenTwoFields = 0;



				//To set max length 10
				const int maxCharacters = 10;
				var screen = UIScreen.MainScreen.Bounds;


				vwContainer.Frame = new CGRect(0, 0, screen.Width, screen.Height - 20);
				vwContainer.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("volndear_hands"));
				View.AddSubview(vwContainer);



				svDetails.Frame = new CGRect(5, 0, vwContainer.Frame.Width, vwContainer.Frame.Height);
				vwContainer.AddSubview(svDetails);

				UILabel lblCitizenName = new UILabel(new CGRect(0, 3, vwContainer.Frame.Width, 30));
				lblCitizenName.Text = "User Name";
				lblCitizenName.TextColor = UIColor.Black;
				lblCitizenName.TextAlignment = UITextAlignment.Left;
				lblCitizenName.Font = UIFont.FromName("Helvetica", 18);
				svDetails.AddSubview(lblCitizenName);


				txtCitizenName = new UITextField(new CGRect(0, lblCitizenName.Frame.Bottom + labelTextBoxPadding, vwContainer.Frame.Width - 10, 40));
				txtCitizenName.Font = UIFont.FromName("Helvetica", 18);
				txtCitizenName.TextColor = UIColor.Black;
				txtCitizenName.TextAlignment = UITextAlignment.Left;
				txtCitizenName.Layer.BorderWidth = 1.0f;
				txtCitizenName.Layer.BorderColor = UIColor.Black.CGColor;
				txtCitizenName.Layer.CornerRadius = 5;
				txtCitizenName.ClipsToBounds = true;
				txtCitizenName.Placeholder = "User Name";

				txtCitizenName.ShouldReturn += (textField) =>
				{
					textField.ResignFirstResponder();
					return true;
				};
				svDetails.AddSubview(txtCitizenName);

				UILabel lblMobileNumber = new UILabel(new CGRect(0, txtCitizenName.Frame.Bottom + PaddingBetweenTwoFields, txtCitizenName.Frame.Width, txtCitizenName.Frame.Height));
				lblMobileNumber.Text = "Mobile Number";
				lblMobileNumber.TextColor = UIColor.Black;
				lblMobileNumber.TextAlignment = UITextAlignment.Left;
				lblMobileNumber.Font = UIFont.FromName("Helvetica", 18);
				svDetails.AddSubview(lblMobileNumber);

				txtPrimaryMobile = new UITextField(new CGRect(0, lblMobileNumber.Frame.Bottom + labelTextBoxPadding, vwContainer.Frame.Width - 10, txtCitizenName.Frame.Height));

				txtPrimaryMobile.Font = UIFont.FromName("Helvetica", 18);
				txtPrimaryMobile.TextColor = UIColor.Black;
				txtPrimaryMobile.TextAlignment = UITextAlignment.Left;
				txtPrimaryMobile.Placeholder = "Mobile Number";
				txtPrimaryMobile.ShouldChangeCharacters = (textField, range, replacement) =>
				{
					var newContent = new NSString(textField.Text).Replace(range, new NSString(replacement)).ToString();
					int number;
					return newContent.Length <= maxCharacters && (replacement.Length == 0 || int.TryParse(replacement, out number));
				};
				txtPrimaryMobile.ShouldReturn += (textField) =>
				{
					textField.ResignFirstResponder();
					return true;
				};

				txtPrimaryMobile.Layer.BorderWidth = 1.0f;
				txtPrimaryMobile.Layer.BorderColor = UIColor.Black.CGColor;
				txtPrimaryMobile.Layer.CornerRadius = 5;
				txtPrimaryMobile.ClipsToBounds = true;
				svDetails.AddSubview(txtPrimaryMobile);

				UILabel lblEmail = new UILabel(new CGRect(0, txtPrimaryMobile.Frame.Bottom + PaddingBetweenTwoFields, txtPrimaryMobile.Frame.Width, txtPrimaryMobile.Frame.Height));
				lblEmail.Text = "Email ID";
				lblEmail.TextColor = UIColor.Black;
				lblEmail.TextAlignment = UITextAlignment.Left;
				lblEmail.Font = UIFont.FromName("Helvetica", 18);
				svDetails.AddSubview(lblEmail);


				txtEmail = new UITextField(new CGRect(0, lblEmail.Frame.Bottom + labelTextBoxPadding, txtPrimaryMobile.Frame.Width, txtPrimaryMobile.Frame.Height));

				txtEmail.Font = UIFont.FromName("Helvetica", 18);
				txtEmail.TextColor = UIColor.Black;
				txtEmail.Placeholder = "Email ID";
				txtEmail.TextAlignment = UITextAlignment.Left;
				txtEmail.Layer.BorderWidth = 1.0f;
				txtEmail.Layer.BorderColor = UIColor.Black.CGColor;
				txtEmail.Layer.CornerRadius = 5;
				txtEmail.ClipsToBounds = true;
				svDetails.AddSubview(txtEmail);


				txtEmail.ShouldReturn += (textField) =>
				{
					textField.ResignFirstResponder();
					return true;
				};

				UILabel lblAddress = new UILabel(new CGRect(0, txtEmail.Frame.Bottom + PaddingBetweenTwoFields, txtEmail.Frame.Width, txtEmail.Frame.Height));
				lblAddress.Text = "Address";
				lblAddress.TextColor = UIColor.Black;
				lblAddress.TextAlignment = UITextAlignment.Left;
				lblAddress.Font = UIFont.FromName("Helvetica", 18);
				svDetails.AddSubview(lblAddress);

				txtBaseAddress = new UITextView(new CGRect(0, lblAddress.Frame.Bottom + labelTextBoxPadding, txtEmail.Frame.Width, txtEmail.Frame.Height));

				txtBaseAddress.Font = UIFont.FromName("Helvetica", 18);
				txtBaseAddress.TextColor = UIColor.Black;
				txtBaseAddress.TextAlignment = UITextAlignment.Left;
				txtBaseAddress.Layer.BorderWidth = 1.0f;
				txtBaseAddress.Layer.BorderColor = UIColor.Black.CGColor;
				txtBaseAddress.Layer.CornerRadius = 5;
				txtBaseAddress.ClipsToBounds = true;
				svDetails.AddSubview(txtBaseAddress);

				UILabel lblDistrict = new UILabel(new CGRect(0, txtBaseAddress.Frame.Bottom + PaddingBetweenTwoFields, txtEmail.Frame.Width, txtEmail.Frame.Height));
				lblDistrict.Text = "District/City";
				lblDistrict.TextColor = UIColor.Black;
				lblDistrict.TextAlignment = UITextAlignment.Left;
				lblDistrict.Font = UIFont.FromName("Helvetica", 18);
				svDetails.AddSubview(lblDistrict);


				txtBaseAddressDistrict = new UITextField(new CGRect(0, lblDistrict.Frame.Bottom + labelTextBoxPadding, txtEmail.Frame.Width, txtEmail.Frame.Height));
				txtBaseAddressDistrict.Placeholder = "District / City";
				txtBaseAddressDistrict.Font = UIFont.FromName("Helvetica", 18);
				txtBaseAddressDistrict.TextColor = UIColor.Black;
				txtBaseAddressDistrict.TextAlignment = UITextAlignment.Left;
				txtBaseAddressDistrict.Layer.BorderWidth = 1.0f;
				txtBaseAddressDistrict.Layer.BorderColor = UIColor.Black.CGColor;
				txtBaseAddressDistrict.Layer.CornerRadius = 5;
				txtBaseAddressDistrict.ClipsToBounds = true;
				svDetails.AddSubview(txtBaseAddressDistrict);


				UILabel lblPincode = new UILabel(new CGRect(0, txtBaseAddressDistrict.Frame.Bottom + PaddingBetweenTwoFields, txtEmail.Frame.Width, txtEmail.Frame.Height));
				lblPincode.Text = "Pincode";
				lblPincode.TextColor = UIColor.Black;
				lblPincode.TextAlignment = UITextAlignment.Left;
				lblPincode.Font = UIFont.FromName("Helvetica", 18);

				svDetails.AddSubview(lblPincode);


				txtPinCode = new UITextField(new CGRect(0, lblPincode.Frame.Bottom + labelTextBoxPadding, txtEmail.Frame.Width, txtEmail.Frame.Height));
				txtPinCode.Placeholder = "Pincode";
				txtPinCode.Font = UIFont.FromName("Helvetica", 18);
				txtPinCode.TextColor = UIColor.Black;
				txtPinCode.TextAlignment = UITextAlignment.Left;
				txtPinCode.Layer.BorderWidth = 1.0f;
				txtPinCode.Layer.BorderColor = UIColor.Black.CGColor;
				txtPinCode.Layer.CornerRadius = 5;
				txtPinCode.ClipsToBounds = true;
				svDetails.AddSubview(txtPinCode);
				txtPinCode.ShouldChangeCharacters = (textField, range, replacement) =>
			{
				var newContent = new NSString(textField.Text).Replace(range, new NSString(replacement)).ToString();
				int number;
				return newContent.Length <= 6 && (replacement.Length == 0 || int.TryParse(replacement, out number));
			};

				UILabel lblDateOFBirth = new UILabel(new CGRect(0, txtPinCode.Frame.Bottom + PaddingBetweenTwoFields, txtEmail.Frame.Width, txtEmail.Frame.Height));
				lblDateOFBirth.Text = "Date Of Birth";
				lblDateOFBirth.TextColor = UIColor.Black;
				lblDateOFBirth.TextAlignment = UITextAlignment.Left;
				lblDateOFBirth.Font = UIFont.FromName("Helvetica", 18);
				svDetails.AddSubview(lblDateOFBirth);



				txtDOB = new UITextField(new CGRect(0, lblDateOFBirth.Frame.Bottom + labelTextBoxPadding, txtEmail.Frame.Width, txtEmail.Frame.Height));
				txtDOB.Placeholder = "DD/MM/YY";
				txtDOB.Font = UIFont.FromName("Helvetica", 18);
				txtDOB.TextColor = UIColor.Black;
				txtDOB.TextAlignment = UITextAlignment.Left;
				txtDOB.Layer.BorderWidth = 1.0f;
				txtDOB.Layer.BorderColor = UIColor.Black.CGColor;
				txtDOB.Layer.CornerRadius = 5;
				txtDOB.ClipsToBounds = true;
				svDetails.AddSubview(txtDOB);

				txtDOB.ShouldBeginEditing += OnTextFieldShouldBeginEditing;

				UILabel lblGender = new UILabel(new CGRect(0, txtDOB.Frame.Bottom + PaddingBetweenTwoFields, txtEmail.Frame.Width, txtEmail.Frame.Height));
				lblGender.Text = "Gender";
				lblGender.TextColor = UIColor.Black;
				lblGender.TextAlignment = UITextAlignment.Left;
				lblGender.Font = UIFont.FromName("Helvetica", 18);
				svDetails.AddSubview(lblGender);

				segmentControl.Frame = new CGRect(0, lblGender.Frame.Bottom + labelTextBoxPadding, 100, 25);


				segmentControl.InsertSegment("Male", 0, false);
				segmentControl.InsertSegment("Female", 1, false);
				segmentControl.InsertSegment("LGBT", 2, false);

				segmentControl.ValueChanged += (sender, args) =>
				{
					if (segmentControl.SelectedSegment == 0)
					{
						gender = "Male";
					}
					else if (segmentControl.SelectedSegment == 1)
					{
						gender = "Female";
					}
					else
					{
						gender = "LGBT";

					}
				};

				segmentControl.SelectedSegment = 0;
				segmentControl.SizeToFit();

				svDetails.Add(segmentControl);


				UILabel lblNewPassword = new UILabel(new CGRect(0, segmentControl.Frame.Bottom + PaddingBetweenTwoFields, txtEmail.Frame.Width, txtEmail.Frame.Height));
				lblNewPassword.Text = "New Password";
				lblNewPassword.TextColor = UIColor.Black;
				lblNewPassword.TextAlignment = UITextAlignment.Left;
				lblNewPassword.Font = UIFont.FromName("Helvetica", 18);
				svDetails.AddSubview(lblNewPassword);


				txtNewPassword = new UITextField(new CGRect(0, lblNewPassword.Frame.Bottom + labelTextBoxPadding, txtEmail.Frame.Width, txtEmail.Frame.Height));
				txtNewPassword.Placeholder = "*********";
				txtNewPassword.Font = UIFont.FromName("Helvetica", 18);
				txtNewPassword.TextColor = UIColor.Black;
				txtNewPassword.TextAlignment = UITextAlignment.Left;
				txtNewPassword.Layer.BorderWidth = 1.0f;
				txtNewPassword.Layer.BorderColor = UIColor.Black.CGColor;
				txtNewPassword.Layer.CornerRadius = 5;
				txtNewPassword.ClipsToBounds = true;
				svDetails.AddSubview(txtNewPassword);
				txtNewPassword.SecureTextEntry = true;

				txtNewPassword.ShouldBeginEditing += delegate
				{
					isSupplement = true;
					return true;

				};

				UILabel lblConfirmPassword = new UILabel(new CGRect(0, txtNewPassword.Frame.Bottom + PaddingBetweenTwoFields, txtEmail.Frame.Width, txtEmail.Frame.Height));
				lblConfirmPassword.Text = "Confirm Password";
				lblConfirmPassword.TextColor = UIColor.Black;
				lblConfirmPassword.TextAlignment = UITextAlignment.Left;
				lblConfirmPassword.Font = UIFont.FromName("Helvetica", 18);
				svDetails.AddSubview(lblConfirmPassword);



				txtConfirmPassword = new UITextField(new CGRect(0, lblConfirmPassword.Frame.Bottom + labelTextBoxPadding, txtEmail.Frame.Width, txtEmail.Frame.Height));
				txtConfirmPassword.Placeholder = "********";
				txtConfirmPassword.Font = UIFont.FromName("Helvetica", 18);
				txtConfirmPassword.TextColor = UIColor.Black;
				txtConfirmPassword.TextAlignment = UITextAlignment.Left;
				txtConfirmPassword.Layer.BorderWidth = 1.0f;
				txtConfirmPassword.Layer.BorderColor = UIColor.Black.CGColor;
				txtConfirmPassword.Layer.CornerRadius = 5;
				txtConfirmPassword.ClipsToBounds = true;
				svDetails.AddSubview(txtConfirmPassword);
				txtConfirmPassword.SecureTextEntry = true;



				svDetails.ContentSize = new CGSize(svDetails.Frame.Width, txtConfirmPassword.Frame.Bottom + 60);

			}
			catch (Exception ex)
			{


			}
		}


		private void KeyBoardUpNotification(NSNotification notification)
		{
			if (isSupplement == true)
			{
				bottom = (heightScroll + offset);

				var test = UIScreen.MainScreen.Bounds;
				// Calculate how far we need to scroll
				//scroll_amount = (r.Width - (View.Frame.Size.Height - bottom));
				if (test.Height == 568 && test.Width == 320)
				{
					scroll_amount = 250; //IPHONE 5/5S
				}
				else if (test.Height == 480 && test.Width == 320)
				{
					scroll_amount = 220; //IPHONE 4/4S
				}
				else if (test.Height == 667 && test.Width == 375)
				{
					scroll_amount = 220; //IPHONE 6
				}
				else if (test.Height == 736 && test.Width == 414)
				{
					scroll_amount = 270; //IPHONE 6 Plus
				}
				// Perform the scrolling
				if (scroll_amount > 0)
				{
					moveViewUp = true;
					ScrollTheView(moveViewUp);
				}
				else {
					moveViewUp = false;
				}
			}
		}

		private void KeyBoardDownNotification(NSNotification notification)
		{
			if (moveViewUp) { ScrollTheView(false); }
		}

		private void ScrollTheView(bool move)
		{

			// scroll the view up or down
			UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
			UIView.SetAnimationDuration(0.3);

			CGRect frame = View.Frame;

			if (move)
			{
				frame.Y -= scroll_amount;
				isSupplement = false;
			}
			else {
				frame.Y += scroll_amount;
				scroll_amount = 0;
			}

			View.Frame = frame;
			UIView.CommitAnimations();
		}

		bool OnTextFieldShouldBeginEditing(UITextField textField)
		{
			var modalPicker = new ModalPickerViewController(ModalPickerType.Date, "Select A Date", this)
			{
				HeaderBackgroundColor = UIColor.Red,
				HeaderTextColor = UIColor.White,
				TransitioningDelegate = new ModalPickerTransitionDelegate(),
				ModalPresentationStyle = UIModalPresentationStyle.Custom
			};

			modalPicker.DatePicker.Mode = UIDatePickerMode.Date;

			modalPicker.OnModalPickerDismissed += (s, ea) =>
			{
				var dateFormatter = new NSDateFormatter()
				{
					DateFormat = "dd-MMM-yyyy"
				};

				textField.Text = dateFormatter.ToString(modalPicker.DatePicker.Date);
			};

			PresentViewController(modalPicker, true, null);

			return false;
		}
	


			
				///// <summary>
				///// Check for valid email.
				///// </summary>
				///// <returns><c>true</c>, if valid email was ised, <c>false</c> otherwise.</returns>
				///// <param name="email">Email.</param>
				private bool isValidEmail(string email)
				{
					if (email != string.Empty)
					{
						string sPattern = @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[,]{0,1}\s*)+$";
						bool checkEmail = System.Text.RegularExpressions.Regex.IsMatch(email, sPattern);

						if (checkEmail == false)
						{
							return false;
						}
						else
						{
							return true;
						}
					}
					else
					{
						return true;
					}
				}


	}
}

