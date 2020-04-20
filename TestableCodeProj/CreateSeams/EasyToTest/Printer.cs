using System;
using System.Collections.Generic;
using System.Text;

namespace TestableCodeProj.CreateSeams.EasyToTest
{
	public class Printer : IPrinter
	{
		public void WriteLine(string text)
		{
			throw new NotImplementedException();
		}
	}
}
