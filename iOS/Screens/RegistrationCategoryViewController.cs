using System;
using CoreGraphics;
using UIKit;

namespace Volndear.iOS
{
	public partial class RegistrationCategoryViewController : UIViewController
	{
		public RegistrationCategoryViewController() : base("RegistrationCategoryViewController", null)
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
			ConfigureRegistrationCategoryView();
			//NavigationController.PopViewController(true);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		private void ConfigureRegistrationCategoryView()
		{
			try
			{
				

				var screen = UIScreen.MainScreen.Bounds;
				UIView vwContainer = new UIView();
				vwContainer.Frame = new CGRect(0, 70, screen.Width, screen.Height - 70);
				vwContainer.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("volndear_hands"));
				View.AddSubview(vwContainer);



				UIButton btnCitizenRegistration = new UIButton(new CGRect(20, 30, 100, 100));
				btnCitizenRegistration.BackgroundColor = UIColor.Gray;
				btnCitizenRegistration.SetTitle("Citizen", UIControlState.Normal);
				//	btnLogin.SetBackgroundImage(UIImage.FromFile("Images/logout.png"), UIControlState.Normal);
				//btnLogin.TouchUpInside += (sender, e) =>
				//{

				//	NavigationController.PushViewController(new LoginController(), true);
				//};
				btnCitizenRegistration.Layer.BorderWidth = 1.0f;
				btnCitizenRegistration.Layer.BorderColor = UIColor.Gray.CGColor;
				btnCitizenRegistration.Layer.CornerRadius = 5;
				btnCitizenRegistration.ClipsToBounds = true;
				btnCitizenRegistration.Font = UIFont.FromName("Helvetica", 18);
				vwContainer.AddSubview(btnCitizenRegistration);



				UIButton btnPrivateOrgRegistration = new UIButton(new CGRect(btnCitizenRegistration.Frame.Width + 100, 30, 100, 100));
				btnPrivateOrgRegistration.BackgroundColor = UIColor.Gray;
				btnPrivateOrgRegistration.SetTitle("Private\nOrganization", UIControlState.Normal);
				//	btnLogin.SetBackgroundImage(UIImage.FromFile("Images/logout.png"), UIControlState.Normal);
				//btnLogin.TouchUpInside += (sender, e) =>
				//{

				//	NavigationController.PushViewController(new LoginController(), true);
				//};
				btnPrivateOrgRegistration.Layer.BorderWidth = 1.0f;
				btnPrivateOrgRegistration.Layer.BorderColor = UIColor.Gray.CGColor;
				btnPrivateOrgRegistration.Layer.CornerRadius = 5;
				btnPrivateOrgRegistration.ClipsToBounds = true;
				btnPrivateOrgRegistration.Font = UIFont.FromName("Helvetica", 18);
				vwContainer.AddSubview(btnPrivateOrgRegistration);


				UIButton btnGovernmentOrgRegistration = new UIButton(new CGRect(20, btnCitizenRegistration.Frame.Bottom + 30, 100, 100));
				btnGovernmentOrgRegistration.BackgroundColor = UIColor.Gray;
				btnGovernmentOrgRegistration.SetTitle("Government", UIControlState.Normal);
				//	btnLogin.SetBackgroundImage(UIImage.FromFile("Images/logout.png"), UIControlState.Normal);
				//btnLogin.TouchUpInside += (sender, e) =>
				//{

				//	NavigationController.PushViewController(new LoginController(), true);
				//};
				btnGovernmentOrgRegistration.Layer.BorderWidth = 1.0f;
				btnGovernmentOrgRegistration.Layer.BorderColor = UIColor.Gray.CGColor;
				btnGovernmentOrgRegistration.Layer.CornerRadius = 5;
				btnGovernmentOrgRegistration.ClipsToBounds = true;
				btnGovernmentOrgRegistration.Font = UIFont.FromName("Helvetica", 18);
				vwContainer.AddSubview(btnGovernmentOrgRegistration);

				UIButton btnNonGovtOrgRegistration = new UIButton(new CGRect(btnGovernmentOrgRegistration.Frame.Width + 100, btnGovernmentOrgRegistration.Frame.Y, 100, 100));
				btnNonGovtOrgRegistration.BackgroundColor = UIColor.Gray;
				btnNonGovtOrgRegistration.SetTitle("NGO", UIControlState.Normal);
				//	btnLogin.SetBackgroundImage(UIImage.FromFile("Images/logout.png"), UIControlState.Normal);
				//btnLogin.TouchUpInside += (sender, e) =>
				//{

				//	NavigationController.PushViewController(new LoginController(), true);
				//};
				btnNonGovtOrgRegistration.Layer.BorderWidth = 1.0f;
				btnNonGovtOrgRegistration.Layer.BorderColor = UIColor.Gray.CGColor;
				btnNonGovtOrgRegistration.Layer.CornerRadius = 5;
				btnNonGovtOrgRegistration.ClipsToBounds = true;
				btnNonGovtOrgRegistration.Font = UIFont.FromName("Helvetica", 18);
				vwContainer.AddSubview(btnNonGovtOrgRegistration);

				btnCitizenRegistration.TouchUpInside += (sender, e) =>
				{

					NavigationController.PushViewController(new UserRegistrationViewController(), true);
				};

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

	}
}

