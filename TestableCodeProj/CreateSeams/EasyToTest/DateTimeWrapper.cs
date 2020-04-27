using System;

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
