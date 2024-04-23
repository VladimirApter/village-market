using System.Collections.Generic;
using System.Numerics;

namespace Model
{
    public static class Objects
    {
        public static List<Thing> Things { get; set; } = new();
        public static List<Instrument> Instruments { get; set; } = new();
        public static Dictionary<(int, int), Seedbed> Seedbeds { get; set; } = new();
    }
}