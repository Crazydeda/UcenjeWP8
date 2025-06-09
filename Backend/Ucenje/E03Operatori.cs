using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucenje
{
    internal class E03Operatori
    {

        public static void Izvedi()
        {

            Console.WriteLine("E03Operatori");

            string grad = "Osijek";
            string ime = "Edunova";

            Console.WriteLine(grad + " " + ime + " " + 5);
            Console.WriteLine("{0} {1} {2}", grad, ime, 5);

            Console.WriteLine(8%2);
            int b = 9 % 2;
            Console.WriteLine(b);

            int i = 0;
            i = i + 1;
            i += 1;
            i++;
            ++i;

            int k = 1, j = 0;
            k = ++k - j++;
            Console.WriteLine(--k+j++);


        }

    }
}
