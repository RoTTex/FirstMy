using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Twitter
{
	public partial class InfoScreen : UIViewController
	{
		public InfoScreen () : base ("InfoScreen", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			var img = new UIImage (@"info/logo.png");
			var imgView = new UIImageView(img);
			imgView.Frame = new RectangleF (0, 20, 320, img.Size.Height);
			imgView.ContentMode = UIViewContentMode.Center;
			Add (imgView);

			var text = new UITextView (new RectangleF(10, img.Size.Height + 30, 300, 200));
			text.Font = UIFont.FromName("HelveticaNeue", 13);
			text.Text = "Нам не стыдно за выпускаемые продукты, все они сделаны с вниманием к деталям. Пользователи это ценят, многие наши приложения попадают в топы AppStore и получают высокие оценки. \n\nМы любим своих заказчиков и решаем их задачи. На письма и телефон отвечаем быстро, по праздникам и выходным, делаем работу в срок и никуда не пропадаем.\nЗакажите разработку сейчас! ";
			text.TextAlignment = UITextAlignment.Left;
			text.TextColor = UIColor.Black;
			text.ScrollEnabled = true;
			Add (text);

			var imgCallButton = new UIImage (@"Info/button.png").StretchableImage (11, 0);
			var imgCallButton_pressed = new UIImage (@"Info/button_pressed.png").StretchableImage (11, 0);
			var imgCallButtonIcon = new UIImage (@"Info/icon_phone.png");
			var btnCall = UIButton.FromType (UIButtonType.Custom);
			btnCall.Frame = new RectangleF (10, 380, 130, 60);
			btnCall.SetBackgroundImage(imgCallButton, UIControlState.Normal);
			btnCall.SetBackgroundImage(imgCallButton_pressed, UIControlState.Highlighted);
			btnCall.SetImage (imgCallButtonIcon, UIControlState.Normal);
			btnCall.ContentMode = UIViewContentMode.Center;
			Add (btnCall);

			var imgMailButton = new UIImage (@"Info/button.png").StretchableImage (11, 0);
			var imgMailButton_pressed = new UIImage (@"Info/button_pressed.png").StretchableImage (11, 0);
			var imgMailButtonIcon = new UIImage (@"Info/icon_mail.png");
			var btnMail = UIButton.FromType (UIButtonType.Custom);
			btnMail.Frame = new RectangleF (180, 380, 130, 60);
			btnMail.SetBackgroundImage(imgMailButton, UIControlState.Normal);
			btnMail.SetBackgroundImage(imgMailButton_pressed, UIControlState.Highlighted);
			btnMail.SetImage (imgMailButtonIcon, UIControlState.Normal);
			Add (btnMail);
		}

		public override void ViewWillAppear (bool animated) {
			base.ViewWillAppear (animated);
			this.NavigationController.SetNavigationBarHidden (true, animated);
		}
		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			this.NavigationController.SetNavigationBarHidden (false, animated);
		}
	}
}

