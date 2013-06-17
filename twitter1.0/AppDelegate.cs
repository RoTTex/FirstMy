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
		UINavigationController navi = new UINavigationController ();

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			AddNavigationController();

			window.MakeKeyAndVisible ();			
			return true;
		}

		private void AddNavigationController()
		{
			var tabBarController = new UITabBarController();
			tabBarController.ViewControllers = new UIViewController[] { 
				CreateController (@"Twitter", @"TabBar/icon_twitter.png"),
				CreateController (@"Dribbble", @"TabBar/icon_dribbble.png"),
				CreateController (@"Apple", @"TabBar/icon_apple.png"),
				CreateController (@"GitHub", @"TabBar/icon_github.png")
			};
			window.RootViewController = tabBarController;
		}

		private UIViewController CreateController(string tag, string imageString)
		{
			var homeScreen = new HomeScreen (tag, _twitterConnection);
			homeScreen.Title = tag;
			homeScreen.TabBarItem = new UITabBarItem (tag, new UIImage(imageString), 0);
			return new UINavigationController(homeScreen);
		}

	}
}

