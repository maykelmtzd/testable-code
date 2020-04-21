using FluentAssertions;
using Moq;
using Moq.AutoMock;
using TestableCodeDemos.Module4.Shared;
using Xunit;

namespace TestableCodeDemos.Module4.Easy
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

            _mocker.GetMock<IDatabase>()
                .Setup(p => p.GetInvoice(InvoiceId))
                .Returns(_invoice);

            _mocker.GetMock<IIdentityService>()
                .Setup(p => p.GetUserName())
                .Returns(UserName);

            _command = _mocker.CreateInstance<PrintInvoiceCommand>();
        }

        [Fact]
        public void TestExecuteShouldPrintInvoice()
        {
            _command.Execute(InvoiceId);

            _mocker.GetMock<IInvoiceWriter>()
                .Verify(p => p.Write(_invoice),
                    Times.Once);
        }

        [Fact]
        public void TestExecuteShouldLastPrintedByToCurrentUserName()
        {
            _command.Execute(InvoiceId);

            _invoice.LastPrintedBy.Should().Equals(UserName);
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
