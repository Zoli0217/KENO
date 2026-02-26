namespace KENO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<NapiKeno> adatok = File.ReadAllLines("Huzasok.csv")
                .Skip(1)
                .Select(sor => new NapiKeno(sor.Split(";")))
                .ToList();

            Console.WriteLine("6. feladat: Állomány beolvasása sikeres!");

            
            Console.WriteLine($"7. feladat: Hibás sorok száma: " +
                $"{adatok.Where(x => !x.Helyes()).Count()}");

            adatok = adatok.Where(x => x.Helyes()).ToList();

            Console.WriteLine("9. feladat: Nyeremény számítása");
            List<int> tippek;
            while (true)
            {
                Console.Write("Kérem a tippjét! Vesszővel elválasztva sorolja fel a számokat: ");
                string input = Console.ReadLine();
                
                tippek = input.Split(",").Select(x => int.Parse(x)).ToList();
                
                if (tippek == null || tippek.Count < 1 || tippek.Count > 10)
                {
                    Console.WriteLine("A játéktípus 1..10 lehet!");
                    continue;
                }
                break;
            }

            int osszeg;
            while (true)
            {

                Console.WriteLine("Kérem a fogadási összeget:");
                osszeg = int.Parse(Console.ReadLine());

                if (osszeg < 200 || osszeg > 1000 || osszeg % 200 != 0)
                {
                    Console.WriteLine("Hibás összeg!");
                    continue;
                }
                break;

            }

            int nyeremeny = Szorzo(adatok
                    .OrderByDescending(x => x.huzasDatum).First(), tippek) * osszeg;

            if (nyeremeny > 0)
                Console.WriteLine($"Nyereménye: {nyeremeny}");
            else
                Console.WriteLine("Sajnos nem nyert!");



            List<int> tippek2020 = new List<int>() { 17, 28, 32, 44, 54, 63, 72, 75 };
            int tippOsszege = 4 * 200;

            int megnyert = 0;
            int elkoltottOsszeg = 0;

            Console.WriteLine("10. feladat:");
            Console.WriteLine($"8-as játék 2020-ban, tét:4X [17,28,32,44,54,63,72,75]");
            foreach (var huzas in adatok)
            {
                if (huzas.ev != 2020)
                    continue;

                int nyeremenyOsszeg = Szorzo(huzas, tippek2020) * tippOsszege;

                elkoltottOsszeg += tippOsszege;

                if (nyeremenyOsszeg > 0)
                {
                    Console.WriteLine($"{huzas.huzasDatum} - {nyeremenyOsszeg}");
                    megnyert += nyeremenyOsszeg;
                }     
            }

            Console.WriteLine($"Összesen {elkoltottOsszeg} Ft-ot költött Kenóra");
            Console.WriteLine($"Összesen {megnyert} Ft-ot nyert");
        }

        static int Szorzo(NapiKeno keno, List<int> tippek)
        {
            Dictionary<String, int> nyeroParok = new Dictionary<string, int>(){
                {"10-10",1000000}, {"10-9",8000}, {"10-8",350}, {"10-7",30}, {"10-6",3}, {"10-5",1}, {"10-0",2},
                {"9-9",100000}, {"9-8",1200}, {"9-7",100}, {"9-6",12}, {"9-5",3}, {"9-0",1},
                {"8-8",20000}, {"8-7",350}, {"8-6",25}, {"8-5",5}, {"8-0",1},
                {"7-7",5000}, {"7-6",60}, {"7-5",6}, {"7-4",1}, {"7-0",1},
                {"6-6",500}, {"6-5",20}, {"6-4",3}, {"6-0",1},
                {"5-5",200}, {"5-4",10}, {"5-3",2},
                {"4-4",100}, {"4-3",2},
                {"3-3",15}, {"3-2",1},
                {"2-2",6},
                {"1-1",2}
            };
            int jatekTipus = tippek.Count;
            int talalatokSzama = keno.TalalatSzam(tippek);
            string kulcs = jatekTipus + "-" + talalatokSzama;

            if (nyeroParok.Keys.Contains(kulcs))
                return nyeroParok[kulcs];
            else
                return 0;
        }
    }
}
