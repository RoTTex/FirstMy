using System;
using System.Collections.Generic;
using Xamarin.Social;
using Xamarin.Social.Services;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Auth;

namespace Twitter
{
	public class TwitterConnectoin
	{		
		public bool IsAuthenticated { get; private set; }
		private Account _account;
		public event Action AuthenricationComplete;
		public event Action<string> TweetsTaken;

		private static readonly TwitterService Twitter = new TwitterService {
			ConsumerKey = "OxccMOQobPX3FfvzyTEA",
			ConsumerSecret = "h2eyjBcZFxcN2FOQGmWNNZ2MenIYzWRAJMCMHZBKs",
			CallbackUrl = new Uri ("http://stampsy.com")
		};

		public TwitterConnectoin ()
		{
		}

		public UIViewController GetAuthenticateUI()
		{	
			return Twitter.GetAuthenticateUI (acc =>
			{
				_account = acc; 
				IsAuthenticated = true;
				//AccountStore.Create().Save(acc, "Twitter");
				OnAuthenricationComplete();
			});
		}

		private void OnAuthenricationComplete()
		{
			if (AuthenricationComplete != null)
				AuthenricationComplete ();
		}

		public void GeTwittstByTag(string tag, int count)
		{	
			var req = Twitter.CreateRequest ("GET", new Uri ("https://api.twitter.com/1.1/search/tweets.json"), new Dictionary<string, string> {
				{ "q", "%23" + tag },
				{ "count", count.ToString() }
			}, _account);
			req.GetResponseAsync ().ContinueWith (r => {
				if (r.IsFaulted)
				{
					OnTweetsTaken("");
				} else 
				{
					OnTweetsTaken(r.Result.GetResponseText ());
				}
			});
		}

		private void OnTweetsTaken(string str)
		{
			if (TweetsTaken != null)
				TweetsTaken (str);
		}
	}
}

