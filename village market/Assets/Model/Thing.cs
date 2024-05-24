using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Model
{
    public class Thing
    {
        public GameObject ThingObj { get; set; }
        public bool IsCarried { get; set; } = false;
        public bool CanCarried { get; set; } = true;

        public UnityEngine.Vector2 Cords => ThingObj.transform.position;
    }
}