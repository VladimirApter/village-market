using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Table : SquareSection
    {
        public GameObject TableObj { get; set; }
        public static GameObject TablePrefab { get; set; }
        public List<Fruit> Fruits { get; set; } = new();
        public Dictionary<string, int> FruitsCount = new() { { "beet", 0 }, { "wheat", 0 }, { "fruit", 0 } };
    }
}