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
        var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.L))
        {
            var closestThing = things.OrderBy(CalculateDistancePlayerToThing)
                .FirstOrDefault(t => CalculateDistancePlayerToThing(t) < Player.TakingRadius &&
                                     Vector2.Distance(worldPosition, t.ThingObj.transform.position) <=
                                     new Vector2(SquareSection.SquareSectionScale.x / 2,
                                         SquareSection.SquareSectionScale.y / 2).magnitude);
            
            if (!Player.IsCarrying)
            {
                if (closestThing is { CanCarried: true })
                {
                    PlayerMoving.IsActionAtCurrentMoment = true;
                    PlayerMoving.CurrentActionPos = closestThing.Cords;
                    
                    closestThing.IsCarried = true;
                    Player.IsCarrying = true;

                    if (closestThing is Seed || closestThing is Fruit)
                    {
                        var seedbed = Objects.Seedbeds.FirstOrDefault(x =>
                            x.Value.IsBusy && SquareSection.ConvertSectionToVector(x.Key) ==
                            closestThing.Cords);

                        if (closestThing is AppleTreeSeed seed2)
                        {
                            var seedbeds = seed2.Seedbeds;
                            if (seedbeds != null)
                            {
                                if (closestThing is Seed seed) seed.Seedbeds = null;
                                foreach (var seedbed1 in seedbeds)
                                {
                                    seedbed1.IsBusy = false;
                                }
                            }
                        }

                        if (seedbed.Value != null)
                        {
                            if (closestThing is Seed seed) seed.Seedbed = null;
                            if (closestThing is Apple)
                            {
                                seedbed.Value.IsBusy = true;
                            }
                            else
                            {
                                seedbed.Value.IsBusy = false;
                            }
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
                                    case Apple:
                                        table.FruitsCount["apple"]--;
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
    }

    private void LateUpdate()
    {
        var things = Objects.Things;
        
        if (PlayerMoving.Direction != Vector2.zero)
            direction = PlayerMoving.Direction;

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