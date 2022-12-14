using OpenQA.Selenium;
using System;

namespace WebDriverNUnit.WebDriver
{
	public class Browser
	{
		private static Browser _currentInstance;
		private static IWebDriver _driver;
		public static BrowserFactory.BrowserType currentBrowser;
		public static int ImplWait;
		public static double TimeoutForElement;
		public static string _browser;

		private Browser()
		{
			InitBrowser();
			_driver = BrowserFactory.GetDriver(currentBrowser, ImplWait);
		}

		private static void InitBrowser()
		{
			ImplWait = Convert.ToInt32(Configuration.TimeoutForElement);
			TimeoutForElement = Convert.ToInt32(Configuration.TimeoutForElement);
			_browser = Configuration.Browser;
			Enum.TryParse(_browser, out currentBrowser);
		}

		public static Browser Instance
		{
			get
			{
				if (_currentInstance == null) 
					_currentInstance = new Browser();
				
				return _currentInstance;
			}
		}

		public static void WindowMaximise()
		{
			_driver.Manage().Window.Maximize();
		}

		public static void NavigateTo(string url)
		{
			_driver.Navigate().GoToUrl(url);
		}

		public static IWebDriver GetDriver()
		{
			return _driver;
		}

		public static void Quit()
		{
			_driver.Close();
			_driver.Quit();
			_currentInstance = null;
			_driver = null;
			_browser = null;
		}
	}
}
