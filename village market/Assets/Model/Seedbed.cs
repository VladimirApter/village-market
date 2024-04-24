using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Model
{
    public class Seedbed
    {
        public GameObject SeedbedObj { get; set; }
        public static GameObject SeedbedPrefab { get; set; }
        public bool IsPlanted { get; set; }
        public bool IsPoured { get; set; }
        public Seed Seed;
        public UnityEngine.Vector2 Coords { get; set; }
    }
}