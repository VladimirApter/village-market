using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateThings : MonoBehaviour
{
    public GameObject thingsObjs;
    public GameObject thingPrefab = Thing.ThingPrefab;
    public static List<Thing> things;
    public int thingsCount = 10;
    public GameObject platform = Platform.PlatformObj;
    
    // Start is called before the first frame update
    void Start()
    {
        things = new List<Thing>();
        for (var i = 0; i < thingsCount; i++)
            things.Add(new Thing() 
            {
                IsCarried = false,
                ThingObj = Instantiate(thingPrefab, GetRandomPlatformPosition(thingPrefab), 
                    Quaternion.identity, thingsObjs.transform)
            });
    }

    private Vector2 GetRandomPlatformPosition(GameObject obj)
    {
        var objScale = obj.transform.localScale;
        var platformScale = platform.transform.localScale;
        var maxValueX = (int)((platformScale.x - objScale.x) / 2);
        var maxValueY = (int)((platformScale.y - objScale.y) / 2);
        var rnd = new System.Random();
        return new Vector2(rnd.Next(-maxValueX, maxValueX), rnd.Next(-maxValueY, maxValueY));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
