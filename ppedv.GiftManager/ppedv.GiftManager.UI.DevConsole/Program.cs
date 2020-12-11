using ppedv.GiftManager.Logic;
using ppedv.GiftManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.GiftManager.UI.DevConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** GiftManager ***");

            var core = new Core();

            foreach (var prod in core.Repository.GetAll<Produkt>())
            {
                Console.WriteLine($"{prod.Bezeichnung}");
            }


            Console.WriteLine("Ende");
            Console.ReadKey();
        }
    }
}
