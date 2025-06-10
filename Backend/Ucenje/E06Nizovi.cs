using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucenje
{
    internal class E06Nizovi
    {

        public static void Izvedi()
        {

            Console.WriteLine("E06Nizovi");

            int[] temp = new int[12];

            temp[0] = -1;
            temp[1] = 1;
            temp[temp.Length - 1] = 1;

            Console.WriteLine(temp);

            Console.WriteLine(string.Join(",", temp));

            Console.WriteLine(temp[1]);

            string[] gradovi = new string[3];

            gradovi[0] = "Osijek";
            gradovi[1] = "Zagreb";
            gradovi[2] = "Donji Miholjac";
            //gradovi[3] = "Josipovac";

            Console.WriteLine(gradovi[2]);

            string ime = "Edunova";

            Console.WriteLine(ime[5]);

            double[] iznosi = { 2.3, 4.7, 1,2, 8,4 };

            int[,] tablica = {
            {1,2,3},
            {2,4,5}, 
            {4,6,7},
            };

            Console.WriteLine(tablica[1,2]);
            tablica[1, 0] = 17;

            int[,,] kocka = new int[10, 10, 10];
            int[,,,,,,] multiverse;


        }

    }
}
