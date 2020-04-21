using FluentAssertions;
using Moq;
using Moq.AutoMock;
using TestableCodeDemos.Module6.Shared;
using Xunit;

namespace TestableCodeDemos.Module6.Hard
{
    public class PrintOrEmailInvoiceCommandTests
    {
        private PrintOrEmailInvoiceCommand _command;
        private AutoMocker _mocker;
        private Invoice _invoice;

        private const int InvoiceId = 1;
        private const string EmailAddress = "email@test.com";
        private const string UserName = "mrenze";

        public PrintOrEmailInvoiceCommandTests()
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

            _command = _mocker.CreateInstance<PrintOrEmailInvoiceCommand>();
        }

        [Fact]
        public void TestExecuteForEmailingInvoiceWithNoEmailAddressShouldThrowException()
        {
            _invoice.EmailAddress = string.Empty;

            _command.Invoking(c => c.Execute(InvoiceId, true))
                .Should().Throw<EmailAddressIsBlankException>();
        }

        [Fact]
        public void TestExecuteShouldEmailInvoiceIfEmailing()
        {
            _invoice.EmailAddress = EmailAddress;

            _command.Execute(InvoiceId, true);

            _mocker.GetMock<IInvoiceEmailer>()
                .Verify(p => p.Email(_invoice),
                    Times.Once);
        }

        [Fact]
        public void TestExecuteShouldThrowExceptionIfPrintingAndUserIsNotAdmin()
        {
            _mocker.GetMock<ISecurity>()
                .Setup(p => p.IsAdmin())
                .Returns(false);

            _command.Invoking(c => c.Execute(InvoiceId, false))
                .Should().Throw<UserNotAuthorizedException>();
        }

        [Fact]
        public void TestExecuteShouldPrintInvoiceIfPrinting()
        {
            _command.Execute(InvoiceId, false);

            _mocker.GetMock<IInvoiceWriter>()
                .Verify(p => p.Print(_invoice),
                    Times.Once);
        }

        [Fact]
        public void TestExecuteShouldSetLastPrintedByToCurrentUserIfPrinting()
        {
            _command.Execute(InvoiceId, false);

            _invoice.LastPrintedBy.Should().Be("mrenze");
        }

        [Fact]
        public void TestExecuteShouldSaveChangesToDatabaseIfPrinting()
        {
            _command.Execute(InvoiceId, false);

            _mocker.GetMock<IDatabase>()
                .Verify(p => p.Save(),
                    Times.Once);
        }
    }
}
