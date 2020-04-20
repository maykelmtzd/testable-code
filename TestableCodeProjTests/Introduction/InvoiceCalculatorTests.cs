using System;
using FluentAssertions;
using TestableCodeProj.Introduction.EasyToTest;
using Xunit;

namespace TestableCodeProjTests.Introduction
{
    public class InvoiceCalculatorTests
    {
        private InvoiceCalculator _invoiceCalculator;

        public InvoiceCalculatorTests()
        {
            _invoiceCalculator = new InvoiceCalculator();
        }

        [Fact]
        public void Should_return_correct_price_by_adding_parts_and_services_and_substracting_discount()
        {
            var totalPrice = _invoiceCalculator.CalculateTotalPrice(3.00m, 2.00m, 1.00m);

            totalPrice.Should().Be(4.00m); 
        }
    }
}
