using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ucenje
{
    internal class E07ForPetlja
    {

        public static void Izvedi()
        {

            Console.WriteLine("E07ForPetlja");

            Console.WriteLine("Hrvatska - Češka 5:1");
            Console.WriteLine("Hrvatska - Češka 5:1");
            Console.WriteLine("Hrvatska - Češka 5:1");
            Console.WriteLine("Hrvatska - Češka 5:1");
            Console.WriteLine("Hrvatska - Češka 5:1");
            Console.WriteLine("Hrvatska - Češka 5:1");
            Console.WriteLine("Hrvatska - Češka 5:1");
            Console.WriteLine("Hrvatska - Češka 5:1");
            Console.WriteLine("Hrvatska - Češka 5:1");
            Console.WriteLine("Hrvatska - Češka 5:1");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Hrvatska - Češka 5:1");

            }

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0}.", i+1);

            }

            Console.WriteLine((100/2) * 100 + 100/2);
            Console.WriteLine(100/2 * (100 + 1));

            int suma = 0;
            for(int i = 0; i < 100; i++)
            {
                suma = suma + i + 1;
            }
            Console.WriteLine(suma);

            for(int i = 10; i > 0; i--)
            {
                Console.WriteLine(i);
            }

              for(int i = 1; i <= 90 ; i += 5)
            {
                Console.WriteLine(i);
            }




        }
    }
}
