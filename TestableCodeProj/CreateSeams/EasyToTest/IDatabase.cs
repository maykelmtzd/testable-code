using System;
using System.Collections.Generic;
using System.Text;
using TestableCodeProj.CreateSeams.Shared;

namespace TestableCodeProj.CreateSeams.EasyToTest
{
	public interface IDatabase
	{
		Invoice GetInvoice(int invoiceId);
	}
}
