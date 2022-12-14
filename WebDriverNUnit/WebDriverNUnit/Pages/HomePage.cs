using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverNUnit.Entities;
using WebDriverNUnit.WebDriver;

namespace WebDriverNUnit.Pages
{
	public class HomePage : BasePage
	{
		private static readonly By HomeLbl = By.Id("grid");

		public HomePage() : base(HomeLbl, "Home Page")
		{

		}

		private readonly BaseElement loginBE = new BaseElement(By.ClassName("ph-login"));
		private readonly BaseElement loginFrameBE = new BaseElement(By.ClassName("ag-popup__frame__layout__iframe"));

		private readonly BaseElement userNameBE = new BaseElement(By.Name("username"));
		private readonly BaseElement inputLoginSubmitBE = new BaseElement(By.CssSelector(".login-row button[type='submit']"));
		private readonly BaseElement inputPasswordBE = new BaseElement(By.Name("password"));

		private readonly BaseElement dataClickCounterBE = new BaseElement(By.XPath("//a[@data-show-pixel]//div[@data-click-counter]"));

		public YourAccountPage GoToYourAccountPage(User user)
		{
			return Login(user);
		}

		public YourAccountPage GoToYourAccountPageWithActions(User user)
		{
			return LoginWithActions(user);
		}

		public void AssertIsVisible(By locator)
		{
			var label = new BaseElement(locator);
			label.WaitForIsVisible();
		}

		private YourAccountPage Login(User user)
		{
			loginBE.Click();
			Browser.GetDriver().SwitchTo().Frame(this.loginFrameBE.GetElement());

			userNameBE.SendKeys(user.Login);
			inputLoginSubmitBE.Click();

			inputPasswordBE.SendKeys(user.Password);
			inputLoginSubmitBE.Click();

			dataClickCounterBE.Click();

			Browser.GetDriver().SwitchTo().DefaultContent();

			return new YourAccountPage();
		}

		private YourAccountPage LoginWithActions(User user)
		{
			//login with actions (use click and keydown)

			//click to login button
			loginBE.ClickWithActions();

			Browser.GetDriver().SwitchTo().Frame(this.loginFrameBE.GetElement());

			//input login
			userNameBE.SendKeysWithActions(user.Login);
			inputLoginSubmitBE.PressEnter();

			//input password
			inputPasswordBE.SendKeysWithActions(user.Password);
			inputLoginSubmitBE.PressEnter();

			dataClickCounterBE.ClickWithActions();

			Browser.GetDriver().SwitchTo().DefaultContent();

			return new YourAccountPage();
		}
	}
}
