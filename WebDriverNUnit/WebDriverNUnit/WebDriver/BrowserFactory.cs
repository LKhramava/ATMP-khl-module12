using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;

namespace WebDriverNUnit.WebDriver
{
	public class BrowserFactory
	{
		public enum BrowserType
		{
			Chrome,
			Firefox,
			remoteFirefox,
			remoteChrome
		}

		public static IWebDriver GetDriver(BrowserType type, int timeOutSec)
		{
			IWebDriver? driver;
			switch (type)
			{
				default:
				case BrowserType.Chrome:
					{
						var service = ChromeDriverService.CreateDefaultService();
						ChromeOptions options = new ChromeOptions();
						options.AddArgument("disable-infobars");
						driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(timeOutSec));
						break;
					}
				case BrowserType.Firefox:
					{
						var service = FirefoxDriverService.CreateDefaultService();
						FirefoxOptions options = new FirefoxOptions();
						driver = new FirefoxDriver(service, options, TimeSpan.FromSeconds(timeOutSec));
						break;
					}
				case BrowserType.remoteFirefox:
					{
						FirefoxOptions firefoxOptions = new FirefoxOptions();
						driver = new RemoteWebDriver(new Uri("http://172.20.10.2:4444/wd/hub"), firefoxOptions);
						break;
					}
				case BrowserType.remoteChrome:
					{
						var option = new ChromeOptions();
						option.AddArgument("disable-infobars");
						option.AddArgument("--no-sandbox");
						driver = new RemoteWebDriver(new Uri("http://172.20.10.2:4444/wd/hub"), option.ToCapabilities());
						break;
					}
			}
			return driver;
		}
	}
}
