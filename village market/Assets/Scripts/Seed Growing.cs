using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

public class SeedGrowing : MonoBehaviour
{
    public GameObject fruitObjs;
    private readonly List<Seed> seedsGrewInThisFrame = new();
    private readonly List<Fruit> newFruitsInThisFrame = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var seed in Objects.Things.OfType<Seed>())
        {
            if (!seed.IsPlanted)
                seed.GrowingFramesCount = 0;
        }

        seedsGrewInThisFrame.Clear();
        newFruitsInThisFrame.Clear();
        
        foreach (var seed in Objects.Things.OfType<Seed>().Where(seed => seed.IsPlanted && seed.Seedbed.IsPoured))
        {
            seed.GrowingFramesCount++;
            if (seed.GrowingFramesCount < Seed.FramesToGrow) continue;
            
            newFruitsInThisFrame.Add(new Fruit()
            {
                IsCarried = false,
                Cords = seed.Cords,
                ThingObj = Instantiate(Fruit.FruitPrefab, seed.Cords,
                    Quaternion.identity, fruitObjs.transform),
            });
            seedsGrewInThisFrame.Add(seed);
            Destroy(seed.ThingObj);
        }

        Objects.Things.AddRange(newFruitsInThisFrame);
        Objects.Things.RemoveAll(seed => seedsGrewInThisFrame.Contains(seed));
    }
}
