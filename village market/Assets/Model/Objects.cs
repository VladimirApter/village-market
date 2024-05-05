using System.Collections.Generic;
using System.Numerics;

namespace Model
{
    public static class Objects
    {
        public static List<Thing> Things { get; set; } = new();
        public static List<Instrument> Instruments { get; set; } = new();
        public static Dictionary<(int, int), Seedbed> Seedbeds { get; set; } = new();
        public static Dictionary<(int, int), Table> Table { get; set; } = new();
        public static Dictionary<(int, int), Request> Requests { get; set; } = new();
        public static List<(UnityEngine.Vector2, Fruit)> Fruits { get; set; } = new();
    }
}