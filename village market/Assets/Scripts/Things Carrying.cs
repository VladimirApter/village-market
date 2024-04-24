using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

public class ThingsCarrying : MonoBehaviour
{
    public GameObject player = Player.PlayerObj;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var things = Objects.Things;
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!Player.IsCarrying)
            {
                var closestThing = things.OrderBy(CalculateDistancePlayerToThing)
                    .FirstOrDefault(t => CalculateDistancePlayerToThing(t) < Player.TakingRadius);

                if (closestThing != null)
                {
                    closestThing.IsCarried = true;
                    Player.IsCarrying = true;
                    
                    if (closestThing is Seed || closestThing is Fruit)
                    {
                        var seedbed = Objects.Seedbeds.FirstOrDefault(x =>
                            x.Value.IsPlanted && CreateSeedbeds.ConvertSeedbedCoordinatesToVector(x.Key) ==
                            closestThing.Cords);
                        if (seedbed.Value != null)
                        {
                            if (closestThing is Seed seed)
                                seed.Seedbed = null;
                                
                            seedbed.Value.IsPlanted = false;
                        }
                    }
                    
                }
            }
            else
            {
                Player.IsCarrying = false;
                foreach (var thing in things.Where(x => x.IsCarried))
                    thing.IsCarried = false;
            }
        }

        foreach (var thing in things.Where(x => x.IsCarried))
            thing.ThingObj.transform.position =
                player.transform.position + (player.transform.rotation * new Vector3(2.2f, 0, 0));
    }

    private float CalculateDistancePlayerToThing(Thing t)
    {
        return Vector3.Distance(player.transform.position, t.ThingObj.transform.position);
    }
}