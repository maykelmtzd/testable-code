﻿namespace TestableCodeDemos.Module4.Shared
{
    public interface IDatabase
    {
        Invoice GetInvoice(int invoiceId);

        void Save();
    }
}
