using UnityEngine;

namespace Model
{
    public class Seed : Thing
    {
        public static GameObject SeedPrefab { get; set; }
        public static int FramesToGrow { get; } = 100;
        public Seedbed Seedbed { get; set; }

        public bool IsPlanted => Seedbed != null;

        public bool IsGrowing { get; set; }
        public int GrowingFramesCount { get; set; }
    }
}