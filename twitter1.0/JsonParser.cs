using System;
using Newtonsoft;
using Newtonsoft.Json;
using System.Collections.Generic;
using MonoTouch.Foundation;

namespace Twitter
{
	public class JsonParser
	{
		public JsonParser ()
		{
		}



		public List<Twit> ParseString(string json)
		{
			if (String.IsNullOrEmpty (json))
				return new List<Twit> ();
			var _tweets = new List<Twit>();
			Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(json);
			foreach (var current in jObject["statuses"])
			{
				var tweet = new Twit();
				tweet.ImagePath = current["user"]["profile_image_url"].ToString();
				tweet.Info = current["text"].ToString ();
				tweet.PostDate = current["created_at"].ToString();
				tweet.SmallInfo = current["user"]["description"].ToString ();
				tweet.Name = current["user"]["name"].ToString ();
				_tweets.Add(tweet);
			}
			return _tweets;
		}
	}
}

