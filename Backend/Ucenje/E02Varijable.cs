using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucenje
{
    internal class E02Varijable
    {

        public static void Izvedi()
        {
            Console.Write("Unesi svoje ime:");

            string ime;

            ime = Console.ReadLine();

            Console.WriteLine(ime);

            Console.WriteLine(ime[0]);

            char znak = '@';

            Console.WriteLine(znak);

            Console.WriteLine((int)znak);

            int cijelibroj = int.MaxValue;

            Console.WriteLine(int.MaxValue);
            Console.WriteLine(cijelibroj+1);

            Console.Write("Unesi broj cipela: ");
            int brojcipela = int.Parse(Console.ReadLine());
            Console.WriteLine(brojcipela+1);

            bool logickitip = true;

            float decimalnibroj = 3.14f;

            double velikidecimalnibroj = 3.14;

            decimal db = 3.14m;


        }


    }
}
