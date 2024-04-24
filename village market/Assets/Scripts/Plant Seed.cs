using System.Linq;
using Model;
using UnityEngine;

public class PlantSeed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    //Update is called once per frame
    void Update()
    {
        var seedBeds = Objects.Seedbeds;
        var things = Objects.Things;
        var seedbedScale = Seedbed.SeedbedPrefab.transform.localScale;
        
        var seed = (Seed)things.FirstOrDefault(x => x.IsCarried && x is Seed);
        if (seed == null) return;
    
        foreach (var coords in seedBeds.Keys)
        {
            var cordSeed = (Vector2)seed.ThingObj.transform.position;
            var cordSeedBed = CreateSeedbeds.ConvertSeedbedCoordinatesToVector(coords);
            
            if (Vector2.Distance(cordSeed, cordSeedBed) <= new Vector2(seedbedScale.x / 2, seedbedScale.y / 2).magnitude &&
                Input.GetKeyDown(KeyCode.Mouse0) && !seedBeds[coords].IsPlanted)
            {
                var seedbed = seedBeds[coords];
                seedbed.IsPlanted = true;
                seedbed.Seed = seed;
                seed.IsCarried = false;
                Player.IsCarrying = false;
                seed.ThingObj.transform.position = cordSeedBed;
            }
        }
    }
}