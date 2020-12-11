using ppedv.GiftManager.Logic;
using ppedv.GiftManager.Model;
using ppedv.GiftManager.Model.Fault;
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
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("*** GiftManager ***");

            var core = new Core();
            var alleProds = core.Repository.GetAll<Produkt>();

            try
            {
                foreach (var prod in alleProds)
                {
                    Console.WriteLine($"{prod.Bezeichnung} {prod.Preis:c}");
                    prod.Preis += 1;
                }


                core.Repository.SaveAll();
                Console.WriteLine("Speichern erfolgreich");
            }
            catch (ConcurrencyException ex)
            {
                Console.WriteLine($"Jemand hat die Daten in der Zwischenzeit geänder. " +
                    $"Welche daten sollen nun gespeichert werden\n\n[m]: Meine\n[s]: Server");
                if (Console.ReadKey().Key == ConsoleKey.M)
                    ex.UserWins.Invoke();
                else
                    ex.DbWins.Invoke();

                Console.WriteLine("Preise nun:");
                foreach (var prod in core.Repository.GetAll<Produkt>())
                {
                    Console.WriteLine($"{prod.Bezeichnung} {prod.Preis:c}");
                    prod.Preis += 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FEHLER: {ex.Message}");
            }

            Console.WriteLine("Ende");
            Console.ReadKey();
        }
    }
}
