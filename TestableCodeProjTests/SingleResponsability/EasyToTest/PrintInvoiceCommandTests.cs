﻿using FluentAssertions;
using Moq;
using Moq.AutoMock;
using TestableCodeDemos.Module6.Shared;
using Xunit;

namespace TestableCodeDemos.Module6.Easy
{
    public class PrintInvoiceCommandTests
    {
        private PrintInvoiceCommand _command;
        private AutoMocker _mocker;
        private Invoice _invoice;

        private const int InvoiceId = 1;
        private const string UserName = "mrenze";

        public PrintInvoiceCommandTests()
        {
            _invoice = new Invoice();

            _mocker = new AutoMocker();

            _mocker.GetMock<IDatabase>().
                Setup(p => p.GetInvoice(InvoiceId))
                .Returns(_invoice);

            _mocker.GetMock<ISecurity>()
                .Setup(p => p.IsAdmin())
                .Returns(true);

            _mocker.GetMock<ISecurity>()
                .Setup(p => p.GetUserName())
                .Returns(UserName);

            _command = _mocker.CreateInstance<PrintInvoiceCommand>();
        }

        [Fact]
        public void TestExecuteShouldThrowExceptionIfUserIsNotAdmin()
        {
            _mocker.GetMock<ISecurity>()
                .Setup(p => p.IsAdmin())
                .Returns(false);

            _command.Invoking(c => c.Execute(InvoiceId))
                .Should().Throw<UserNotAuthorizedException>();
        }

        [Fact]
        public void TestExecuteShouldPrintInvoice()
        {
            _command.Execute(InvoiceId);

            _mocker.GetMock<IInvoiceWriter>()
                .Verify(p => p.Print(_invoice),
                    Times.Once);
        }

        [Fact]
        public void TestExecuteShouldSetLastPrintedByToCurrentUser()
        {
            _command.Execute(InvoiceId);

            _invoice.LastPrintedBy.Should().Be("mrenze");
        }

        [Fact]
        public void TestExecuteShouldSaveChangesToDatabase()
        {
            _command.Execute(InvoiceId);

            _mocker.GetMock<IDatabase>()
                .Verify(p => p.Save(),
                    Times.Once);
        }
    }
}
