using System;
using System.Collections.Generic;
using System.Text;

namespace Paskaita10uzduotis3
{
    class Grains
    {
        public string name { get; set; }
        public TimeSpan deliveryTime { get; set; }
        public int unloading { get; set; }
        public int weight { get; set; }
        public TimeSpan unloadingStart { get; set; }
        public Grains() { }
        public Grains(string name, TimeSpan deliveryTime, int unloading, int weight)
        {
            this.name = name;
            this.deliveryTime = deliveryTime;
            this.unloading = unloading;
            this.weight = weight;
        }
        public Grains(string name, TimeSpan deliveryTime, int unloading, int weight, TimeSpan unloadingStart)
        {
            this.name = name;
            this.deliveryTime = deliveryTime;
            this.unloading = unloading;
            this.weight = weight;
            this.unloadingStart = unloadingStart;
        }
        public override string ToString()
        {           // objekto spausdinimo formatas
            return String.Format("{0,-17} {1,15} {2,16} {3,10} {4,17}",
                 name, deliveryTime, unloading, weight, unloadingStart);
        }
    }
}
