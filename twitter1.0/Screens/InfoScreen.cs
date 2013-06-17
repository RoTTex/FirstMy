using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Twitter
{
	public partial class InfoScreen : UIViewController
	{
		private UIButton _btnCall = UIButton.FromType (UIButtonType.Custom);
		private UIButton _btnMail = UIButton.FromType (UIButtonType.Custom);
		private UITextView _text = new UITextView ();


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

			AddComponents ();
		}

		private void AddComponents()
		{
			var imgBackground = new UIImage (@"Tweets/bg.png").StretchableImage (50, 0);
			var imgViewBackground = new UIImageView (imgBackground);
			View.AddSubview (imgViewBackground);

			var img = new UIImage (@"info/logo.png");
			var imgView = new UIImageView(img);
			imgView.Frame = new RectangleF (0, 5, View.Frame.Right -  20, img.Size.Height);
			imgView.ContentMode = UIViewContentMode.Center;
			Add (imgView);

			_text.Frame = new RectangleF(10, imgView.Frame.Bottom + 1, View.Frame.Right -  20, 180);
			_text.Font = UIFont.FromName("HelveticaNeue", 13);
			_text.Text = "Нам не стыдно за выпускаемые продукты, все они сделаны с вниманием к деталям. Пользователи это ценят, многие наши приложения попадают в топы AppStore и получают высокие оценки. \nМы любим своих заказчиков и решаем их задачи. На письма и телефон отвечаем быстро, по праздникам и выходным, делаем работу в срок и никуда не пропадаем.\nЗакажите разработку сейчас! ";
			_text.TextAlignment = UITextAlignment.Left;
			_text.TextColor = UIColor.FromRGB (65, 65, 65);
			_text.ScrollEnabled = true;
			_text.UserInteractionEnabled = false;
			_text.BackgroundColor = new UIColor (0, 0, 0, 0);
			Add (_text);

			var imgButton = new UIImage (@"Info/button.png").StretchableImage (11,0);
			var imgButton_pressed = new UIImage (@"Info/button_pressed.png").StretchableImage (11,0);

			var imgCallButtonIcon = new UIImage (@"Info/icon_phone.png");
			_btnCall.Frame = new RectangleF (10, _text.Frame.Bottom + 1, 130, 50);
			_btnCall.SetBackgroundImage(imgButton, UIControlState.Normal);
			_btnCall.SetBackgroundImage(imgButton_pressed, UIControlState.Highlighted);
			_btnCall.SetImage (imgCallButtonIcon, UIControlState.Normal);
			_btnCall.ContentMode = UIViewContentMode.Top;
			_btnCall.TouchUpInside += (sender, e) => 
			{
				BeginInvokeOnMainThread (() => {new UIAlertView ("Phone", "7-777-2234455", null, "OK").Show ();}); 
			};
			Add (_btnCall);

			var imgMailButtonIcon = new UIImage (@"Info/icon_mail.png");
			_btnMail.Frame = new RectangleF (View.Frame.Right - 140, _text.Frame.Bottom + 1, 130, 50);
			_btnMail.SetBackgroundImage(imgButton, UIControlState.Normal);
			_btnMail.SetBackgroundImage(imgButton_pressed, UIControlState.Highlighted);
			_btnMail.SetImage (imgMailButtonIcon, UIControlState.Normal);
			_btnMail.TouchUpInside += (sender, e) => 
			{
				BeginInvokeOnMainThread (() => {new UIAlertView ("Site", "www.Some.com", null, "OK").Show ();});
			};
			Add (_btnMail);
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			_btnCall.Frame = new RectangleF (10, _text.Frame.Bottom + 1, 130, 50);
			_btnMail.Frame = new RectangleF (View.Frame.Right - 140, _text.Frame.Bottom + 1, 130, 50);

		}
	}
}

