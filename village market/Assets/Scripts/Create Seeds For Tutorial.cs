using System.Collections;
using System.Collections.Generic;
using Model;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateSeedsForTutorial : MonoBehaviour
{
    public GameObject seedsObjs;
    public int seedsCount = 20;
    private GameObject platform;
    
    // Start is called before the first frame update
    void Start()
    {
        platform = Platform.PlatformObj;
        var seeds = new List<Seed>();
        
        for (var i = 0; i < seedsCount; i++)
        {
            seeds.Add(new Seed()
            {
                Cords = GetRandomPlatformPosition(Seed.SeedPrefab),
                ThingObj = Instantiate(Seed.SeedPrefab, GetRandomPlatformPosition(Seed.SeedPrefab),
                    Quaternion.identity, seedsObjs.transform),
            });
        }

        Objects.Things.AddRange(seeds);
    }

    private Vector2 GetRandomPlatformPosition(GameObject obj)
    {
        var objScale = obj.transform.localScale;
        var platformScale = platform.transform.localScale;
        var maxValueX = (int)((platformScale.x - objScale.x) / 2);
        var maxValueY = (int)((platformScale.y - objScale.y) / 2);
        var rnd = new System.Random();
        //return new Vector2(rnd.Next(-maxValueX, maxValueX), rnd.Next(-maxValueY, maxValueY));
        return new Vector2(rnd.Next(-maxValueX, 0), rnd.Next(-maxValueY, maxValueY));
    }

    // Update is called once per frame
    void Update()
    {
    }
}