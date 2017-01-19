using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Volndear.iOS
{
	public partial class LoginViewController : UIViewController
	{
		UIView vwContainer = new UIView();
		Citizen citizen = new Citizen();
		public LoginViewController() : base("LoginViewController", null)
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
			configureLoginView();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void configureLoginView()
		{
			
			citizen = AppDelegate.citizenRegistrationDatabase.GetCitizen();

			//Title = "Login";
			try
			{
				foreach (var subView in vwContainer.Subviews)
				{
					subView.RemoveFromSuperview();
				}

				UITextField txtPassword;
				UITextField txtLogin;
				var screen = UIScreen.MainScreen.Bounds;
				vwContainer = new UIView();
				vwContainer.Frame = new CGRect(0, 70, screen.Width, screen.Height - 70);
				vwContainer.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("volndear_hands"));
				View.AddSubview(vwContainer);

				txtLogin = new UITextField(new CGRect(40, 10, 250, 50));
				txtLogin.AttributedPlaceholder = new NSAttributedString("Login ID", null, UIColor.White);
				txtLogin.BackgroundColor = UIColor.Gray;
				txtLogin.TextColor = UIColor.White;
				txtLogin.TextAlignment = UITextAlignment.Left;
				txtLogin.Font = UIFont.FromName("Helvetica", 18);
				txtLogin.Layer.BorderColor = UIColor.Gray.CGColor;
				txtLogin.ReturnKeyType = UIReturnKeyType.Next;
				txtLogin.AutocorrectionType = UITextAutocorrectionType.No;
				txtLogin.Layer.CornerRadius = 5;
				txtLogin.ClipsToBounds = true;
				vwContainer.AddSubview(txtLogin);


				txtLogin.BecomeFirstResponder();

				txtPassword = new UITextField(new CGRect(txtLogin.Frame.X, txtLogin.Frame.Bottom + 10, txtLogin.Frame.Width, txtLogin.Frame.Height));
				txtPassword.AttributedPlaceholder = new NSAttributedString("Password", null, UIColor.White);
				txtPassword.TextColor = UIColor.White;
				txtPassword.BackgroundColor = UIColor.Gray;
				txtPassword.TextAlignment = UITextAlignment.Left;
				txtPassword.Font = UIFont.FromName("Helvetica", 18);
				txtPassword.SecureTextEntry = true;
				txtPassword.Layer.BorderColor = UIColor.Gray.CGColor;
				txtPassword.ReturnKeyType = UIReturnKeyType.Done;
				txtPassword.Layer.CornerRadius = 5;
				txtPassword.ClipsToBounds = true;
				vwContainer.AddSubview(txtPassword);

				txtLogin.ShouldReturn = delegate (UITextField textField)
				{
					txtPassword.BecomeFirstResponder();
					return true;
				};

				// make 'return' on last text field save and close the form
				txtPassword.ShouldReturn = delegate (UITextField textField)
				{
					txtPassword.ResignFirstResponder();

					return true;
				};


				UIButton btnLogin = new UIButton(new CGRect(txtLogin.Frame.X, txtPassword.Frame.Bottom + 20, 70, 30));
				btnLogin.BackgroundColor = UIColor.Gray;
				btnLogin.SetTitle("Login", UIControlState.Normal);
				btnLogin.Layer.BorderWidth = 1.0f;
				btnLogin.Layer.BorderColor = UIColor.Gray.CGColor;
				btnLogin.Layer.CornerRadius = 5;
				btnLogin.ClipsToBounds = true;
				btnLogin.Font = UIFont.FromName("Helvetica", 18);


				//	btnLogin.SetBackgroundImage(UIImage.FromFile("Images/logout.png"), UIControlState.Normal);
				btnLogin.TouchUpInside += (sender, e) =>
				{
					if (txtLogin.Text.Trim() != string.Empty && txtPassword.Text.Trim() != string.Empty)
					{

						citizen = AppDelegate.citizenRegistrationDatabase.GetCitizen();
						if (citizen.CitizenName == txtLogin.Text && citizen.Password == txtPassword.Text)
						{
							AppGlobal.LoggedInUser = citizen;
							NavigationController.PushViewController(new EventsViewController(), true);
						}
						else
						{

							using (var alert = new UIAlertView("Warning", "your login credentials are incorrect, please try again.", null, "OK", null))
								alert.Show();
						}
					}
					else
					{
						using (var alert = new UIAlertView("Warning", "Please enter the valid login credentials.", null, "OK", null))
							alert.Show();

					}


				};

				vwContainer.AddSubview(btnLogin);

				UIButton btnRegister = new UIButton();
				btnRegister.Frame = new CGRect(vwContainer.Frame.Width - 120, txtPassword.Frame.Bottom + 20, 90, 30);
				btnRegister.BackgroundColor = UIColor.Gray;
				btnRegister.SetTitle("Register", UIControlState.Normal);
				//	btnLogin.SetBackgroundImage(UIImage.FromFile("Images/logout.png"), UIControlState.Normal);

				btnRegister.Layer.BorderWidth = 1.0f;
				btnRegister.Layer.BorderColor = UIColor.Gray.CGColor;
				btnRegister.Layer.CornerRadius = 5;
				btnRegister.ClipsToBounds = true;
				btnRegister.Font = UIFont.FromName("Helvetica", 18);
				vwContainer.AddSubview(btnRegister);

				btnRegister.TouchUpInside += (sender, e) =>
				{

					NavigationController.PushViewController(new RegistrationCategoryViewController(), true);
				};

				if (citizen == null)
				{

					btnLogin.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

