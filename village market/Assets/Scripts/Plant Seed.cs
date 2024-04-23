using System.Linq;
using Model;
using UnityEngine;

public class PlantSeed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var seedBeds = Objects.Seedbeds;
        var things = Objects.Things;

        foreach (var coords in seedBeds.Keys)
        {
            var thing = things.FirstOrDefault(x => x.IsCarried && x is Seed);
            if (thing == null) continue;

            var cordSeed = (Vector2)thing.ThingObj.transform.position;
            var cordSeedBed = CreateSeedbeds.ConvertSeedbedCoordinatesToVector(coords);

            if (Vector2.Distance(cordSeed, cordSeedBed) <= new Vector2(2.5f, 2.5f).magnitude &&
                Input.GetKeyDown(KeyCode.Mouse0) && !seedBeds[coords].IsPlanted)
            {
                Debug.Log("Семечко посажено: " + cordSeed);
                seedBeds[coords].IsPlanted = true;
                thing.CanCarried = false;
                thing.IsCarried = false;
                Player.IsCarrying = false;
                thing.ThingObj.transform.position = cordSeedBed;
            }
        }
    }
}