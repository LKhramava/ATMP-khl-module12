using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using WebDriverNUnit.Entities;
using WebDriverNUnit.Pages;
using WebDriverNUnit.Utility;
using WebDriverNUnit.WebDriver;
using static WebDriverNUnit.Utility.TestDataHelper;

namespace WebDriverNUnit
{
	public class TestsForMailRu : BaseTest
	{
		[Test]
		[TestCaseSource(nameof(GetTestLoginData))]
		public void TestLogin(User user)
		{
			var homePage = new HomePage();
			homePage.GoToYourAccountPage(user);
		}

		private static List<User> GetTestLoginData
		{
			get
			{
				var users = new List<User>();

				using (var fs = File.OpenRead(@".\Resources\TestLoginData.csv"))
				using (var sr = new StreamReader(fs))
				{
					string line = string.Empty;
					while (line != null)
					{
						line = sr.ReadLine();
						if (line != null)
						{
							string[] split = line.Split(new char[] { ',' },
								StringSplitOptions.None);

							var user = new User() { Login = split[0], Password = split[1]};
							users.Add(user);
						}
					}
				}

				return users;
			}
		}

		[Test]
		[TestCaseSource(nameof(GetTestLoginData))]
		public void TestLoginWithActions(User user)
		{
			var homePage = new HomePage();
			homePage.GoToYourAccountPageWithActions(user);
		}

		[Test]
		[TestCaseSource(nameof(GetTestLoginData))]
		public void DeleteFirstDraftEmailWithActions(User user)
		{
			var homePage = new HomePage();
			var accountPage = homePage.GoToYourAccountPageWithActions(user);

			var deleteFirstDraftEmailResult = accountPage.DelteFirstDraftEmail();
			Assert.IsTrue(deleteFirstDraftEmailResult);

		}

		[Test]
		[TestCase("lizakhramova", "070461040485", "lizakhramova@mail.ru", "TestAT", "Hello! My name is Liza! How are you? See you later. Bye.")]
		public void SaveDraftEmail(string login, string password, string letterEmail, string letterSubject, string letterBody)
		{
			var homePage = new HomePage();
			var user = new User() { Login = login, Password = password };

			var accountPage = homePage.GoToYourAccountPage(user);

			letterSubject = letterSubject + DateTime.Now.TimeOfDay.Ticks.ToString();

			var letter = new Letter() { Email = letterEmail, Subject = letterSubject, Body = letterBody };
			accountPage.SaveDraftEmail(letter);
			var letterInDraft = accountPage.VerifySavedDraftEmail(letter);
			Assert.NotNull(letterInDraft);
		}

		[Test]
		public void SaveDraftEmailWithXmlTestData()
		{
			var testData = TestDataHelper.ReadTestData(TestDataType.Xml);

			var homePage = new HomePage();

			var accountPage = homePage.GoToYourAccountPage(testData.User);

			accountPage.SaveDraftEmail(testData.Letter);
			var letterInDraft = accountPage.VerifySavedDraftEmail(testData.Letter);
			Assert.NotNull(letterInDraft);
		}

		[Test]
		[TestCase("lizakhramova", "070461040485", "lizakhramova@mail.ru", "TestAT", "Hello! My name is Liza! How are you? See you later. Bye.")]
		public void SendDraftEmail(string login, string password, string letterEmail, string letterSubject, string letterBody)
		{
			var homePage = new HomePage();
			var user = new User() { Login = login, Password = password };

			var accountPage = homePage.GoToYourAccountPage(user);

			letterSubject = letterSubject + DateTime.Now.TimeOfDay.Ticks.ToString();

			var letter = new Letter() { Email = letterEmail, Subject = letterSubject, Body = letterBody };
			accountPage.SaveDraftEmail(letter);

			var sendDraftEmailResult = false;

			if (!accountPage.VerifySavedDraftEmail(letter))
				sendDraftEmailResult = accountPage.SendDraftEmail(letter);

			accountPage.Logout();

			Assert.IsTrue(sendDraftEmailResult);
		}
	}
}