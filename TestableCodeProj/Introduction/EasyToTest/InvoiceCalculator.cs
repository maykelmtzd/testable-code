using System;
namespace TestableCodeProj.Introduction.EasyToTest
{
    public class InvoiceCalculator
    {
        public decimal CalculateTotalPrice(decimal parts, decimal service, decimal discount)
        {
            return parts + service - discount;
        }   
    }
}
