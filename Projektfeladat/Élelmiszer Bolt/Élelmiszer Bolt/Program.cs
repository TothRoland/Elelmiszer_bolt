using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Élelmiszer_Bolt
{
    class Program
    {
        #region Struktúra Tömb Létrehozása + Globális változók
        public struct adatok
        {
            public string megnevezes, mennyiseg;
            public int darabszam, ar;
        }
        static int index = File.ReadAllLines("index.txt").Length;
        static int n = File.ReadAllLines($"arucikkek{index}.txt").Length;
        static adatok[] adat = new adatok[100];

        static int szin;
        #endregion

        #region Főmenü
        static void fomenu()
        {
            szinValtas();

            Console.Clear();

            Console.SetCursorPosition(30, 6);
            Console.WriteLine("1. Belépés a boltban dolgozóként");
            Console.SetCursorPosition(30, 8);
            Console.WriteLine("2. Belépés vásárlóként");
            Console.SetCursorPosition(30, 10);
            Console.WriteLine("3. Kilépés");

            int valasztas = 0;
            Console.SetCursorPosition(30, 12);
            Console.Write("Választott menüpont: ");
            valasztas = int.Parse(Console.ReadLine());

            switch (valasztas)
            {
                case 1:
                    {
                        dolgozo();
                        break;
                    }
                case 2:
                    {
                        vasarlo();
                        break;
                    }
                case 3:
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\nKilépés...");
                        break;
                    }
            }
        }
        #endregion

        #region Dolgozói Főmenü
        static void dolgozo()
        {
            szinValtas();

            Console.Clear();

            int valasztas;

            Console.SetCursorPosition(30, 6);
            Console.WriteLine("1. Új árucikk felvitele");
            Console.SetCursorPosition(30, 8);
            Console.WriteLine("2. Árufeltöltés");
            Console.SetCursorPosition(30, 10);
            Console.WriteLine("3. Árváltoztatás");
            Console.SetCursorPosition(30, 12);
            Console.WriteLine("4. Jelenlegi árucikkek kilistázása");
            Console.SetCursorPosition(30, 14);
            Console.WriteLine("5. Visszalépés a főmenübe");

            Console.SetCursorPosition(30, 16);
            Console.Write("Választott menüpont: ");
            valasztas = int.Parse(Console.ReadLine());

            switch (valasztas)
            {
                case 1:
                    {
                        dolgozoM1();
                        break;
                    }
                case 2:
                    {
                        dolgozoM2();
                        break;
                    }
                case 3:
                    {
                        dolgozoM3();
                        break;
                    }
                case 4:
                    {
                        dolgozoM4();
                        break;
                    }
                case 5:
                    {
                        fomenu();
                        break;
                    }
            }
        }
        #endregion

        #region Dolgozói Menüpont Függvények

        #region dolgozoM1
        static void dolgozoM1()
        {
            //Új árucikk felvitele

            Console.Clear();

            Console.Write("Az új árucikk megnevezése: ");
            string megn = Console.ReadLine();
            Console.Write("Az új árucikk mennyisége: ");
            string menny = Console.ReadLine();
            Console.Write("Az új árucikk készleten lévő darabszáma: ");
            int db = int.Parse(Console.ReadLine());
            Console.Write("Az új árucikk ára: ");
            int ara = int.Parse(Console.ReadLine());

            beolvas();

            int index = File.ReadAllLines("index.txt").Length;
            FileStream f = new FileStream($"arucikkek{index}.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(f);

            sw.WriteLine($"{megn};{menny};{db};{ara}");

            sw.Close();
            f.Close();

            n++;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nAz új árucikk felvitele megtörtént.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nNyomj meg egy gombot a menühöz való visszatéréshez.");

            Console.ReadKey();
            dolgozo();
        }
        #endregion

        #region dolgozoM2
        static void dolgozoM2()
        {
            //Árufeltöltés

            Console.Clear();

            Console.Write("Melyik árucikk készletéhez érkezett szállítmány: ");
            string aru = Console.ReadLine();
            Console.Write("Mennyi darab érkezett: ");
            int darab = int.Parse(Console.ReadLine());

            beolvas();

            for (int i = 0; i < n; i++)
            {
                if (adat[i].megnevezes == aru)
                {
                    adat[i].darabszam += darab;
                    break;
                }
            }

            int index = File.ReadAllLines("index.txt").Length;
            FileStream f = new FileStream($"arucikkek{index + 1}.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(f);

            for (int i = 0; i < n; i++)
            {
                sw.WriteLine($"{adat[i].megnevezes};{adat[i].mennyiseg};{adat[i].darabszam};{adat[i].ar}");
            }

            sw.Close();
            f.Close();

            FileStream f1 = new FileStream("index.txt", FileMode.Append);
            StreamWriter sw1 = new StreamWriter(f1);

            sw1.WriteLine("x");

            sw1.Close();
            f1.Close();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nAz árufeltöltés megtörtént.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nNyomj meg egy gombot a menühöz való visszatéréshez.");

            Console.ReadKey();
            dolgozo();
        }
        #endregion

        #region dolgozoM3
        static void dolgozoM3()
        {
            //Árváltoztatás

            Console.Clear();

            Console.Write("Melyik árucikknek változott az ára: ");
            string aru = Console.ReadLine();
            Console.Write("Az új ár: ");
            int ar = int.Parse(Console.ReadLine());

            beolvas();

            for (int i = 0; i < n; i++)
            {
                if (adat[i].megnevezes == aru)
                {
                    adat[i].ar = ar;
                    break;
                }
            }

            int index = File.ReadAllLines("index.txt").Length;
            FileStream f = new FileStream($"arucikkek{index + 1}.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(f);

            for (int i = 0; i < n; i++)
            {
                sw.WriteLine($"{adat[i].megnevezes};{adat[i].mennyiseg};{adat[i].darabszam};{adat[i].ar}");
            }

            sw.Close();
            f.Close();

            FileStream f1 = new FileStream("index.txt", FileMode.Append);
            StreamWriter sw1 = new StreamWriter(f1);

            sw1.WriteLine("x");

            sw1.Close();
            f1.Close();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nAz árváloztatás megtörtént.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nNyomj meg egy gombot a menühöz való visszatéréshez.");

            Console.ReadKey();
            dolgozo();
        }
        #endregion

        #region dolgozoM4
        static void dolgozoM4()
        {
            //Árucikkek kilistázása

            Console.Clear();

            beolvas();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{adat[i].megnevezes}\t{adat[i].mennyiseg}\t{adat[i].darabszam}db\t{adat[i].ar}Ft");
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nNyomj meg egy gombot a menühöz való visszatéréshez.");

            Console.ReadKey();
            dolgozo();
        }
        #endregion
        #endregion

        #region Vásárlói Főmenü
        static void vasarlo()
        {
            szinValtas();

            Console.Clear();

            int valasztas;

            Console.SetCursorPosition(30, 6);
            Console.WriteLine("1. Árucikkek kilistázása");
            Console.SetCursorPosition(30, 8);
            Console.WriteLine("2. Vásárlás");
            Console.SetCursorPosition(30, 10);
            Console.WriteLine("3. Visszalépés a főmenübe");

            Console.SetCursorPosition(30, 12);
            Console.Write("Választott menüpont: ");
            valasztas = int.Parse(Console.ReadLine());

            switch (valasztas)
            {
                case 1:
                    {
                        vasarloM1();
                        break;
                    }
                case 2:
                    {
                        vasarloM2();
                        break;
                    }
                case 3:
                    {
                        fomenu();
                        break;
                    }
            }
        }
        #endregion

        #region Vásárlói Menüpont Függvények
        #region vasarloM1
        static void vasarloM1()
        {
            //Árucikkek kilistázása

            Console.Clear();

            beolvas();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{adat[i].megnevezes}\t{adat[i].mennyiseg}\t{adat[i].darabszam}db\t{adat[i].ar}Ft");
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nNyomj meg egy gombot a menühöz való visszatéréshez.");

            Console.ReadKey();
            vasarlo();
        }
        #endregion

        #region vasarloM2
        static void vasarloM2()
        {
            //Vásárlás

            Console.Clear();

            Console.Write("Melyik termékből vásárolsz: ");
            string aru = Console.ReadLine();
            Console.Write("Mennyit vásárolsz belőle: ");
            int menny = int.Parse(Console.ReadLine());
            Console.Write("Fizetési mód (készpénz, bankkártya vagy utalvány): ");
            string tipus = Console.ReadLine();

            beolvas();

            int j = 0;

            for (int i = 0; i < n; i++)
            {
                if (adat[i].megnevezes == aru)
                {
                    j = i;
                    break;
                }
            }

            int index = File.ReadAllLines("index.txt").Length;
            FileStream f = new FileStream($"arucikkek{index + 1}.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(f);

            for (int i = 0; i < n; i++)
            {
                if (i == j)
                {
                    sw.WriteLine($"{adat[i].megnevezes};{adat[i].mennyiseg};{adat[i].darabszam - menny};{adat[i].ar}");
                }
                else
                {
                    sw.WriteLine($"{adat[i].megnevezes};{adat[i].mennyiseg};{adat[i].darabszam};{adat[i].ar}");
                }
            }

            sw.Close();
            f.Close();

            FileStream f1 = new FileStream("index.txt", FileMode.Append);
            StreamWriter sw1 = new StreamWriter(f1);

            sw1.WriteLine("x");

            sw1.Close();
            f1.Close();

            if (tipus == "bankkártya")
            {
                Console.WriteLine($"\nFizetendő összeg: {adat[j].ar * menny * 0.95}Ft");
                Console.WriteLine($"\nNem jár visszajáró.\n\nKöszönjük a vásárlását!");
            }
            else
            {
                Console.WriteLine($"\nFizetendő összeg: {adat[j].ar * menny}Ft");
                Console.Write("Fizetett összeg: ");
                int fiz = int.Parse(Console.ReadLine());
                Console.WriteLine($"\nVisszajáró: {fiz - adat[j].ar * menny}Ft\n\nKöszönjük a vásárlását!");
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nNyomj meg egy gombot a menühöz való visszatéréshez.");

            Console.ReadKey();
            vasarlo();
        }
        #endregion
        #endregion

        #region Általános Függvények
        static void beolvas()
        {
            int index = File.ReadAllLines("index.txt").Length;
            FileStream f = new FileStream($"arucikkek{index}.txt", FileMode.Open);
            StreamReader sr = new StreamReader(f);

            for (int i = 0; i < n; i++)
            {
                string[] sor = sr.ReadLine().Split(';');
                adat[i].megnevezes = sor[0];
                adat[i].mennyiseg = sor[1];
                adat[i].darabszam = int.Parse(sor[2]);
                adat[i].ar = int.Parse(sor[3]);
            }

            sr.Close();
            f.Close();
        }

        static void futtatas()
        {
            Console.SetCursorPosition(30, 6);
            Console.WriteLine("1. Világos mód");
            Console.SetCursorPosition(30, 8);
            Console.WriteLine("2. Sötét mód");

            Console.SetCursorPosition(30, 10);
            Console.Write("Választott menüpont: ");
            szin = int.Parse(Console.ReadLine());

            fomenu();

            Console.ReadKey();
        }

        static void szinValtas()
        {
            switch (szin)
            {
                case 1:
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    }
                case 2:
                    { 
                        break;
                    }
            }
        }
        #endregion

        static void Main(string[] args)
        {
            futtatas();
        }
    }
}