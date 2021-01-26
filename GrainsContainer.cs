using System;
using System.Collections.Generic;
using System.Text;

namespace Paskaita10uzduotis3
{
    class GrainsContainer
    {
        const int Cmax = 10000;
        const string CBeginWork = "08:00:00";
        const string CEndWork = "12:50:00";
        public int n { get; set; }
        public Grains[] GrainsArray { get; set; }
        public GrainsContainer()
        {
            n = 0;
            GrainsArray = new Grains[Cmax];
        }
        public void AddGrains(Grains c)
        {           // + naujas iškrovimas
            if (n < Cmax)
                GrainsArray[n++] = c;
        }
        public void Sort()
        {           // rikiuojami iškrovimai
            for (int i = 0; i < n - 1; i++)
            {
                int minInd = i;
                for (int j = i + 1; j < n; j++)
                    if (GrainsArray[j].deliveryTime < GrainsArray[minInd].deliveryTime)
                        minInd = j;
                Grains temp = GrainsArray[minInd];
                GrainsArray[minInd] = GrainsArray[i];
                GrainsArray[i] = temp;
            }
        }
        public void UnloadingStart(out int ind, out TimeSpan endWork)
        {           // paskaičiuojama iškrovimo pradžia kiekvienam kroviniui
            TimeSpan TimeA = TimeSpan.Parse(CBeginWork);
            TimeSpan TimeB = TimeSpan.Parse(CEndWork);
            TimeSpan TimeC = new TimeSpan();
            ind = n;
            for (int i = 0; i < n; i++)
            {
                if (GrainsArray[i].deliveryTime >= TimeA)
                    TimeC = GrainsArray[i].deliveryTime;
                else TimeC = TimeA;
                if (TimeC > TimeB)
                {
                    ind = i;
                    break;
                }
                GrainsArray[i].unloadingStart = TimeC;
                TimeA = TimeC + new TimeSpan(0, GrainsArray[i].unloading, 0);
            }       // baigiasi paskutinis iškrovimas prieš pietus
            endWork = GrainsArray[ind - 1].unloadingStart + new TimeSpan(0, GrainsArray[ind - 1].unloading, 0);
        }
        public int SumWeight(int ind)
        {           // paskaičiuojamas iškrautų krovinių svoris
            int sum = 0;
            for (int i = 0; i < ind; i++)
                sum += GrainsArray[i].weight;
            return sum;
        }
    }
}
