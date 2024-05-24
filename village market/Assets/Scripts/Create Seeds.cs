using System.Collections;
using System.Collections.Generic;
using Model;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateSeeds : MonoBehaviour
{
    public GameObject seedsObjs;
    private int beetSeedsCount = 10;
    private int wheatSeedsCount = 10;
    private int appleTreeSeedsCount = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        var seeds = new List<Seed>();
        
        for (var i = 0; i < beetSeedsCount; i++)
        {
            seeds.Add(new BeetSeed()
            {
                ThingObj = Instantiate(BeetSeed.BeetSeedPrefab, SquareSection.ConvertSectionToVector((-4, 4)),
                    Quaternion.identity, seedsObjs.transform),
            });
        }
        
        for (var i = 0; i < wheatSeedsCount; i++)
        {
            seeds.Add(new WheatSeed()
            {
                ThingObj = Instantiate(WheatSeed.WheatSeedPrefab, SquareSection.ConvertSectionToVector((-2, 4)),
                    Quaternion.identity, seedsObjs.transform),
            });
        }
        
        for (var i = 0; i < appleTreeSeedsCount; i++)
        {
            seeds.Add(new AppleTreeSeed()
            {
                ThingObj = Instantiate(AppleTreeSeed.AppleTreeSeedPrefab, SquareSection.ConvertSectionToVector((-1, 4)),
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