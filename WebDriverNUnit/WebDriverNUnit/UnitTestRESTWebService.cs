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
	internal class UnitTestRESTWebService : BaseTestRESTWebService
	{
		[Test]
		public void TestCheckGetUsersStatusCode()
		{
			var expectedStatusCode = HttpStatusCode.OK;
			Assert.AreEqual(expectedStatusCode, Response.StatusCode);
		}

		[Test]
		public void TestCheckGetUsersContentTypeHeader()
		{
			var expectedContentType = "application/json; charset=utf-8";
			var contentType = Response.ContentType;

			Assert.AreEqual(expectedContentType, contentType);
		}

		[Test]
		public void TestCheckGetUsersCount()
		{
			var expectedUsersCount = 10;
			var responseBody = GetReponseBody(Response);

			List<UserInfo> users = JsonConvert.DeserializeObject<List<UserInfo>>(responseBody);

			Assert.AreEqual(expectedUsersCount, users.Count);
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
