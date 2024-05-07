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
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.L))
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
                            x.Value.IsBusy && SquareSection.ConvertSectionToVector(x.Key) ==
                            closestThing.Cords);

                        if (seedbed.Value != null)
                        {
                            if (closestThing is Seed seed)
                                seed.Seedbed = null;

                            seedbed.Value.IsBusy = false;
                        }

                        if (closestThing is Fruit)
                        {
                            var table = Objects.Tables.FirstOrDefault(x =>
                                SquareSection.ConvertSectionToVector(x.Key) ==
                                closestThing.Cords).Value;
                            if (table != null)
                            {
                                switch (closestThing)
                                {
                                    case Wheat:
                                        table.FruitsCount["wheat"]--;
                                        break;
                                    case Beet:
                                        table.FruitsCount["beet"]--;
                                        break;
                                    default:
                                        table.FruitsCount["fruit"]--;
                                        break;
                                }

                                table.Fruits.Remove((Fruit)closestThing);
                            }
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