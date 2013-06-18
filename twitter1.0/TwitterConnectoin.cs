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
			_account = new Account ("mopkobb88@gmail.com", new Dictionary<string, string> {
				{ "oauth_token", "1510660160-JKU5JWOMmdaODzcQZ0ljTrhjvZNRqeTH8Kz1B0r" },
				{ "oauth_token_secret", "ERE0F92Y7BOadyDP6L9esWDBMaCCEexZZrb4cUfMc" },
				{ "user_id", "1510660160" },
				{ "screen_name", "mopkobb88" },
				{ "oauth_consumer_key", "OxccMOQobPX3FfvzyTEA" },
				{ "oauth_consumer_secret", "h2eyjBcZFxcN2FOQGmWNNZ2MenIYzWRAJMCMHZBKs" }

			});
//
//			var req = Twitter.CreateRequest ("GET", new Uri ("https://api.twitter.com/oauth/authorize?oauth_token=1510660160-JKU5JWOMmdaODzcQZ0ljTrhjvZNRqeTH8Kz1B0r"),_account);
//			req.GetResponseAsync ().ContinueWith (r => {
//				if (r.IsFaulted)
//				{
//					OnTweetsTaken("");
//				} else 
//				{
//					OnTweetsTaken(r.Result.GetResponseText ());
//				}
//			});


			return Twitter.GetAuthenticateUI (acc =>
			{
				_account = acc;
				if (_account!=null)
					IsAuthenticated = true;
				AccountStore.Create().Save(acc, "Twetter");
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
			_account = new Account ("mopkobb88@gmail.com", new Dictionary<string, string> {
				{ "oauth_token", "1510660160-JKU5JWOMmdaODzcQZ0ljTrhjvZNRqeTH8Kz1B0r" },
				{ "oauth_token_secret", "ERE0F92Y7BOadyDP6L9esWDBMaCCEexZZrb4cUfMc" },
				{ "user_id", "1510660160" },
				{ "screen_name", "mopkobb88" },
				{ "oauth_consumer_key", "OxccMOQobPX3FfvzyTEA" },
				{ "oauth_consumer_secret", "h2eyjBcZFxcN2FOQGmWNNZ2MenIYzWRAJMCMHZBKs" }

			});
//			if (!IsAuthenticated)
//				return;
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

