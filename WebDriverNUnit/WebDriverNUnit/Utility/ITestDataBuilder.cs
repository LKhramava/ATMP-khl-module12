using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverNUnit.Utility
{
	public interface ITestDataBuilder
	{
        void BuildUser();
        void BuildLetter();

        TestData TestData { get; }
	}
}
