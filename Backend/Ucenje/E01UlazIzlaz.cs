using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucenje
{
    internal class E01UlazIzlaz
    {

        public static void Izvedi()
        {

            Console.Write("Pozdrav svijetu");
            Console.WriteLine("Isti red");
            Console.WriteLine("Osijek je \nSUPER");
            Console.WriteLine("\t\tOvo je uvučeno");
            Console.Write("Unesi svoje ime: ");

            //varijabla ime
            string ime=Console.ReadLine();

            Console.WriteLine("Unijeli ste {0}", ime);

     

        } // kraj metode

    } // kraj klase
} // kraj namespacea
