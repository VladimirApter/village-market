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
        var seed = (Seed)Objects.Things.FirstOrDefault(x => x.IsCarried && x is Seed);
        if (seed == null) return;

        var coordsSeed = (Vector2)seed.ThingObj.transform.position;

        foreach (var seedBed in Objects.Seedbeds.Values)
        {
            var coordsSeedBed = seedBed.Coords;

            if (Vector2.Distance(coordsSeed, coordsSeedBed + new Vector2(0, 1.5f)) <=
                new Vector2(SquareSection.SquareSectionScale.x / 2, SquareSection.SquareSectionScale.y / 2).magnitude &&
                (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K)) && !seedBed.IsBusy)
            {
                if (seed is AppleTreeSeed)
                {
                    var directions = new Vector2[] { new(-4, 4), new(-4, -4), new(4, 4), new(4, -4) };

                    var validPositions = directions
                        .Where(direction =>
                            IsSeedbedBusy(coordsSeedBed, direction) &&
                            IsSeedbedBusy(coordsSeedBed, new Vector2(0, direction.y)) &&
                            IsSeedbedBusy(coordsSeedBed, new Vector2(direction.x, 0)))
                        .SelectMany(direction => new[]
                        {
                            coordsSeedBed, 
                            coordsSeedBed + direction, 
                            coordsSeedBed + new Vector2(0, direction.y), 
                            coordsSeedBed + new Vector2(direction.x, 0)
                        }).Distinct().ToList();

                    if (validPositions.Any())
                    {
                        var coordSeedBedMin = validPositions.OrderBy(vec => vec.x).ThenBy(vec => vec.y).First();

                        UpdateSeedPosition(seed, coordSeedBedMin + new Vector2(2.5f, 2.5f));

                        var seedBedsForSeed = validPositions.Select(x => Objects.Seedbeds[SquareSection.ConvertVectorToSection(x)]).ToArray();
                        seed.Seedbeds = seedBedsForSeed;
                        foreach (var sb in seedBedsForSeed) sb.IsBusy = true;

                        Player.IsCarrying = false;
                    }
                    return;
                }

                UpdateSeedPosition(seed, coordsSeedBed);
                seed.Seedbed = seedBed;
                seedBed.IsBusy = true;
                Player.IsCarrying = false;
            }
        }
    }

    private bool IsSeedbedBusy(Vector2 baseCoord, Vector2 offset) =>
        Objects.Seedbeds.ContainsKey(SquareSection.ConvertVectorToSection(baseCoord + offset)) &&
        !Objects.Seedbeds[SquareSection.ConvertVectorToSection(baseCoord + offset)].IsBusy;

    private void UpdateSeedPosition(Seed seed, Vector2 position)
    {
        seed.ThingObj.transform.position = position;
        seed.Cords = position;
        seed.IsCarried = false;

        var spriteRenderer = seed.ThingObj.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "things";
        spriteRenderer.sortingOrder = 0;
    }
}
