using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Volndear.iOS
{
	public partial class AddNewEventViewController : UIViewController
	{
		UIView vwNewEventContainer = new UIView();
		UIScrollView svAddNewEventDetails = new UIScrollView();
		private nfloat scroll_amount = 0.0f;    // amount to scroll 
		private float bottom = 0.0f;           // bottom point
		private float offset = 0.0f;          // extra offset
		private bool moveViewUp = false;
		int heightScroll = 3;
		bool isSupplement = false;

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


		public AddNewEventViewController() : base("AddNewEventViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyBoardUpNotification);
			//Keyboard down notification
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyBoardDownNotification);
			DismissKeyboardOnBackgroundTap();
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(7, 198, 192);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
			AutomaticallyAdjustsScrollViewInsets = false;
			svAddNewEventDetails.ContentInset = UIEdgeInsets.Zero;
			configureAddNewEventView();
			// Perform any additional setup after loading the view, typically from a nib.
		}
		private void configureAddNewEventView()
		{
			UITextField txtNewEventName = new UITextField();
			UITextField txtDOB = new UITextField();
			UITextField txtPrimaryMobile = new UITextField();
			UITextView txtEventDescription = new UITextView();
			UITextView txtBaseAddress = new UITextView();
			UITextField txtBaseAddressDistrict = new UITextField();

			Events newEvent = new Events();
			UITextField txtPinCode = new UITextField();

			this.NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, args) =>
					{

						if (txtNewEventName.Text != string.Empty && txtDOB.Text != string.Empty && txtBaseAddress.Text != string.Empty && txtBaseAddressDistrict.Text != string.Empty
							&& txtEventDescription.Text != string.Empty && txtPinCode.Text != string.Empty && txtPrimaryMobile.Text != string.Empty)
						{
							newEvent.EventName = txtNewEventName.Text.Trim();
							newEvent.EventOrganizerContactNumber = txtPrimaryMobile.Text.Trim();
							newEvent.EventDateTime = newEventDateTime;
							newEvent.EventAddress = txtBaseAddress.Text.Trim() + " " + txtBaseAddressDistrict.Text.Trim() + " " + txtPinCode.Text.Trim();
							newEvent.EventDescription = txtEventDescription.Text.Trim();
							newEvent.EventOrganizerName = AppGlobal.LoggedInUser.CitizenName;
							newEvent.EventPostedTime = System.DateTime.Now;
							AppDelegate.eventDatabase.InsertNewEvent(newEvent);

							txtNewEventName.Text = string.Empty;
							txtDOB.Text = string.Empty;
							txtBaseAddress.Text = string.Empty;
							txtBaseAddressDistrict.Text = string.Empty;
							txtEventDescription.Text = string.Empty;
							txtPinCode.Text = string.Empty;
							txtPrimaryMobile.Text = string.Empty;
					NavigationController.PushViewController(new EventsViewController(), true);

						}
						else
						{
							using (var alert = new UIAlertView("Warning", "All the fields are mandatory fields, please fill all the fields", null, "OK", null))
								alert.Show();
						}
					})
				, true);

		

			Title = "Add New Event";
			try
			{
				nfloat labelTextBoxPadding = 0;
				nfloat PaddingBetweenTwoFields = 0;
				//To set max length 10
				const int maxCharacters = 10;
				var screen = UIScreen.MainScreen.Bounds;


				vwNewEventContainer.Frame = new CGRect(0, 70, screen.Width, screen.Height - 70);
				vwNewEventContainer.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("volndear_hands"));
				View.AddSubview(vwNewEventContainer);



				svAddNewEventDetails.Frame = new CGRect(5, 0, vwNewEventContainer.Frame.Width, vwNewEventContainer.Frame.Height);
				vwNewEventContainer.AddSubview(svAddNewEventDetails);

				UILabel lblEventName = new UILabel(new CGRect(0, 3, vwNewEventContainer.Frame.Width, 30));
				lblEventName.Text = "Event Name";
				lblEventName.TextColor = UIColor.Black;
				lblEventName.TextAlignment = UITextAlignment.Left;
				lblEventName.Font = UIFont.FromName("Helvetica", 18);
				svAddNewEventDetails.AddSubview(lblEventName);


				txtNewEventName = new UITextField(new CGRect(0, lblEventName.Frame.Bottom + labelTextBoxPadding, vwNewEventContainer.Frame.Width - 10, 40));
				txtNewEventName.Font = UIFont.FromName("Helvetica", 18);
				txtNewEventName.TextColor = UIColor.Black;
				txtNewEventName.TextAlignment = UITextAlignment.Left;
				txtNewEventName.Layer.BorderWidth = 1.0f;
				txtNewEventName.Layer.BorderColor = UIColor.Black.CGColor;
				txtNewEventName.Layer.CornerRadius = 5;
				txtNewEventName.ClipsToBounds = true;
				txtNewEventName.Placeholder = " Maximum of 30 Characters";


				txtNewEventName.ShouldReturn += (textField) =>
				{
					textField.ResignFirstResponder();
					return true;
				};
				svAddNewEventDetails.AddSubview(txtNewEventName);

				UILabel lblDateOFBirth = new UILabel(new CGRect(0, txtNewEventName.Frame.Bottom + PaddingBetweenTwoFields, txtNewEventName.Frame.Width, txtNewEventName.Frame.Height));
				lblDateOFBirth.Text = "Date Of Event";
				lblDateOFBirth.TextColor = UIColor.Black;
				lblDateOFBirth.TextAlignment = UITextAlignment.Left;
				lblDateOFBirth.Font = UIFont.FromName("Helvetica", 18);
				svAddNewEventDetails.AddSubview(lblDateOFBirth);



				txtDOB = new UITextField(new CGRect(0, lblDateOFBirth.Frame.Bottom + labelTextBoxPadding, txtNewEventName.Frame.Width, txtNewEventName.Frame.Height));
				txtDOB.Placeholder = "DD/MM/YY";
				txtDOB.Font = UIFont.FromName("Helvetica", 18);
				txtDOB.TextColor = UIColor.Black;
				txtDOB.TextAlignment = UITextAlignment.Left;
				txtDOB.Layer.BorderWidth = 1.0f;
				txtDOB.Layer.BorderColor = UIColor.Black.CGColor;
				txtDOB.Layer.CornerRadius = 5;
				txtDOB.ClipsToBounds = true;
				svAddNewEventDetails.AddSubview(txtDOB);

				txtDOB.ShouldBeginEditing += OnTextFieldShouldBeginEditing;

				txtDOB.ShouldReturn = delegate (UITextField textField)
				{
					txtDOB.ResignFirstResponder();

					return true;
				};

				UILabel lblMobileNumber = new UILabel(new CGRect(0, txtDOB.Frame.Bottom + PaddingBetweenTwoFields, txtDOB.Frame.Width, txtDOB.Frame.Height));
				lblMobileNumber.Text = "Mobile Number to contact";
				lblMobileNumber.TextColor = UIColor.Black;
				lblMobileNumber.TextAlignment = UITextAlignment.Left;
				lblMobileNumber.Font = UIFont.FromName("Helvetica", 18);
				svAddNewEventDetails.AddSubview(lblMobileNumber);

				txtPrimaryMobile = new UITextField(new CGRect(0, lblMobileNumber.Frame.Bottom + labelTextBoxPadding, vwNewEventContainer.Frame.Width - 10, txtDOB.Frame.Height));

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
				svAddNewEventDetails.AddSubview(txtPrimaryMobile);

				UILabel lblDescription = new UILabel(new CGRect(0, txtPrimaryMobile.Frame.Bottom + PaddingBetweenTwoFields, txtNewEventName.Frame.Width, txtNewEventName.Frame.Height));
				lblDescription.Text = "Description (Max of 250 Characters)";
				lblDescription.TextColor = UIColor.Black;
				lblDescription.TextAlignment = UITextAlignment.Left;
				lblDescription.Font = UIFont.FromName("Helvetica", 18);
				svAddNewEventDetails.AddSubview(lblDescription);

				txtEventDescription = new UITextView(new CGRect(0, lblDescription.Frame.Bottom + labelTextBoxPadding, txtNewEventName.Frame.Width, 70));

				txtEventDescription.Font = UIFont.FromName("Helvetica", 18);
				txtEventDescription.TextColor = UIColor.Black;
				txtEventDescription.TextAlignment = UITextAlignment.Left;
				txtEventDescription.Layer.BorderWidth = 1.0f;
				txtEventDescription.Layer.BorderColor = UIColor.Black.CGColor;
				txtEventDescription.Layer.CornerRadius = 5;
				txtEventDescription.ClipsToBounds = true;
				svAddNewEventDetails.AddSubview(txtEventDescription);





				UILabel lblAddress = new UILabel(new CGRect(0, txtEventDescription.Frame.Bottom + PaddingBetweenTwoFields, txtNewEventName.Frame.Width, txtNewEventName.Frame.Height));
				lblAddress.Text = "Address";
				lblAddress.TextColor = UIColor.Black;
				lblAddress.TextAlignment = UITextAlignment.Left;
				lblAddress.Font = UIFont.FromName("Helvetica", 18);
				svAddNewEventDetails.AddSubview(lblAddress);

				txtBaseAddress = new UITextView(new CGRect(0, lblAddress.Frame.Bottom + labelTextBoxPadding, txtNewEventName.Frame.Width, 70));

				txtBaseAddress.Font = UIFont.FromName("Helvetica", 18);
				txtBaseAddress.TextColor = UIColor.Black;
				txtBaseAddress.TextAlignment = UITextAlignment.Left;
				txtBaseAddress.Layer.BorderWidth = 1.0f;
				txtBaseAddress.Layer.BorderColor = UIColor.Black.CGColor;
				txtBaseAddress.Layer.CornerRadius = 5;
				txtBaseAddress.ClipsToBounds = true;
				svAddNewEventDetails.AddSubview(txtBaseAddress);

				txtBaseAddress.ShouldBeginEditing += delegate
				{
					if (isKeyboardup == false)
					{
						isSupplement = true;

					}
					else
					{
						isSupplement = false;

					}
					return true;



				};

				UILabel lblDistrict = new UILabel(new CGRect(0, txtBaseAddress.Frame.Bottom + PaddingBetweenTwoFields, txtNewEventName.Frame.Width, txtNewEventName.Frame.Height));
				lblDistrict.Text = "District/City";
				lblDistrict.TextColor = UIColor.Black;
				lblDistrict.TextAlignment = UITextAlignment.Left;
				lblDistrict.Font = UIFont.FromName("Helvetica", 18);
				svAddNewEventDetails.AddSubview(lblDistrict);


				txtBaseAddressDistrict = new UITextField(new CGRect(0, lblDistrict.Frame.Bottom + labelTextBoxPadding, txtNewEventName.Frame.Width, txtNewEventName.Frame.Height));
				txtBaseAddressDistrict.Placeholder = "District / City";
				txtBaseAddressDistrict.Font = UIFont.FromName("Helvetica", 18);
				txtBaseAddressDistrict.TextColor = UIColor.Black;
				txtBaseAddressDistrict.TextAlignment = UITextAlignment.Left;
				txtBaseAddressDistrict.Layer.BorderWidth = 1.0f;
				txtBaseAddressDistrict.Layer.BorderColor = UIColor.Black.CGColor;
				txtBaseAddressDistrict.Layer.CornerRadius = 5;
				txtBaseAddressDistrict.ClipsToBounds = true;
				svAddNewEventDetails.AddSubview(txtBaseAddressDistrict);


				txtBaseAddressDistrict.ShouldBeginEditing += delegate
				{
					if (isKeyboardup == false)
					{
						isSupplement = true;

					}
					else
					{
						isSupplement = false;

					}
					return true;



				};
				txtBaseAddressDistrict.ShouldReturn += (textField) =>
				{
					textField.ResignFirstResponder();
					return true;
				};




				UILabel lblPincode = new UILabel(new CGRect(0, txtBaseAddressDistrict.Frame.Bottom + PaddingBetweenTwoFields, txtNewEventName.Frame.Width, txtNewEventName.Frame.Height));
				lblPincode.Text = "Pincode";
				lblPincode.TextColor = UIColor.Black;
				lblPincode.TextAlignment = UITextAlignment.Left;
				lblPincode.Font = UIFont.FromName("Helvetica", 18);

				svAddNewEventDetails.AddSubview(lblPincode);


				txtPinCode = new UITextField(new CGRect(0, lblPincode.Frame.Bottom + labelTextBoxPadding, txtNewEventName.Frame.Width, txtNewEventName.Frame.Height));
				txtPinCode.Placeholder = "Pincode";
				txtPinCode.Font = UIFont.FromName("HelvetAutomaticallyAdjustsScrollViewInsets = false;\n\t\t\tsvEvents.ContentInset = UIEdgeInsets.Zero;ica", 18);
				txtPinCode.TextColor = UIColor.Black;
				txtPinCode.TextAlignment = UITextAlignment.Left;
				txtPinCode.Layer.BorderWidth = 1.0f;
				txtPinCode.Layer.BorderColor = UIColor.Black.CGColor;
				txtPinCode.Layer.CornerRadius = 5;
				txtPinCode.ClipsToBounds = true;
				svAddNewEventDetails.AddSubview(txtPinCode);
				txtPinCode.ShouldBeginEditing += delegate
				{
					if (isKeyboardup == false)
					{
						isSupplement = true;

					}
					else
					{
						isSupplement = false;

					}
					return true;



				};
				txtPinCode.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();
				return true;
			};

				txtPinCode.ShouldChangeCharacters = (textField, range, replacement) =>
			{
				var newContent = new NSString(textField.Text).Replace(range, new NSString(replacement)).ToString();
				int number;
				return newContent.Length <= 6 && (replacement.Length == 0 || int.TryParse(replacement, out number));
			};
				svAddNewEventDetails.ContentSize = new CGSize(svAddNewEventDetails.Frame.Width, txtPinCode.Frame.Bottom + 60);

			}
			catch (Exception ex)
			{


			}
		}
		DateTime newEventDateTime;
		bool OnTextFieldShouldBeginEditing(UITextField textField)
		{
			var modalPicker = new ModalPickerViewController(ModalPickerType.Date, "Select A Date", this)
			{
				HeaderBackgroundColor = UIColor.Red,
				HeaderTextColor = UIColor.White,
				TransitioningDelegate = new ModalPickerTransitionDelegate(),
				ModalPresentationStyle = UIModalPresentationStyle.Custom
			};

			modalPicker.DatePicker.Mode = UIDatePickerMode.DateAndTime;

			modalPicker.OnModalPickerDismissed += (s, ea) =>
			{
				var dateFormatter = new NSDateFormatter()
				{
					DateFormat = "dd-MMM-yyyy HH:mm",

				};
				try
				{


					DateTime myDate = DateTime.ParseExact(dateFormatter.ToString(modalPicker.DatePicker.Date), "dd-MMM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
					if (myDate >= System.DateTime.Now)
					{
						textField.Text = dateFormatter.ToString(modalPicker.DatePicker.Date);
						newEventDateTime = myDate;
					}
					else
					{
						using (var alert = new UIAlertView("Warning", "The date you selected is beyond the present date , please try again", null, "OK", null))
							alert.Show();
					}
				}
				catch (Exception ex)
				{

				}

			};

			PresentViewController(modalPicker, true, null);

			return false;
		}
		bool isKeyboardup = false;
		private void KeyBoardUpNotification(NSNotification notification)
		{
			isKeyboardup = true;
			if (isSupplement == true)
			{
				bottom = (heightScroll + offset);

				var test = UIScreen.MainScreen.Bounds;
				// Calculate how far we need to scroll
				//scroll_amount = (r.Width - (View.Frame.Size.Height - bottom));
				if (test.Height == 568 && test.Width == 320)
				{
					scroll_amount = 200; //IPHONE 5/5S
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
			isKeyboardup = false;
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


		protected void DismissKeyboardOnBackgroundTap()
		{
			var tap = new UITapGestureRecognizer { CancelsTouchesInView = false };
			tap.AddTarget(() => View.EndEditing(true));
			svAddNewEventDetails.AddGestureRecognizer(tap);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

