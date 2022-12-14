using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using WebDriverNUnit.Entities;
using WebDriverNUnit.Pages;
using WebDriverNUnit.WebDriver;


[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(2)]
namespace WebDriverNUnit.SpecFlow.Steps
{
	[Binding]
	public class SpecFlowMailRuTestsStepDefinitions
	{
		//private HomePage homePage;
		//private YourAccountPage yourAccountPage;

		private readonly ScenarioContext _scenarioContext;

		public SpecFlowMailRuTestsStepDefinitions(ScenarioContext scenarioContext)
		{
			_scenarioContext = scenarioContext;
		}

		[Given(@"I open mail\.ru page")]
		public void IOpenMail_RuPage()
		{
			var homePage = new HomePage();
			_scenarioContext.Set(homePage, "HomePage");
		}

		[When(@"I login with '([^']*)' and '([^']*)' to mail\.ru page")]
		public void ILoginWithAndToMail_RuPage(string login, string password)
		{
			var user = new User() { Login = login, Password = password };
			var homePage = _scenarioContext.Get<HomePage>("HomePage");
			var yourAccountPage = homePage.GoToYourAccountPage(user);
			_scenarioContext.Set(yourAccountPage, "YourAccountPage");
		}

		[Then(@"My account '([^']*)' page is opened")]
		public void MyAccountPageIsOpened(string login)
		{
			var yourAccountPage = _scenarioContext.Get<YourAccountPage>("YourAccountPage");
			Assert.IsTrue(yourAccountPage.IsLoginDisplayed(string.Format("{0}@mail.ru", login)));
		}
		
		[When(@"I save email with address '([^']*@mail.ru)', subject '([^']*)' and body '([^']*)' in draft")]
		public void WhenISaveEmailWithAddressSubjectAndBodyInDraft(string letterEmail, string letterSubject, string letterBody)
		{
			var currentLetterSubject = letterSubject + DateTime.Now.TimeOfDay.Ticks.ToString();
			_scenarioContext.Set(currentLetterSubject, "CurrentLetterSubject");
			_scenarioContext.Set(letterEmail, "CurrentLetterEmail");
			_scenarioContext.Set(letterBody, "CurrentLetterBody");

			var letter = new Letter() { Email = letterEmail, Subject = currentLetterSubject, Body = letterBody };
			var yourAccountPage = _scenarioContext.Get<YourAccountPage>("YourAccountPage");
			yourAccountPage.SaveDraftEmail(letter);
		}

		[Then(@"I check the saved email in draft")]
		public void ThenICheckTheSavedEmailInDraft()
		{
			var sendDraftEmailResult = false;

			var letter = new Letter() { Email = _scenarioContext.Get<string>("CurrentLetterEmail"), Subject = _scenarioContext.Get<string>("CurrentLetterSubject"), Body = _scenarioContext.Get<string>("CurrentLetterBody") };

			var yourAccountPage = _scenarioContext.Get<YourAccountPage>("YourAccountPage");
			if (yourAccountPage.VerifySavedDraftEmail(letter))
				sendDraftEmailResult = yourAccountPage.SendDraftEmail(letter);

			yourAccountPage.Logout();

			Assert.IsTrue(sendDraftEmailResult);
		}
	}
}
