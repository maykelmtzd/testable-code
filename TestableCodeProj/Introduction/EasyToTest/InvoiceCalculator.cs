using System;
namespace TestableCodeProj.Introduction.EasyToTest
{
    public class InvoiceCalculator
    {
        public decimal GetTotal(decimal parts, decimal service, decimal discount)
        {
            return parts + service - discount;
        }   
    }
}
