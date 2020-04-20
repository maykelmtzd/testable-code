using System;
using System.Collections.Generic;
using System.Text;

namespace TestableCodeProj.CreateSeams.EasyToTest
{
	public class DateTimeWrapper : IDateTimeWrapper
	{
		public DateTime GetNow()
		{
			return DateTime.Now;
		}
	}
}
