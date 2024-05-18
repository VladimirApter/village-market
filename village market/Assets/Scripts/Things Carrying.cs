using System;
using System.Linq;
using Model;
using UnityEngine;

public class ThingsCarrying : MonoBehaviour
{
    public GameObject player = Player.PlayerObj;
    private Vector2 direction;

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
                var thing = things.FirstOrDefault(x => x.IsCarried);
                if (thing != null)
                {
                    thing.IsCarried = false;
                    
                    var spriteRenderer = thing.ThingObj.GetComponent<SpriteRenderer>();
                    spriteRenderer.sortingLayerName = "things";
                    spriteRenderer.sortingOrder = 0;
                }
            }
        }

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        if (direction is { x: 0, y: 0 })
            return;

        foreach (var thing in things.Where(x => x.IsCarried))
        {
            thing.ThingObj.transform.position = player.transform.position + (Vector3)direction.normalized * 2.2f;
            
            var spriteRenderer = thing.ThingObj.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = "player";
            spriteRenderer.sortingOrder = 0;
            spriteRenderer.flipX = false;
            if (Math.Abs(direction.y + 1f) < 1e-3)
            {
                spriteRenderer.sortingLayerName = "player";
                spriteRenderer.sortingOrder = 2;
            }
            if (Math.Abs(direction.x + 1f) < 1e-3)
                spriteRenderer.flipX = true;
            
            break;
        }
    }

    private float CalculateDistancePlayerToThing(Thing t)
    {
        return Vector3.Distance(player.transform.position, t.ThingObj.transform.position);
    }
}