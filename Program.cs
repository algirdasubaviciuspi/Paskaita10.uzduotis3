using System;
using System.IO;

namespace Paskaita10uzduotis3
{
    class Program
    {
        const string CFd = "..\\..\\..\\Duomenys3.txt";
        const string CFr = "..\\..\\..\\Rezultatai3.txt";
        static void Main(string[] args)
        {
            GrainsContainer Warehouse = new GrainsContainer();

            ReadData(CFd, Warehouse);
            Warehouse.Sort();

            int ind;
            TimeSpan endWork = new TimeSpan();
            Warehouse.UnloadingStart(out ind, out endWork);

            int sum = Warehouse.SumWeight(ind);

            if (File.Exists(CFr))
                File.Delete(CFr);
            PrintToFile(CFr, "Grūdų sandėlio gavimai", Warehouse, sum, endWork);
        }
        static void ReadData(string file, GrainsContainer Warehouse)
        {               // nuskaito duomenis iš failo
            if (File.Exists(file))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts;
                        parts = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        string name = parts[0].Trim();
                        string timeString = parts[1];
                        TimeSpan deliveryTime = TimeSpan.Parse(timeString);
                        int unloading = int.Parse(parts[2]);
                        int weight = int.Parse(parts[3]);
                        Grains newGrains = new Grains(name, deliveryTime, unloading, weight);
                        Warehouse.AddGrains(newGrains);
                    }
                }
            }
        }
        static void PrintToFile(string file, string tableName, GrainsContainer Warehouse, int sum, TimeSpan endWork)
        {               // išveda rezultatus į failą
            using (var fr = File.AppendText(file))
            {
                fr.WriteLine(tableName);
                if (Warehouse.n > 0)
                {
                    string tableHead = new string('-', 80) + '\n' +
                         String.Format("{0,-17} {1,15} {2,16} {3,10} {4,17}",
                         "Pavadinimas", "Atvežimo laikas", "Iškrovimo trukmė", "Svoris(kg)", "Iškrovimo pradžia") + '\n' +
                         new string('-', 80);
                    fr.WriteLine(tableHead);
                    for (int i = 0; i < Warehouse.n; i++)
                    {
                        fr.WriteLine(Warehouse.GrainsArray[i].ToString());
                    }
                    fr.WriteLine(new string('-', 80));
                    fr.WriteLine();
                    fr.WriteLine("{0}kg {1,5}", sum, endWork);
                }
                else
                {
                    fr.WriteLine("Sąrašas tuščias");
                }
            }
        }
    }
}
