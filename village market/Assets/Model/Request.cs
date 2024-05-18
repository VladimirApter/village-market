using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UIElements.Slider;

namespace Model
{
    public class Request
    {
        public GameObject RequestObj { get; set; }
        public static GameObject RequestPrefab { get; set; }
        public List<GameObject> Fruits { get; set; } = new();
        public Dictionary<string, int> FruitsCount = new() { { "beet", 0 }, { "wheat", 0 }, { "fruit", 0 } };
        public int Price { get; set; }
        public int FramesToDestroy { get; set; }
        public int DestroyFramesCount { get; set; }
        public GameObject DestroyBar { get; set; }
        public static GameObject DestroyBarPrefab { get; set; }
    }
}
