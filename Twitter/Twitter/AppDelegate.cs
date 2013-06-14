using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Twitter
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		TwitterConnectoin _twitterConnection = new TwitterConnectoin();

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			AddNavigationController();

			window.MakeKeyAndVisible ();			
			return true;
		}

		private void AddNavigationController()
		{
			var button = new UIBarButtonItem();
			button.Title = "Инфо";

			var controllers = new UIViewController[4];
			controllers [0] = GetController (@"Twitter", @"TabBar/icon_twitter.png");
			controllers [1] = GetController (@"Dribbble", @"TabBar/icon_dribbble.png");
			controllers [2] = GetController (@"Apple", @"TabBar/icon_apple.png");
			controllers [3] = GetController (@"GitHub", @"TabBar/icon_github.png");

			var navi = new UINavigationController ();
			var tabBarController = new UITabBarController();
			tabBarController.ViewControllers = controllers;
			navi.PushViewController (tabBarController, true);
			
			button.Clicked += (sender, e) => {
				InfoScreen infoScreen = new InfoScreen();
				navi.PushViewController (infoScreen, true);
			};			
			navi.NavigationBar.TopItem.SetRightBarButtonItem (button, true);
			window.RootViewController = navi;
		}

		private UIViewController GetController(string tag, string imageString)
		{
			HomeScreen homeScreen = new HomeScreen (tag, _twitterConnection);
			homeScreen.TabBarItem = new UITabBarItem (tag, new UIImage(imageString), 0);
			return homeScreen;
		}
	}
}

