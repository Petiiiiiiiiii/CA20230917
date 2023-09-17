using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace autokOOP
{
    class Auto
    {
        public string Azonosito { get; set; } //property (tulajdonság/jellemző)
        //a fentit a prop tab-tab módszerrel könnyen elő lehet hívni
        //de így is lehetne egyszerűbben:
        //public string azonosito;
        public int Loero { get; set; }
        public double Tomeg { get; set; }
        public int Gyorsulas { get; set; }
        public int Beavatkozas { get; set; }

        public Auto(string sor) //konstruktor, példányosításkor automatikusan végrehajtódik
        {
            string[] atmeneti = sor.Split(';');
            Azonosito = atmeneti[0];
            Loero = Convert.ToInt32(atmeneti[1]);
            Tomeg = Convert.ToDouble(atmeneti[2]);
            Gyorsulas = Convert.ToInt32(atmeneti[3]);
            Beavatkozas = Convert.ToInt32(atmeneti[4]);
        }

        public override string ToString()
        {
            return $"{Azonosito,-18} | {Loero,4} lóerő | {Tomeg,4} t | {Gyorsulas,2} mp | {Beavatkozas,2}* kellett beavatkozni";
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            List<Auto> autok = new List<Auto>(); //példányosítás
            //2.Az adatokat olvassa be fájlból és tárolja el egy listában.
            foreach (var item in File.ReadAllLines(@"..\..\..\autok.txt"))
            {
                autok.Add(new Auto(item));
            }


            Console.WriteLine("\n4. kiírás");
            foreach (var item in autok)
            {
                Console.WriteLine(item);
            }

            //3.Írja ki a képernyőre a minta szerint a leggyorsabb autó adatait.
            //(Az a leggyorsabb, ami a legkevesebb idő alatt gyorsul 100 - ra.) 

            legGyorsabb(autok);

            //4.Írja ki a képernyőre a minta szerint a legkevésbé önálló autó azonosítóját.
            //(Az a legkevésbé önálló, amelynél a legtöbbet kellett beavatkozni a vezetésbe.)

            legkevesbeOnallo(autok);

            //6.Írja ki a képernyőre a minta szerint az autók átlag tömegét. 

            atlagTomeg(autok);

            //7.Írja ki a képernyőre, és egy új fájlba autónként a következő adatokat szóközzel elválasztva:
            //sorszám(a feldolgozás sorrendjében automatikusan generált), tömeg kg-ban. (1 tonna = 1000 kg).

            kiiras(autok);


            Console.ReadKey();

        }

        static void kiiras(List<Auto> a)
        {
            var sw = new StreamWriter(@"..\..\..\tomegek.txt");

            var osszesTomeg = a
                .Select(k => k.Tomeg);

            int index = 1;
            foreach (var sor in osszesTomeg)
            {
                sw.WriteLine($"{index}. autó tömege : {sor * 1000} kg");
                index++;
            }

            sw.Close();
        }

        static void atlagTomeg(List<Auto> a) 
        {
            double atlag = 0;
            var osszesTomeg = a
                .Select(k => k.Tomeg);

            foreach (var db in osszesTomeg)
            {
                atlag += db;
            }

            atlag = atlag / osszesTomeg.Count();

            Console.WriteLine();
            Console.WriteLine($"Az autók átlag tömege : {atlag:0.00} t");
        }

        static void legkevesbeOnallo(List<Auto> a) 
        {
            var legkevesbeOnallo = a
                .Where(k => k.Beavatkozas == a.Max(k => k.Beavatkozas))
                .Select(k => k.Azonosito);

            Console.WriteLine();
            Console.WriteLine($"Legkevésbe önálló autó azonosítója : {legkevesbeOnallo.First()}");
        }

        static void legGyorsabb(List<Auto> a)
        {
            var leggyorsabb = a
                .Where(k => k.Gyorsulas == a.Min(k => k.Gyorsulas))
                .First();

            Console.WriteLine();
            Console.WriteLine("Leggyorsabb autó adatai: ");
            Console.WriteLine($"{leggyorsabb}");
        }
    }
}
