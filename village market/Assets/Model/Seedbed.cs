using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Model
{
    public class Seedbed : SquareSection
    {
        public GameObject SeedbedObj { get; set; }
        public static GameObject SeedbedPrefab { get; set; }
        public bool IsPoured { get; set; }
    }
}