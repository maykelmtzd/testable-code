﻿using System;
using Moq;
using TestableCodeProj.CreateSeams.EasyToTest;
using TestableCodeProj.CreateSeams.Shared;
using Xunit;

namespace TestableCodeProjTests.CreateSeams.EasyToTest
{
    public class PrintInvoiceCommandTests
	{
        private PrintInvoiceCommand _command;
        private Mock<IDatabase> _mockDatabase;
        private Mock<IPrinter> _mockPrinter;
        private Mock<IDateTimeWrapper> _mockDateTime;
        private Invoice _invoice;

        private const int InvoiceId = 1;
        private const decimal Total = 1.23m;
        private static readonly DateTime Date = new DateTime(2001, 2, 3);

        public PrintInvoiceCommandTests()
        {
            _invoice = new Invoice()
            {
                Id = InvoiceId,
                Total = Total
            };

            _mockDatabase = new Mock<IDatabase>();
            _mockPrinter = new Mock<IPrinter>();
            _mockDateTime = new Mock<IDateTimeWrapper>();

            _mockDatabase
                .Setup(p => p.GetInvoice(InvoiceId))
                .Returns(_invoice);

            _mockDateTime
                .Setup(p => p.GetNow())
                .Returns(Date);

            _command = new PrintInvoiceCommand(
                _mockDatabase.Object,
                _mockPrinter.Object,
                _mockDateTime.Object);
        }

        [Fact]
        public void TestExecuteShouldPrintInvoiceNumber()
        {
            _command.Execute(InvoiceId);

            _mockPrinter
                .Verify(p => p.WriteLine("Invoice ID: 1"),
                    Times.Once);
        }

        [Fact]
        public void TestExecuteShouldPrintTotalPrice()
        {
            _command.Execute(InvoiceId);

            _mockPrinter
                .Verify(p => p.WriteLine("Total: $1.23"),
                    Times.Once);
        }

        [Fact]
        public void TestExecuteShouldPrintTodaysDate()
        {
            _command.Execute(InvoiceId);

            _mockPrinter
                .Verify(p => p.WriteLine("Printed: 2/3/2001"),
                    Times.Once);
        }
    }
}
