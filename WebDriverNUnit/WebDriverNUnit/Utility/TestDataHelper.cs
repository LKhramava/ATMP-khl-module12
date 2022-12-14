using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebDriverNUnit.Entities;

namespace WebDriverNUnit.Utility
{
	public static class TestDataHelper
	{
		public static TestData ReadTestData(TestDataType type)
		{
			Director director = new Director();
			ITestDataBuilder builder;

			switch (type)
			{
				default:
				case TestDataType.Xml:
					{
						builder = new XmlTestDataBuilder();
						break;
					}
			}

			director.Build(builder);
			return builder.TestData;
		}
	}
}
