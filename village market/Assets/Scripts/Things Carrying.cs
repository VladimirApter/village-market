using System.Linq;
using Model;
using UnityEngine;

public class ThingsCarrying : MonoBehaviour
{
    public GameObject player = Player.PlayerObj;
    private Vector2 direction;
    private float rotationDirection;

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

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if (direction.y != 0) rotationDirection = direction.y < 0 ? (float)Direction.Down : (float)Direction.Up;
        if (direction.x != 0) rotationDirection = direction.x < 0 ? (float)Direction.Left : (float)Direction.Right;

        foreach (var thing in things.Where(x => x.IsCarried))
        {
            thing.ThingObj.transform.eulerAngles = new Vector3(0, 0, rotationDirection);
            thing.ThingObj.transform.position =
                player.transform.position + (thing.ThingObj.transform.rotation * new Vector3(2.2f, 0, 0));
        }
    }

    private enum Direction
    {
        Up = 90,
        Down = -90,
        Left = -180,
        Right = 0
    }

    private float CalculateDistancePlayerToThing(Thing t)
    {
        return Vector3.Distance(player.transform.position, t.ThingObj.transform.position);
    }
}