using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverNUnit.Utility
{
    class Director
    {
        public void Build(ITestDataBuilder Builder)
        {
            Builder.BuildUser();
            Builder.BuildLetter();
        }
    }
}
