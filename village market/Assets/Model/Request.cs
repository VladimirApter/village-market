using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Request
    {
        public GameObject RequestObj { get; set; }
        public static GameObject RequestPrefab { get; set; }
        public List<GameObject> Fruits { get; set; } = new();
        public Dictionary<string, int> FruitsCount = new() { { "fruit", 0 } };
    }
}
