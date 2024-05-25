using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateSeeds : MonoBehaviour
{
    public GameObject seedsObjs;
    private int beetSeedsCount = 20;
    private int wheatSeedsCount = 20;
    private int appleTreeSeedsCount = 20;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var seeds = Objects.Things.Where(t => t is Seed).ToArray();
        var seedsToAdd = new List<Seed>();
        
        for (var i = 0; i < beetSeedsCount - seeds.Count(s => s is BeetSeed); i++)
        {
            seedsToAdd.Add(new BeetSeed()
            {
                ThingObj = Instantiate(BeetSeed.BeetSeedPrefab, SquareSection.ConvertSectionToVector((-4, 4)),
                    Quaternion.identity, seedsObjs.transform),
            });
        }
        for (var i = 0; i < wheatSeedsCount - seeds.Count(s => s is WheatSeed); i++)
        {
            seedsToAdd.Add(new WheatSeed()
            {
                ThingObj = Instantiate(WheatSeed.WheatSeedPrefab, SquareSection.ConvertSectionToVector((-2, 4)),
                    Quaternion.identity, seedsObjs.transform),
            });
        }
        for (var i = 0; i < appleTreeSeedsCount - seeds.Count(s => s is AppleTreeSeed); i++)
        {
            seedsToAdd.Add(new AppleTreeSeed()
            {
                ThingObj = Instantiate(AppleTreeSeed.AppleTreeSeedPrefab, SquareSection.ConvertSectionToVector((-1, 4)),
                    Quaternion.identity, seedsObjs.transform),
            });
        }
        
        Objects.Things.AddRange(seedsToAdd);
    }
}