using System;
using System.Collections.Generic;
using System.Text;
using Ninject;
using Ninject.Extensions.Conventions;

namespace TestableCodeDemos.Module6.Easy
{
	public class Program
	{
        static void Main_SingleResponsability(string[] args)
        {
            var invoiceId = int.Parse(args[0]);
            var commandName = args[1];

            var container = new StandardKernel();

            container.Bind(p =>
            {
                p.FromThisAssembly()
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            var router = new Dictionary<string, Type>()
            {
                {"email",  typeof(EmailInvoiceCommand)},
                {"print",  typeof(PrintInvoiceCommand)}
            };

            //Missing input validation, the router might not have a value for the given key.
            Type commandToExecute = router[commandName];

            var command = (ICommand)container.Get(commandToExecute);

            command.Execute(invoiceId);
        }
    }
}
