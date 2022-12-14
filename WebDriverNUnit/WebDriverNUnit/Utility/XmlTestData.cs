using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverNUnit.Entities;

namespace WebDriverNUnit.Utility
{
	public class XmlTestData : TestData
	{
		private User _user;
		private Letter _letter;

		public User User
		{
			get
			{
				return _user;
			}
			set
			{
				_user = value;
			}
		}

		public Letter Letter
		{
			get
			{
				return _letter;
			}
			set
			{
				_letter = value;
			}
		}
	}
}
