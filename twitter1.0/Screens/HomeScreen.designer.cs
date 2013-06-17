// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Twitter
{
	[Register ("HomeScreen")]
	partial class HomeScreen
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnSHowTwit { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnSHowTwit != null) {
				btnSHowTwit.Dispose ();
				btnSHowTwit = null;
			}
		}
	}
}
