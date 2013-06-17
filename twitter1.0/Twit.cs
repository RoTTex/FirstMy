using System;
using MonoTouch.UIKit;

namespace Twitter
{
	public class Twit
	{
		public string ImagePath { get; set; }
		public string Name { get; set; }
		public string Info { get; set; }
		public string SmallInfo { get; set; }
		public string PostDate { get; set; }
		public string Link { get; set; }

		public Twit ()
		{
			Name = "Some Man";
			ImagePath = @"main/avatar.png";
			Info = "Some info yrhfygutjgyfhryfhrygjtngkjgrjnggurghfjgybhdjkksuughrkondnhghhd jsfjngjeijgksmkmge  dnsniosdgis";
			SmallInfo = "Some Add";
			PostDate = DateTime.Now.ToString();
			Link = "www.SomeLinq.ru";
		}
	}
}

