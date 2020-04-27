using System;
using System.Collections.Generic;
using System.Text;
using Ninject;
using Ninject.Extensions.Conventions;

namespace TestableCodeDemos.Module6.Hard
{
	public class Program
	{
        static void Main_SingleResponsability(string[] args)
        {
            var invoiceId = int.Parse(args[0]);
            var shouldEmail = bool.Parse(args[1]);

            var container = new StandardKernel();

            container.Bind(p =>
            {
                p.FromThisAssembly()
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            var command = container.Get<PrintOrEmailInvoiceCommand>();

            command.Execute(invoiceId, shouldEmail);
        }
    }
}
