using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Table : SquareSection
    {
        public GameObject TableObj { get; set; }
        public static GameObject TablePrefab { get; set; }
        public Dictionary<string, int> CountProduct = new(){{"fruit", 0}};
    }
}