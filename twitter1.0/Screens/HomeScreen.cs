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
		private int _count = 5;
		private TwitterConnectoin _twitterConection;
		private string _tag;
		private UITableView _table;
		private TableSource _tableSource;

		public HomeScreen (string tag, TwitterConnectoin twitterConection) : base ("HomeScreen", null)
		{
			_twitterConection = twitterConection;
			_tag = tag;
		}

		private new void Init()
		{
			_tableSource = new TableSource ();

			_twitterConection.TweetsTaken += (json) => 
			{
				JsonParser parser = new JsonParser();
				var tweets = parser.ParseString(json);

				_tableSource.AddRange(tweets);
				InvokeOnMainThread(_table.ReloadData);
			};

			_tableSource.SelectionChanged += (twit) => 
			{
				var twitter = new TwitScreen(twit);
				this.NavigationController.PushViewController(twitter, true);
			};
			_table.Source = _tableSource;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var imgView = new UIImageView (View.Bounds);
			imgView.Image = new UIImage (@"Tweets/bg.png");
			_table = new UITableView(new RectangleF(0, 0, 320, 250));	
			_table.RowHeight = 50;
			_table.BackgroundView = imgView;
			Add (_table);

			var btn = UIButton.FromType (UIButtonType.RoundedRect);
			btn.SetTitle("Показать еще", UIControlState.Normal);
			btn.Font = UIFont.FromName("HelveticaNeue-Bold", 17);
			btn.Frame = new RectangleF (15, _table.Frame.Bottom + 15, 290, 60);
			btn.TouchUpInside += (sender, e) => 
			{
				_count += 5;
				_twitterConection.GeTwittstByTag(_tag, _count);
			};
			Add (btn);

			Init ();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			if (_twitterConection.IsAuthenticated)
			{
				_twitterConection.GeTwittstByTag(_tag, _count);
				return;
			}

			_twitterConection.AuthenricationComplete += delegate() 
			{
				DismissViewController(true, null);
			};
			PresentViewController(_twitterConection.GetAuthenticateUI(), false, () => { });

		}
	}
}

