using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Volndear.iOS
{
	public partial class HomeScreenViewController : UIViewController
	{
		UIView vwContainer = new UIView();
		Citizen citizen = new Citizen();
		System.Timers.Timer timer = new System.Timers.Timer();
		public HomeScreenViewController() : base("HomeScreenViewController", null)
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
			// Perform any additional setup after loading the view, typically from a nib.
			if (AppGlobal.isFromRegistrationScreen)
			{
				configureLoginView();
			}
			else
			{ 
				configureInitialView();
			}

			timer.Interval = 2000;
			timer.Elapsed += OnTimedEvent;
			timer.Enabled = true;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
		private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
		{
			InvokeOnMainThread(() =>
			{
				configureLoginView();
				
			});


		}


		private void configureInitialView()
		{
			try
			{
				//Title = "Volndear";
				var screen = UIScreen.MainScreen.Bounds;
				vwContainer.Frame = new CGRect(0, 70, screen.Width, screen.Height - 70);
				vwContainer.BackgroundColor = UIColor.White;//UIColor.FromPatternImage(UIImage.FromFile("volndear_hands"));
				View.AddSubview(vwContainer);

				UIImageView imgVwVolndear = new UIImageView(new CGRect(5, 150, screen.Width - 10, 100));
				imgVwVolndear.Image = UIImage.FromFile("Images/volundear.png");
				vwContainer.AddSubview(imgVwVolndear);


				//UILabel lblVolndear = new UILabel(new CGRect(5, screen.Height / 2- 100, screen.Width - 10, 40));
				//lblVolndear.Text = "VOLNDEAR";
				//lblVolndear.TextColor = UIColor.Black;
				//lblVolndear.TextAlignment = UITextAlignment.Center;
				//lblVolndear.Font = UIFont.FromName("Helvetica", 55);
				//vwContainer.AddSubview(lblVolndear);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}



		private void configureLoginView()
		{
			timer.Enabled = false;
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
				vwContainer.Frame = new CGRect(0, 70, screen.Width, screen.Height-70);
				//vwContainer.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("volndear_hands"));
				View.AddSubview(vwContainer);

				UIImageView imgVwVolndear = new UIImageView(new CGRect(5, 10, screen.Width - 10, 100));
				imgVwVolndear.Image = UIImage.FromFile("Images/volundear.png");
				vwContainer.AddSubview(imgVwVolndear);


				var imgVwLogintxt = new UIImageView(UIImage.FromBundle("Images/login.png"))
				{
					// Indent it 10 pixels from the left.
					Frame = new CGRect(10, 0, 20, 20)
				};


				UIView txtLoginLeftView = new UIView(new CGRect(0, 0, 40, 20));
				txtLoginLeftView.AddSubview(imgVwLogintxt);
			
				txtLogin = new UITextField(new CGRect(40, 110, 250, 50));
				txtLogin.AttributedPlaceholder = new NSAttributedString("Login ID", null, UIColor.White);
				txtLogin.BackgroundColor = UIColor.Gray;
				txtLogin.TextColor = UIColor.White;
				//txtLogin.TextAlignment = UITextAlignment.Left;
				txtLogin.Font = UIFont.FromName("Helvetica", 18);
				txtLogin.Layer.BorderColor = UIColor.Gray.CGColor;
				txtLogin.ReturnKeyType = UIReturnKeyType.Next;
				txtLogin.AutocorrectionType = UITextAutocorrectionType.No;
				//txtLogin.Layer.CornerRadius = 5;
				//txtLogin.ClipsToBounds = true;
				vwContainer.AddSubview(txtLogin);
				txtLogin.ClearButtonMode = UITextFieldViewMode.WhileEditing;
				txtLogin.LeftViewMode = UITextFieldViewMode.Always;
				txtLogin.LeftView = txtLoginLeftView;

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
				//txtPassword.Layer.CornerRadius = 5;
				//txtPassword.ClipsToBounds = true;
				vwContainer.AddSubview(txtPassword);
				txtPassword.ClearButtonMode = UITextFieldViewMode.WhileEditing;
				var imgVwPasswordtxt = new UIImageView(UIImage.FromBundle("Images/password.png"))
				{
					// Indent it 10 pixels from the left.
					Frame = new CGRect(10, 0, 20, 20)
				};


				UIView txtPasswordLeftView = new UIView(new CGRect(0, 0, 40, 20));
				txtPasswordLeftView.AddSubview(imgVwPasswordtxt);

				txtPassword.ShouldReturn = delegate (UITextField textField)
				{
					txtPassword.BecomeFirstResponder();
					return true;
				};
				txtPassword.LeftViewMode = UITextFieldViewMode.Always;
				txtPassword.LeftView = txtPasswordLeftView;

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






	}
}

