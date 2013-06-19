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



		private int _clickCount = 1;
		private int _count = 0;
		private TwitterConnectoin _twitterConection;
		private string _tag;
		private UITableView _table;
		private TableSource _tableSource = new TableSource ();
		private UIBarButtonItem _btnInfo = new UIBarButtonItem();
		private UIAlertView _alert = new UIAlertView ();
		private bool _isLoaded = false;
		private bool _isSelected = false;



		public HomeScreen (string tag, TwitterConnectoin twitterConection) : base ()
		{
			Title = tag;
			_twitterConection = twitterConection;
			_tag = tag;
		}




		private new void Init()
		{
			_twitterConection.TweetsTaken += (json) => 
			{
				JsonParser parser = new JsonParser();
				var tweets = parser.ParseString(json);

				_tableSource.AddRange(tweets);
				InvokeOnMainThread(_table.ReloadData);

				InvokeOnMainThread(CloseAlert);

				_isLoaded = true;
			};

			_tableSource.SelectionChanged += (twit) => 
			{
				if (_isSelected)
					return;
				_isSelected = true;
				var twitter = new TwitScreen(twit);
				this.NavigationController.PushViewController(twitter, true);
			};
			_table.Source = _tableSource;

			OnTabSelected ();
		}

		private void CloseAlert()
		{
			_alert.DismissWithClickedButtonIndex(0, true);
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

		}

		public override void ViewDidAppear (bool animated)
		{
//			if (_isLoaded)
//			{
//				DidRotate(new UIInterfaceOrientation());
//				return;
//			}

			base.ViewDidAppear (animated);
			
			AddComponents ();

			Init ();

			var lbl = new UILabel (new RectangleF(100,0,100,30));
			lbl.Text = "Загрузка";
			lbl.BackgroundColor = UIColor.FromRGBA (0, 0, 0, 0);
			lbl.TextColor = UIColor.White;
			var activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
			activitySpinner.Frame = new RectangleF (0,-35,50,50);
			activitySpinner.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			activitySpinner.StartAnimating ();
			_alert = new UIAlertView();
			_alert.Frame.Size = new SizeF (60, 60);
			_alert.AddSubview(activitySpinner);
			_alert.AddSubview (lbl);
			_alert.Show();
			_twitterConection.GeTwittstByTag(_tag, GetNumberOfRows());
			_isSelected = false;
		}

		private int GetNumberOfRows(bool isAdd = false)
		{
			var temp = ((int)(View.Frame.Height / 50) - 1) * _clickCount;
			if (_count < temp)
				_count = temp;
			else if (isAdd)
				_count += temp;
			return _count;
		}

		private void AddComponents()
		{
			_table = new UITableView ();	
			//_table.Frame = new RectangleF(0,0, View.Frame.Width, View.Frame.Height);
			_table.RowHeight = 50;
			_table.BackgroundColor = UIColor.FromPatternImage(new UIImage (@"Tweets/bg.png"));
			Add (_table);

			var btn = UIButton.FromType (UIButtonType.RoundedRect);
			btn.SetTitle("Показать еще", UIControlState.Normal);
			btn.Font = UIFont.FromName("HelveticaNeue-Bold", 17);
			btn.Frame = new RectangleF (15, 5, View.Frame.Width - 30, 40);;
			btn.TouchUpInside += (sender, e) => 
			{
				_isLoaded= false;
				_alert.Show();
				_clickCount++;
				_twitterConection.GeTwittstByTag(_tag, GetNumberOfRows(true));
			};
			_tableSource.BtnAdd = btn;

			_btnInfo = new UIBarButtonItem ();
			_btnInfo.Title = "Инфо";
			_btnInfo.Clicked += (sender, e) => {
				InfoScreen infoScreen = new InfoScreen();
				NavigationController.PushViewController (infoScreen, true);
			};			
			NavigationController.NavigationBar.TopItem.RightBarButtonItem = _btnInfo;

			SetFrame ();
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			SetFrame ();
		}

		private void SetFrame()
		{
			_table.Frame = new RectangleF(0,0, View.Frame.Width, View.Frame.Height);
			_tableSource.BtnAdd.Frame = new RectangleF (15, 5, View.Frame.Width - 30, 40);
		}
	}
}

