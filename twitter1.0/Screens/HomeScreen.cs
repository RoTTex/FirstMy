using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Social;
using Xamarin.Social.Services;
using Xamarin.Auth;
using Newtonsoft;
using Newtonsoft.Json;

namespace Twitter
{
	public partial class HomeScreen : UIViewController
	{		
		public Action<string> TabSelected;



		private int _count = 5;
		private TwitterConnectoin _twitterConection;
		private string _tag;
		private UITableView _table;
		private TableSource _tableSource = new TableSource ();
		private UIBarButtonItem _btnInfo = new UIBarButtonItem();
		private LoadingOverlay _loadingOverlay;



		public HomeScreen (string tag, TwitterConnectoin twitterConection) : base ()
		{
			Title = tag;
			_twitterConection = twitterConection;
			_tag = tag;
		}




		private new void Init()
		{
			//_tableSource = new TableSource ();


			_twitterConection.TweetsTaken += (json) => 
			{
				JsonParser parser = new JsonParser();
				var tweets = parser.ParseString(json);

				_tableSource.AddRange(tweets);
				InvokeOnMainThread(_table.ReloadData);

				InvokeOnMainThread(_loadingOverlay.Hide);
			};

			_tableSource.SelectionChanged += (twit) => 
			{
				var twitter = new TwitScreen(twit);
				this.NavigationController.PushViewController(twitter, true);
			};
			_table.Source = _tableSource;

			OnTabSelected ();
		}

		private void OnTabSelected()
		{
			if (TabSelected != null)
				TabSelected (_tag);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			AddComponents ();

			Init ();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			if (_twitterConection.IsAuthenticated)
			{
				_loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds);
				View.Add (_loadingOverlay);

				_twitterConection.GeTwittstByTag(_tag, _count);
				return;
			}

			_twitterConection.AuthenricationComplete += delegate() 
			{
				DismissViewController(true, null);
			};
			PresentViewController(_twitterConection.GetAuthenticateUI(), false, () => { });
		}

		private void AddComponents()
		{
			View.ContentMode = UIViewContentMode.ScaleToFill;
			var imgView = new UIImageView (View.Bounds);
			imgView.Image = new UIImage (@"Tweets/bg.png");
			_table = new UITableView(View.Bounds);	
			_table.ContentMode = UIViewContentMode.ScaleToFill;
			_table.RowHeight = 50;
			_table.BackgroundView = imgView;
			Add (_table);

			var btn = UIButton.FromType (UIButtonType.RoundedRect);
			btn.SetTitle("Показать еще", UIControlState.Normal);
			btn.Font = UIFont.FromName("HelveticaNeue-Bold", 17);
			btn.Frame = new RectangleF (0, 0, 290, 50);
			btn.Frame = new RectangleF (15, _table.Frame.Bottom + 15, 290, 60);
			btn.TouchUpInside += (sender, e) => 
			{
				_loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds);
				View.Add (_loadingOverlay);
				_count += 5;
				_twitterConection.GeTwittstByTag(_tag, _count);
			};
			//Add (btn);
			_tableSource.BtnAdd = btn;
			//_table.AddSubview (btn);

			_btnInfo = new UIBarButtonItem ();
			_btnInfo.Title = "Инфо";
			_btnInfo.Clicked += (sender, e) => {
				InfoScreen infoScreen = new InfoScreen();
				NavigationController.PushViewController (infoScreen, true);
			};			
			NavigationController.NavigationBar.TopItem.RightBarButtonItem = _btnInfo;
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			_table.Frame = View.Frame;
		}
	}
}

