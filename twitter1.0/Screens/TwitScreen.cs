using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Twitter
{
	public partial class TwitScreen : UIViewController
	{
		private Twit _tweet;

		public TwitScreen () : base ("TwitScreen", null)
		{
		}

		public TwitScreen (Twit tweet) : base ("TwitScreen", null)
		{
			_tweet = tweet;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var imgViewBackground = new UIImageView (View.Bounds);
			imgViewBackground.Image = new UIImage (@"Tweets/bg.png");
			Add (imgViewBackground);

			var img = new UIImage (_tweet.ImagePath);
			var imgView = new UIImageView (img);
			imgView.Frame = new RectangleF (15, 20, 100, 100);
			Add (imgView);

			var lblName = new UILabel (new RectangleF(imgView.Frame.Right + 5, 40, 300, 45));
			lblName.Font = UIFont.FromName("HelveticaNeue-Bold", 16);
			lblName.TextColor = UIColor.Blue;
			lblName.Text = _tweet.Name;
			lblName.BackgroundColor = new UIColor(0,0,0,0);
			Add (lblName);

			var lblStatus = new UILabel (new RectangleF(imgView.Frame.Right + 5, lblName.Frame.Bottom, 300, 30));
			lblStatus.Font = UIFont.FromName("HelveticaNeue-Bold", 12);
			lblStatus.TextColor = UIColor.Gray;
			lblStatus.Text = _tweet.SmallInfo;
			lblStatus.BackgroundColor = new UIColor(0,0,0,0);
			Add (lblStatus);

			var text = new UITextView (new RectangleF(10, imgView.Frame.Bottom+5, 300, 100));
			text.Font = UIFont.FromName("HelveticaNeue", 12);
			text.Text = _tweet.Info;
			text.TextAlignment = UITextAlignment.Left;
			text.TextColor = UIColor.Black;
			text.BackgroundColor = new UIColor(0,0,0,0);
			Add (text);

			var imgViewLine = new UIImageView (new RectangleF(10, text.Frame.Bottom + 5, 200, 1));
			imgViewLine.Image = new UIImage (@"Tweets/line.png");
			Add (imgViewLine);

			var lblDate = new UILabel (new RectangleF(10, imgViewLine.Frame.Bottom, 50, 30));
			lblDate.Font = UIFont.FromName("HelveticaNeue", 10);
			lblDate.TextColor = UIColor.Gray;
			lblDate.Text = String.Format("{0:d.M.yyyy}", _tweet.PostDate);
			lblDate.BackgroundColor = new UIColor(0,0,0,0);
			Add (lblDate);

			var lblLink = new UILabel (new RectangleF(lblDate.Frame.Right + 5, imgViewLine.Frame.Bottom, 100, 30));
			lblLink.Font = UIFont.FromName("HelveticaNeue", 10);
			lblLink.TextColor = UIColor.Gray;
			lblLink.Text = _tweet.Link;
			lblLink.BackgroundColor = new UIColor(0,0,0,0);
			Add (lblLink);
		}
	}
}

