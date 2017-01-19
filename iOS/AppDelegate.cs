using System;
using System.IO;
using Foundation;
using Mono.Data.Sqlite;
using UIKit;
using Volndear;


namespace Volndear.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{

		UITabBarController tabBarController;
		UINavigationController tab1NavController, tab2NavController,tab3NavController;
		public static CitizenRegistrationDatabase citizenRegistrationDatabase;
		public static EventDatabase eventDatabase;
		// class-level declarations

		public override UIWindow Window
		{
			get;
			set;
		}



		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			SqliteConnection.SetConfig(SQLiteConfig.Serialized);
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method
			CreateDatabase("volndear.db3");
			databaseInitialization("volndear.db3");
			Window = new UIWindow(UIScreen.MainScreen.Bounds);
			// create our nav controller
			//var navigationController = new UINavigationController(new EventsViewController());
			//navigationController.NavigationBar.Translucent = false;
			//Window.RootViewController = navigationController;

			Citizen citizen = new Citizen();
			citizen = AppDelegate.citizenRegistrationDatabase.GetCitizen();
			if (citizen != null)
			{ 	TabBarController.CreateNavigationOverTabViewController(false, ref tab1NavController,
						ref tab2NavController,ref tab3NavController ,ref tabBarController);

				Window.RootViewController = tabBarController;
				// If you have defined a root view controller, set it here:

				Window.MakeKeyAndVisible();
			
			}
			else
			{
				TabBarController.CreateNavigationOverTabViewController(true, ref tab1NavController,
						ref tab2NavController, ref tab3NavController, ref tabBarController);

				Window.RootViewController = tabBarController;
				// If you have defined a root view controller, set it here:

				Window.MakeKeyAndVisible();

			}



			return true;
		}

		public override void OnResignActivation(UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground(UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated(UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate(UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}


		public static void CreateDatabase(
		String filename)
		{
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			//---destination path for file in the Documents 
			// folder---
			var destinationPath =
				System.IO.Path.Combine(documentsPath, filename);

			//---path of source file---

			var sourcePath =
				System.IO.Path.Combine(Environment.CurrentDirectory,
					filename);
			//---print for verfications---
			Console.WriteLine(destinationPath);
			Console.WriteLine(sourcePath);

			try
			{
				//---copy only if file does not exist---
				if (!File.Exists(destinationPath))
				{
					File.Copy(sourcePath, destinationPath, true);
					Console.WriteLine("overwritten");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private void databaseInitialization(string sqliteFilename)
		{
			string documentsPath = System.Environment.CurrentDirectory;

			documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var path = System.IO.Path.Combine(documentsPath, sqliteFilename);

			citizenRegistrationDatabase = new CitizenRegistrationDatabase(path);
			eventDatabase = new EventDatabase(path);

		}

	}
}

