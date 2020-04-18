using System;
namespace TestableCodeProj.Introduction.EasyToTest
{
    public class Program
    {
        static void Main_use_InvoiceCalculator(string[] args)
        {
            var partsPrice = decimal.Parse(args[0]);

            var servicePrice = decimal.Parse(args[1]);

            var discount = decimal.Parse(args[2]);

            var calculator = new InvoiceCalculator();

            var totalPrice = calculator.GetTotal(partsPrice, servicePrice, discount);

            Console.WriteLine("Easy Total price: $" + totalPrice);
        }
    }
}
