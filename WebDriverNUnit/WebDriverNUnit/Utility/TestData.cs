using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverNUnit.Entities;

namespace WebDriverNUnit.Utility
{
	public abstract class TestData
	{
		public virtual User User { get; set; }
		public virtual Letter Letter { get; set; }
	}
}
