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
        var coordsSeed = (Vector2)seed.ThingObj.transform.position;

        foreach (var seedBed in seedBeds.Values)
        {
            var coordsSeedBed = seedBed.Coords;

            if (Vector2.Distance(coordsSeed, coordsSeedBed) <=
                new Vector2(seedbedScale.x / 2, seedbedScale.y / 2).magnitude &&
                Input.GetKeyDown(KeyCode.Mouse0) && !seedBed.IsBusy)
            {
                seed.ThingObj.transform.position = coordsSeedBed;
                seed.Cords = coordsSeedBed;
                seed.IsCarried = false;
                seed.Seedbed = seedBed;
                
                seedBed.IsBusy = true;
                Player.IsCarrying = false;
            }
        }
    }
}