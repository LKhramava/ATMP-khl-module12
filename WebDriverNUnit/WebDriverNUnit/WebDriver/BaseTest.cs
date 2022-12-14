using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverNUnit.WebDriver
{
	public class BaseTest
	{
		protected static Browser Browser = Browser.Instance;

		[SetUp]
		public virtual void InitTest()
		{
			Browser = Browser.Instance;
			Browser.WindowMaximise();
			Browser.NavigateTo(Configuration.StartUrl);
		}

		[TearDown]
		public void TestClean()
		{
			Browser.Quit();
		}
	}
}
