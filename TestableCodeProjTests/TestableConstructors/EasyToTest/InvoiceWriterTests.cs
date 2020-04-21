using Moq;
using Moq.AutoMock;
using TestableCodeDemos.Module3.Shared;
using Xunit;

namespace TestableCodeDemos.Module3.Easy
{
    public class InvoiceWriterTests
    {
        private InvoiceWriter _writer;
        private AutoMocker _mocker;
        private Invoice _invoice;

        public InvoiceWriterTests()
        {
            _invoice = new Invoice()
            {
                Id = 1,
                IsOverdue = false
            };

            _mocker = new AutoMocker();

            _writer = _mocker.CreateInstance<InvoiceWriter>();
        }

        [Fact]
        public void TestWriteShouldSetPageLayout()
        {
            _writer.Write(_invoice);

            var layout = _mocker
                .GetMock<IPageLayout>().Object;

            _mocker.GetMock<IPrinter>()
                .Verify(p => p.SetPageLayout(layout),
                    Times.Once);
        }

        [Fact]
        public void TestWriteShouldPrintOverdueInvoiceInRed()
        {
            _invoice.IsOverdue = true;

            _writer.Write(_invoice);

            _mocker.GetMock<IPrinter>()
                .Verify(p => p.SetInkColor("Red"),
                    Times.Once);
        }

        [Fact]
        public void TestWriteShouldPrintOnTimeInvoiceInDefaultColor()
        {
            _writer.Write(_invoice);

            _mocker.GetMock<IPrinter>()
                .Verify(p => p.SetInkColor(It.IsAny<string>()),
                    Times.Never);
        }

        [Theory]
        [InlineData("Invoice ID: 1")]
        // Remaining test cases would go here
        public void TestWriteShouldPrintInvoiceNumber(string line)
        {
            _writer.Write(_invoice);

            _mocker.GetMock<IPrinter>()
                .Verify(p => p.WriteLine(line), 
                    Times.Once());
        }        
    }
}
