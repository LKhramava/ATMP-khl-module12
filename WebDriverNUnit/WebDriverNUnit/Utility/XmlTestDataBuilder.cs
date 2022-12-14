using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebDriverNUnit.Entities;
using WebDriverNUnit.WebDriver;

namespace WebDriverNUnit.Utility
{
	internal class XmlTestDataBuilder : ITestDataBuilder
	{
        private TestData _testData;

        public XmlTestDataBuilder()
        {
            _testData = new XmlTestData();
        }

        public void BuildUser()
        {
            XmlSerializer reader = new XmlSerializer(typeof(User));
            StreamReader file = new StreamReader(Configuration.UserData);
            _testData.User = (User)reader.Deserialize(file);
            file.Close();
        }

        public void BuildLetter()
        {
            XmlSerializer reader = new XmlSerializer(typeof(Letter));
            StreamReader file = new StreamReader(Configuration.LetterData);
            _testData.Letter = (Letter)reader.Deserialize(file);
            _testData.Letter.Subject += DateTime.Now.TimeOfDay.Ticks.ToString();
            file.Close();
        }

        public TestData TestData
        {
            get { return _testData; }
        }
    }
}
