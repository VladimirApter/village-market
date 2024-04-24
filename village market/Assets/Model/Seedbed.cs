using UnityEngine;

namespace Model
{
    public class Seedbed
    {
        public GameObject SeedbedObj { get; set; }
        public static GameObject SeedbedPrefab { get; set; }
        public bool IsPlanted { get; set; }
        public bool IsPoured { get; set; }
        public Seed Seed;
    }
}