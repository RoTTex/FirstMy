using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Twitter
{
	public class TableSource : UITableViewSource 
	{
		public event Action<Twit> SelectionChanged;



		private List<Twit> _tableItems = new List<Twit>();
		private string _cellIdentifier = "TableCell";
		private UILabel _lblTime = new UILabel();



		public UIButton BtnAdd; 



		public TableSource ()
		{
		}

		public TableSource (List<Twit> items)
		{
			_tableItems = items;
		}



		public override int RowsInSection (UITableView tableview, int section)
		{
			return _tableItems.Count;
		}

		public void AddRange(List<Twit> twits)
		{
			_tableItems = twits;
			_tableItems.Add (new Twit());
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			if (indexPath.Row >= _tableItems.Count || _tableItems [indexPath.Row].Name == "Some Man")
				return GetButtonCell ();

			StringBuilder stringBuilder = new StringBuilder ();
			int length = _tableItems [indexPath.Row].Info.Length > 30 ? 30 : _tableItems [indexPath.Row].Info.Length;
			for (int i = 0; i < length; i++)
				stringBuilder.Append (_tableItems [indexPath.Row].Info[i]);

			_lblTime = new UILabel ();
			_lblTime.BackgroundColor = new UIColor (0, 0, 0, 0);
			_lblTime.Text = "1";
			_lblTime.TextColor = UIColor.FromRGB (65, 65, 65);

			var btn = new UIButton (new RectangleF(270,5,40,40));
			btn.BackgroundColor = UIColor.Black;

			var nsUrl = new NSUrl (_tableItems [indexPath.Row].ImagePath);
			var nsData = NSData.FromUrl(nsUrl);
			var cell = new UITableViewCell (UITableViewCellStyle.Subtitle, _cellIdentifier);
			//cell.AddSubview(_lblTime);
			_lblTime.Frame = new RectangleF(400 - 50, 5, 40, 40);
			cell.ImageView.Image = new UIImage(nsData != null ? nsData : @"Main/avatar.png");

			cell.TextLabel.Text = _tableItems [indexPath.Row].Name;
			cell.TextLabel.Font = UIFont.FromName("HelveticaNeue-Bold", 17);
			cell.DetailTextLabel.Text = stringBuilder.ToString();
			cell.DetailTextLabel.Font = UIFont.FromName("HelveticaNeue", 13);
			cell.DetailTextLabel.TextColor = UIColor.FromRGB (65, 65, 65);

			return cell;
		}

		private UITableViewCell GetButtonCell()
		{
			var cell = new UITableViewCell (UITableViewCellStyle.Default, _cellIdentifier);
			cell.AddSubview (BtnAdd);
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{			
			tableView.DeselectRow (indexPath, true);
			if (SelectionChanged != null)
				SelectionChanged (_tableItems [indexPath.Row]);
		}
	}
}

