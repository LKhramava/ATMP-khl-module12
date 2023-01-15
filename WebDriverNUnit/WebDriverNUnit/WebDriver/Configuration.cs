using System.Configuration;

namespace WebDriverNUnit.WebDriver
{
	public class Configuration
	{
		public static string GetEnvironmentVal(string val, string defaultVal)
		{
			return ConfigurationManager.AppSettings[val] ?? defaultVal;
		}

		public static string TimeoutForElement = GetEnvironmentVal("TimeoutForElement", "30");
		public static string Browser = GetEnvironmentVal("Browser", "chrome");
		public static string StartUrl = GetEnvironmentVal("StartUrl", "https://www.mail.ru/");
		public static string UserData = GetEnvironmentVal("UserData", @".\Resources\UserData.xml");
		public static string LetterData = GetEnvironmentVal("LetterData", @".\Resources\LetterData.xml");
		public static string JsonPlaceholderUrl = GetEnvironmentVal("JsonPlaceholderUrl", "https://jsonplaceholder.typicode.com/users");
	}
}
