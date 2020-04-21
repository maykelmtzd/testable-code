using FluentAssertions;
using Moq;
using Moq.AutoMock;
using TestableCodeDemos.Module6.Shared;
using Xunit;

namespace TestableCodeDemos.Module6.Easy
{
    public class EmailInvoiceCommandTests
    {
        private EmailInvoiceCommand _command;
        private AutoMocker _mocker;
        private Invoice _invoice;

        private const int InvoiceId = 1;
        private const string EmailAddress = "email@test.com";

        public EmailInvoiceCommandTests()
        {
            _invoice = new Invoice()
            {
                EmailAddress = EmailAddress
            };

            _mocker = new AutoMocker();

            _mocker.GetMock<IDatabase>()
                .Setup(p => p.GetInvoice(InvoiceId))
                .Returns(_invoice);

            _command = _mocker.CreateInstance<EmailInvoiceCommand>();
        }

        [Fact]
        public void TestExecuteForInvoiceWithNoEmailAddressShouldThrowException()
        {
            _invoice.EmailAddress = string.Empty;

            _command.Invoking(c => c.Execute(InvoiceId))
                .Should().Throw<EmailAddressIsBlankException>();
        }

        [Fact]
        public void TestExecuteShouldEmailInvoice()
        {
            _command.Execute(InvoiceId);

            _mocker.GetMock<IInvoiceEmailer>()
                .Verify(p => p.Email(_invoice),
                    Times.Once);
        }
    }
}
