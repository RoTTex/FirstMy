using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreImage;
using System.Text;

namespace Twitter
{
	public partial class TwitScreen : UIViewController
	{
		private Twit _tweet;
		private UITextView _text = new UITextView ();
		private UIImageView _imgView = new UIImageView();
		private UILabel _lblStatus = new UILabel();
		private UIImageView _imgViewLine = new UIImageView();
		private UILabel _lblUnderline = new UILabel();



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
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			AddComponents ();
		}

		private void AddComponents()
		{			
			View.BackgroundColor = UIColor.FromPatternImage(new UIImage (@"Tweets/bg.png"));

			var nsUrl = new NSUrl (_tweet.ImagePath);
			var nsData = NSData.FromUrl(nsUrl);
			var img = new UIImage (nsData != null ? nsData : @"Main/avatar.png");

			_imgView = new UIImageView (CreateMaskToImage(img));
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
			_text.Frame = new RectangleF (10, _imgView.Frame.Bottom + 10, View.Frame.Width - 20, _text.ContentSize.Height);

			_imgViewLine = new UIImageView (new RectangleF(10, _text.Frame.Bottom + 5, 200, 1));
			_imgViewLine.Image = new UIImage (@"Tweets/line.png");
			Add (_imgViewLine);

			_lblUnderline = new UILabel (new RectangleF(10, _imgViewLine.Frame.Bottom, View.Frame.Width, 30));
			_lblUnderline.Font = UIFont.FromName("HelveticaNeue", 10);
			_lblUnderline.TextColor = UIColor.FromRGB(119,119,119);
			_lblUnderline.Text = String.Format("{0:d.M.yyyy}   " + _tweet.Link, GetDate(_tweet.PostDate));
			_lblUnderline.BackgroundColor = new UIColor(0,0,0,0);
			Add (_lblUnderline);
			_imgViewLine.Frame.Size = new SizeF(_lblUnderline.IntrinsicContentSize.Width,1);
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			_lblStatus.Frame = new RectangleF(_imgView.Frame.Right + 18, _lblStatus.Frame.Top, View.Frame.Width - _imgView.Frame.Right - 28, 30);
			_text.Frame = new RectangleF (10, _imgView.Frame.Bottom + 10, View.Frame.Width - 20, 60);
			_text.Frame = new RectangleF (10, _imgView.Frame.Bottom + 10, View.Frame.Width - 20, _text.ContentSize.Height);
			_imgViewLine.Frame = new RectangleF(10, _text.Frame.Bottom + 5, 200, 1);
			_lblUnderline.Frame = new RectangleF (10, _imgViewLine.Frame.Bottom, View.Frame.Width, 30);
		}

		private DateTime GetDate(string dateTime)
		{
			string[] temp = dateTime.Split (' ');
			var strBuildr = new StringBuilder ();
			strBuildr.Append (temp[0]);
			strBuildr.Append (" ");
			strBuildr.Append (temp[1]);
			strBuildr.Append (" ");
			strBuildr.Append (temp[2]);
			return DateTime.Parse (strBuildr.ToString());
		}
				
		private UIImage CreateMaskToImage(UIImage image)
		{
			var maskRef = new UIImage (@"Main/mask_avatar_mini.png").CGImage; 

			var maskedImage = image.CGImage.WithMask(CGImage.CreateMask(
				maskRef.Width,
				maskRef.Height,
				maskRef.BitsPerComponent,
				maskRef.BitsPerPixel,
				maskRef.BytesPerRow,
				maskRef.DataProvider,
				null,
				false
				));
			return UIImage.FromImage(maskedImage);
		}
	}
}

