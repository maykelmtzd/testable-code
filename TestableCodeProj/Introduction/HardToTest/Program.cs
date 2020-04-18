using System;

namespace TestableCodeProj.Introduction.HardToTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var partsPrice = decimal.Parse(args[0]);

            var servicePrice = decimal.Parse(args[1]);

            var discount = decimal.Parse(args[2]);

            var totalPrice = partsPrice + servicePrice - discount;

            Console.WriteLine("Hard Total price: $" + totalPrice);
        }
    }
}
