using System.Collections;
using System.Collections.Generic;
using Model;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateSeedsForTutorial : MonoBehaviour
{
    public GameObject seedsObjs;
    public int seedsCount = 1;
    private GameObject platform;
    
    // Start is called before the first frame update
    void Start()
    {
        platform = Platform.PlatformObj;
        var seeds = new List<Seed>();
        
        for (var i = 0; i < seedsCount; i++)
        {
            seeds.Add(new WheatSeed()
            {
                ThingObj = Instantiate(WheatSeed.WheatSeedPrefab, SquareSection.ConvertSectionToVector((-2, 4)),//GetRandomPlatformPosition(),
                    Quaternion.identity, seedsObjs.transform),
            });
        }

        Objects.Things.AddRange(seeds);
    }

    // Update is called once per frame
    void Update()
    {
    }
}