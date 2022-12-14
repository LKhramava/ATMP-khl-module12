using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverNUnit.WebDriver
{
	public class BasePage
	{
		protected By TitleLocator;
		protected string title;

		protected BasePage(By TitleLocator, string title)
		{
			this.TitleLocator = TitleLocator;
			this.title = title;
			AssertIsOpen();
		}

		public void AssertIsOpen()
		{
			var label = new BaseElement(this.TitleLocator, this.title);
			label.WaitForIsVisible();
		}

	}
}
