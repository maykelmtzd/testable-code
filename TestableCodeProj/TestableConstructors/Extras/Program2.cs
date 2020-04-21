using System;
using System.Collections.Generic;
using System.Linq;

namespace TestableCodeDemos.Module3.Extras
{
    public class Program2
    {
        static void MainMain_TestableConstructors_2(string[] args)
        {
            var invoiceId = int.Parse(args[0]);

            var factory = new PrintInvoiceCommandFactory();

            var command = factory.Create();

            command.Execute(invoiceId);
        }
    }
}
