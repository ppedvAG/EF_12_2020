using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EfCoreFromDb
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            using var context = new NORTHWNDContext();

            //foreach (var emps in context.Employees.Include(x => x.Orders).ToList())
            foreach (var emps in context.Employees.ToList())
            {
                Console.WriteLine($"{emps.LastName}");

                //context.Entry(emps).Collection(x => x.Orders).Load(); //expl.
                foreach (var o in emps.Orders)
                {

                    Console.WriteLine($"\t{o.OrderDate:d}");
                }

            }


            Console.WriteLine("Ende");
            Console.ReadLine();

        }
    }
}
