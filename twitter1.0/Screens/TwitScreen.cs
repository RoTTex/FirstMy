using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreImage;

namespace Twitter
{
	public partial class TwitScreen : UIViewController
	{
		private Twit _tweet;
		private UITextView _text = new UITextView ();
		private UIImageView _imgView = new UIImageView();
		private UILabel _lblStatus = new UILabel();



		public TwitScreen () : base ()
		{
			this.Title = "Твит";
		}

		public TwitScreen (Twit tweet) : base ()
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

			AddComponents ();
		}

		private void AddComponents()
		{			
			View.BackgroundColor = UIColor.FromPatternImage(new UIImage (@"Tweets/bg.png"));

			var nsUrl = new NSUrl (_tweet.ImagePath);
			var nsData = NSData.FromUrl(nsUrl);
			var img = new UIImage (nsData != null ? nsData : @"Main/avatar.png");

			_imgView = new UIImageView (img);
			_imgView.Frame = new RectangleF (15, 28, 64, 64);
			Add (_imgView);

			var lblName = new UILabel (new RectangleF(_imgView.Frame.Right + 18, 32, View.Frame.Width - _imgView.Frame.Right - 28, 40));
			lblName.Font = UIFont.FromName("HelveticaNeue-Bold", 16);
			lblName.TextColor = UIColor.FromRGB(68,100,143);
			lblName.Text = _tweet.Name;
			lblName.BackgroundColor = new UIColor(0,0,0,0);
			Add (lblName);

			_lblStatus.Frame = new RectangleF(_imgView.Frame.Right + 18, lblName.Frame.Bottom, View.Frame.Width - _imgView.Frame.Right - 28, 30);
			_lblStatus.Font = UIFont.FromName("HelveticaNeue-Bold", 12);
			_lblStatus.TextColor = UIColor.FromRGB(65,65,65);
			_lblStatus.Text = _tweet.SmallInfo;
			_lblStatus.BackgroundColor = new UIColor(0,0,0,0);
			Add (_lblStatus);

			_text = new UITextView (new RectangleF(10, _imgView.Frame.Bottom + 10, View.Frame.Width - 20, 60));
			_text.Font = UIFont.FromName("HelveticaNeue", 12);
			_text.Text = _tweet.Info;
			_text.TextAlignment = UITextAlignment.Left;
			_text.TextColor = UIColor.FromRGB(65,65,65);;
			_text.BackgroundColor = new UIColor(0,0,0,0);
			_text.UserInteractionEnabled = false;
			Add (_text);

			var imgViewLine = new UIImageView (new RectangleF(10, _text.Frame.Bottom + 5, 200, 1));
			imgViewLine.Image = new UIImage (@"Tweets/line.png");
			Add (imgViewLine);

			var lblDate = new UILabel (new RectangleF(10, imgViewLine.Frame.Bottom, 50, 30));
			lblDate.Font = UIFont.FromName("HelveticaNeue", 10);
			lblDate.TextColor = UIColor.FromRGB(119,119,119);
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

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			_lblStatus.Frame = new RectangleF(_imgView.Frame.Right + 18, _lblStatus.Frame.Top, View.Frame.Width - _imgView.Frame.Right - 28, 30);
			_text.Frame = new RectangleF (10, _imgView.Frame.Bottom + 10, View.Frame.Width - 20, 60);
		}
	}
}

