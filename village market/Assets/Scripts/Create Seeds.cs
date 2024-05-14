using System.Collections;
using System.Collections.Generic;
using Model;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateSeeds : MonoBehaviour
{
    public GameObject seedsObjs;
    public int beetSeedsCount = 10;
    public int wheatSeedsCount = 10;
    private GameObject platform;
    
    // Start is called before the first frame update
    void Start()
    {
        platform = Platform.PlatformObj;
        var seeds = new List<Seed>();
        
        for (var i = 0; i < beetSeedsCount; i++)
        {
            seeds.Add(new BeetSeed()
            {
                ThingObj = Instantiate(BeetSeed.BeetSeedPrefab, SquareSection.ConvertSectionToVector((-4, 4)),//GetRandomPlatformPosition(),
                    Quaternion.identity, seedsObjs.transform),
            });
        }
        
        for (var i = 0; i < wheatSeedsCount; i++)
        {
            seeds.Add(new WheatSeed()
            {
                ThingObj = Instantiate(WheatSeed.WheatSeedPrefab, SquareSection.ConvertSectionToVector((-2, 4)),//GetRandomPlatformPosition(),
                    Quaternion.identity, seedsObjs.transform),
            });
        }

        foreach (var seed in seeds)
            seed.Cords = seed.ThingObj.transform.position;
        Objects.Things.AddRange(seeds);
    }

    private Vector2 GetRandomPlatformPosition()
    {
        var platformScale = platform.transform.localScale;
        var maxValueX = (int)((platformScale.x - 5) / 2);
        var maxValueY = (int)((platformScale.y - 5) / 2);
        var rnd = new System.Random();
        //return new Vector2(rnd.Next(-maxValueX, maxValueX), rnd.Next(-maxValueY, maxValueY));
        return new Vector2(rnd.Next(-maxValueX, 0), rnd.Next(-maxValueY, maxValueY));
    }

    // Update is called once per frame
    void Update()
    {
    }
}