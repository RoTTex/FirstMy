using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Text;

namespace Twitter
{
	public class TableSource : UITableViewSource {
		List<Twit> _tableItems = new List<Twit>();
		string _cellIdentifier = "TableCell";

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
			StringBuilder stringBuilder = new StringBuilder ();
			int length = _tableItems [indexPath.Row].Info.Length > 30 ? 30 : _tableItems [indexPath.Row].Info.Length;
			for (int i = 0; i < length; i++)
				stringBuilder.Append (_tableItems [indexPath.Row].Info[i]);

			UITableViewCell cell = new UITableViewCell (UITableViewCellStyle.Subtitle, _cellIdentifier);
			cell.ImageView.Image = new UIImage(_tableItems [indexPath.Row].ImagePath);

			cell.TextLabel.Text = _tableItems [indexPath.Row].Name;
			cell.TextLabel.Font = UIFont.FromName("HelveticaNeue-Bold", 17);
			cell.DetailTextLabel.Text = stringBuilder.ToString();
			cell.DetailTextLabel.Font = UIFont.FromName("HelveticaNeue", 13);

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

