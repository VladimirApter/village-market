using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateSeeds : MonoBehaviour
{
    public GameObject seedsObjs;
    public GameObject seedPrefab;
    public int seedsCount = 20;
    public GameObject platform = Platform.PlatformObj;
    
    // Start is called before the first frame update
    void Start()
    {
        var things = new List<Thing>();
        for (var i = 0; i < seedsCount; i++)
        {
            things.Add(new Seed()
            {
                IsCarried = false,
                ThingObj = Instantiate(seedPrefab, GetRandomPlatformPosition(seedPrefab),
                    Quaternion.identity, seedsObjs.transform),
                SeedPrefab = seedPrefab
            });
        }

        Objects.things = things;
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
