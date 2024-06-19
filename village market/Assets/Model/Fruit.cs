using UnityEngine;

namespace Model
{
    public class Fruit : Thing
    {
        public static GameObject FruitPrefab { get; set; }
        public bool IsOnTable { get; set; }
    }
}