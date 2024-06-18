using System.Collections;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Model
{
    public class Seedbed : SquareSection
    {
        public GameObject SeedbedObj { get; set; }
        public static GameObject SeedbedPrefab { get; set; }
        public static int FramesToDestroy { get; } = 30;
        public static bool CanCreate { get; set; } = true;
        public bool IsPoured { get; set; }
        public bool CanDestroy { get; set; }
        public int DestroyFramesCount { get; set; }
        public IEnumerator WaitAndCanDestroy()
        {
            yield return new WaitForSeconds(2f);
            CanDestroy = !CanDestroy;
        }
        public static IEnumerator WaitAndCanCreate()
        {
            CanCreate = false;
            yield return new WaitForSeconds(0.1f);
            CanCreate = true;
        }
        public GameObject DestroyBar { get; set; }
    }
}