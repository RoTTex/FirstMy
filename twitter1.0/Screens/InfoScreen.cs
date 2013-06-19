using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Twitter
{
	public partial class InfoScreen : UIViewController
	{
		private UIScrollView _scroll = new UIScrollView();
		private UIButton _btnCall = UIButton.FromType (UIButtonType.Custom);
		private UIButton _btnMail = UIButton.FromType (UIButtonType.Custom);
		private UIImageView _imgView = new UIImageView();
		private UITextView _text = new UITextView();



		public InfoScreen () : base ()
		{
		}



		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			AddComponents ();
		}

		private void AddComponents()
		{
			View.BackgroundColor = UIColor.FromPatternImage (new UIImage (@"Tweets/bg.png"));
			_scroll.Frame = View.Frame;
			_scroll.ScrollEnabled = true;
			//_scroll.ContentSize = new SizeF (100, 500);
			Add(_scroll);

			var img = new UIImage (@"info/logo.png");
			_imgView = new UIImageView(img);
			//_imgView.Frame = new RectangleF (0, 5, View.Frame.Width, img.Size.Height);
			_imgView.ContentMode = UIViewContentMode.Center;
			_scroll.AddSubview (_imgView);

			//_text.Frame = new RectangleF(10, _imgView.Frame.Bottom + 1, View.Frame.Right -  20, View.Frame.Height == 219 ? 120 : 180);
			_text.Font = UIFont.FromName("HelveticaNeue", 13);
			_text.Text = "Нам не стыдно за выпускаемые продукты, все они сделаны с вниманием к деталям. Пользователи это ценят, многие наши приложения попадают в топы AppStore и получают высокие оценки. \nМы любим своих заказчиков и решаем их задачи. На письма и телефон отвечаем быстро, по праздникам и выходным, делаем работу в срок и никуда не пропадаем.\nЗакажите разработку сейчас! ";
			_text.TextAlignment = UITextAlignment.Left;
			_text.TextColor = UIColor.FromRGB (65, 65, 65);
			_text.ScrollEnabled = true;
			_text.UserInteractionEnabled = false;
			_text.BackgroundColor = new UIColor (0, 0, 0, 0);
			_scroll.AddSubview (_text);
			_text.Frame = new RectangleF(10, _imgView.Frame.Bottom + 1, View.Frame.Right -  20, _text.ContentSize.Height);

			var imgButton = new UIImage (@"Info/button.png").StretchableImage (11,0);
			var imgButton_pressed = new UIImage (@"Info/button_pressed.png").StretchableImage (11,0);

			var imgCallButtonIcon = new UIImage (@"Info/icon_phone.png");		
			var imgCallButtonIconView = new UIImageView (imgCallButtonIcon);
			imgCallButtonIconView.Frame = new RectangleF (55, 16, imgCallButtonIcon.Size.Width, imgCallButtonIcon.Size.Height);
			//_btnCall.Frame = new RectangleF (10, _text.Frame.Bottom + 5, 130, 65);
			_btnCall.SetBackgroundImage(imgButton, UIControlState.Normal);
			_btnCall.SetBackgroundImage(imgButton_pressed, UIControlState.Highlighted);
			_btnCall.AddSubview(imgCallButtonIconView);
			_btnCall.TouchUpInside += (sender, e) => 
			{
				BeginInvokeOnMainThread (() => {new UIAlertView ("Phone", "7-777-2234455", null, "OK").Show ();}); 
			};
			_scroll.AddSubview (_btnCall);

			var imgMailButtonIcon = new UIImage (@"Info/icon_mail.png");
			var imgMailButtonIconView = new UIImageView (imgMailButtonIcon);
			imgMailButtonIconView.Frame = new RectangleF (55, 16, imgMailButtonIcon.Size.Width, imgMailButtonIcon.Size.Height);
			//_btnMail.Frame = new RectangleF (_scroll.Frame.Width - 140, _text.Frame.Bottom + 5, 130, 65);
			_btnMail.SetBackgroundImage(imgButton, UIControlState.Normal);
			_btnMail.SetBackgroundImage(imgButton_pressed, UIControlState.Highlighted);
			_btnMail.AddSubview(imgMailButtonIconView);
			_btnMail.TouchUpInside += (sender, e) => 
			{
				BeginInvokeOnMainThread (() => {new UIAlertView ("Site", "www.Some.com", null, "OK").Show ();});
			};
			_scroll.AddSubview  (_btnMail);

			SetFrame ();
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			SetFrame ();
		}

		private void SetFrame()
		{
			_scroll.Frame =  View.Frame;
			_scroll.ContentSize = new SizeF (100, 500);
			_imgView.Frame = new RectangleF (0, 5, View.Frame.Width, _imgView.Frame.Height);
			_text.Frame = new RectangleF(10, _imgView.Frame.Bottom + 1, View.Frame.Right -  20, View.Frame.Height == 219 ? 120 : 190);
			_text.Frame = new RectangleF(10, _imgView.Frame.Bottom + 1, View.Frame.Right -  20, _text.ContentSize.Height);
			_btnCall.Frame = new RectangleF (10, _text.Frame.Bottom + 5, 130, 65);
			_btnMail.Frame = new RectangleF (_scroll.Frame.Width - 140, _text.Frame.Bottom + 5, 130, 65);
		}
	}
}

