using System;

namespace TestableCodeDemos.Module5.Shared
{
    public class InvoiceWriter : IInvoiceWriter
    {
        public void Print(Invoice invoice)
        {
            throw new NotImplementedException();
        }
    }
}
