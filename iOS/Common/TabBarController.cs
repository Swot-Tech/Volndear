using System;
using UIKit;

namespace Volndear.iOS
{
	public class TabBarController:UITabBarController
	{
		UIViewController Events, Maps, Profile;
		public TabBarController()
		{
			Events = new UIViewController();
			Events.Title = "Events";
			Events.View.BackgroundColor = UIColor.Green;

			Maps = new UIViewController();
			Maps.Title = "Near Me";
			Maps.View.BackgroundColor = UIColor.Orange;

			Profile = new UIViewController();
			Profile.Title = "Profile";
			Profile.View.BackgroundColor = UIColor.Red;

			var tabs = new UIViewController[] {
								Events, Maps, Profile
						};

			ViewControllers = tabs;
		}


		public static void CreateNavigationOverTabViewController(bool isLoginView,
		ref UINavigationController tab1NavController,
		 ref UINavigationController tab2NavController,ref UINavigationController tab3NavController,
		 
			 ref UITabBarController tabBarController
		)
		{
			if (isLoginView)
			{
				var viewController1 = new HomeScreenViewController()
				{ 
					
				Title = "Login",
				
				};
				UIImage eventsImages = UIImage.FromBundle("Images/event.png");
				var eventsTabBar = new UITabBarItem("Events", eventsImages, 1);
				viewController1.TabBarItem = eventsTabBar;
				tab1NavController = new UINavigationController(viewController1);

			}
			else

			{
				var viewController1 = new EventsViewController()
				{
					Title = "Events",
				};
				UIImage eventsImages = UIImage.FromBundle("Images/event.png");
				var eventsTabBar = new UITabBarItem("Events", eventsImages, 1);
				viewController1.TabBarItem = eventsTabBar;
				tab1NavController = new UINavigationController(viewController1);
			}

			var viewController2 = new MapViewController()
			{
				Title = "Near me",
			};
			UIImage MapsImg = UIImage.FromBundle("Images/location-pin.png");
			var chatsTabBar = new UITabBarItem("Near Me", MapsImg, 2);
			viewController2.TabBarItem = chatsTabBar;

			var viewController3 = new HomeScreenViewController()
			{

			};
			UIImage profileImages = UIImage.FromBundle("Images/logout.png");
			var profileTabBar = new UITabBarItem("Log Out", profileImages, 1);
			viewController3.TabBarItem = profileTabBar;
		

			//tab1NavController = new UINavigationController(viewController1);
			tab2NavController = new UINavigationController(viewController2);
			tab3NavController = new UINavigationController(viewController3);

			tabBarController = new UITabBarController();

			tabBarController.ViewControllers = new UIViewController[] {
				tab1NavController,
				tab2NavController,
				tab3NavController,
				//tab4NavController,
			};
		}

	}
}
