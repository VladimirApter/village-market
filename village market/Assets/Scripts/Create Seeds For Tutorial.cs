using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateSeedsForTutorial : MonoBehaviour
{
    public GameObject seedsObjs;
    public int seedsCount = 20;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var seeds = Objects.Things.Where(t => t is Seed).ToArray();
        var seedsToAdd = new List<Seed>();
        
        for (var i = 0; i < seedsCount - seeds.Count(s => s is WheatSeed); i++)
        {
            seedsToAdd.Add(new WheatSeed()
            {
                ThingObj = Instantiate(WheatSeed.WheatSeedPrefab, SquareSection.ConvertSectionToVector((-2, 4)),
                    Quaternion.identity, seedsObjs.transform),
            });
        }
        
        Objects.Things.AddRange(seedsToAdd);
    }
}