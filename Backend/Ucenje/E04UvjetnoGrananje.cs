using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucenje
{
    internal class E04UvjetnoGrananje
    {

        public static void Izvedi()
        {

            Console.WriteLine("E04UvjetnoGrananje");

            int broj = 7;

            bool uvjet = broj == 7;

            if (uvjet)
            {
                Console.WriteLine("Super!");
            }

            if (broj > 2)
            {
                Console.WriteLine("OK");
            }
            else if (broj == 1)
            {
                Console.WriteLine("Nije dobro");
            }
            else
            {
                Console.WriteLine("Nije ocjena");
            }
        }
    }
}
