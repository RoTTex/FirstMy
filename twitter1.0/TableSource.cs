using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Twitter
{
	public class TableSource : UITableViewSource {
		List<Twit> _tableItems = new List<Twit>();
		string _cellIdentifier = "TableCell";
		public UIButton BtnAdd; 

		public event Action<Twit> SelectionChanged;

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
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			//if (_tableItems [indexPath.Row].Name == "Some Man")
			//	return GetButtonCell ();

			StringBuilder stringBuilder = new StringBuilder ();
			int length = _tableItems [indexPath.Row].Info.Length > 30 ? 30 : _tableItems [indexPath.Row].Info.Length;
			for (int i = 0; i < length; i++)
				stringBuilder.Append (_tableItems [indexPath.Row].Info[i]);

			var lbl = new UILabel (new RectangleF(270, 5, 40, 40));
			lbl.BackgroundColor = new UIColor (0, 0, 0, 0);
			lbl.Text = "1";
			lbl.TextColor = UIColor.FromRGB (65, 65, 65);

			var nsUrl = new NSUrl (_tableItems [indexPath.Row].ImagePath);
			var nsData = NSData.FromUrl(nsUrl);
			var cell = new UITableViewCell (UITableViewCellStyle.Subtitle, _cellIdentifier);
			cell.AddSubview(lbl);
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

