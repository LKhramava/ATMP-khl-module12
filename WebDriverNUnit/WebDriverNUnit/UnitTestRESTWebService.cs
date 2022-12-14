using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using WebDriverNUnit.Entities;
using WebDriverNUnit.WebDriver;

namespace WebDriverNUnit
{
	internal class UnitTestRESTWebService
	{
		[Test]
		public void TestCheckGetUsersStatusCode()
		{
			var responce = MakeRequest();
			Assert.IsTrue(responce.StatusCode == HttpStatusCode.OK);
		}

		[Test]
		public void TestCheckGetUsersContentTypeHeader()
		{
			var response = MakeRequest();
			var contentType = response.ContentType;

			Assert.IsTrue(contentType == "application/json; charset=utf-8");
		}

		[Test]
		public void TestCheckGetUsersCount()
		{
			var response = MakeRequest();
			var responseBody = GetReponseBody(response);

			List<UserInfo> users = JsonConvert.DeserializeObject<List<UserInfo>>(responseBody);

			Assert.IsTrue(users.Count == 10);
		}

		public static HttpWebResponse MakeRequest()
		{
			string responceBody = String.Empty;
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Configuration.JsonPlaceholderUrl);
			request.Method = "Get";

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			return response;
		}

		private static string GetReponseBody(HttpWebResponse response)
		{
			string responseBody = String.Empty;
			using (Stream s = response.GetResponseStream())
			{
				using (StreamReader r = new StreamReader(s))
				{
					responseBody = r.ReadToEnd();
				}
			}
			return responseBody;
		}
	}
}
