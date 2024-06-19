using System.Collections;
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

        var seedBeds = Objects.Seedbeds;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var seedbedCoordinates =
            SquareSection.ConvertVectorToSection(mousePosition +
                                                 new Vector3(0, SquareSection.SquareSectionScale.y / 2));

        if (!SquareSection.GetCurrentSectionCoordinates().Contains(seedbedCoordinates) ||
            Vector2.Distance(SquareSection.ConvertSectionToVector(seedbedCoordinates), mousePosition) >
            new Vector2(SquareSection.SquareSectionScale.x, SquareSection.SquareSectionScale.y)
                .magnitude || !seedBeds.Keys.Contains(seedbedCoordinates)) return;

        var coordsSeedBed = seedBeds[seedbedCoordinates].Coords;

        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K)) && !seedBeds[seedbedCoordinates].IsBusy)
        {
            PlayerMoving.IsActionAtCurrentMoment = true;
            PlayerMoving.CurrentActionPos = coordsSeedBed;
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

                    UpdateSeedPosition(seed, coordSeedBedMin + new Vector2(SquareSection.SquareSectionScale.x/2, SquareSection.SquareSectionScale.y/2));

                    var seedBedsForSeed = validPositions//.OrderBy(vec => vec.x).ThenBy(vec => vec.y)
                        .Select(x => Objects.Seedbeds[SquareSection.ConvertVectorToSection(x)])
                        .Take(4)
                        .ToArray();
                    seed.Seedbeds = seedBedsForSeed;
                    foreach (var sb in seedBedsForSeed) sb.IsBusy = true;

                    Player.IsCarrying = false;
                }

                return;
            }
            UpdateSeedPosition(seed, coordsSeedBed);
            seed.Seedbed = seedBeds[seedbedCoordinates];
            seedBeds[seedbedCoordinates].IsBusy = true;
            Player.IsCarrying = false;
            
            StartCoroutine(WaitAndCanCreate(seed));
        }
    }
    public static IEnumerator WaitAndCanCreate(Seed seed)
    {
        seed.CanCarried = false;
        yield return new WaitForSeconds(0.1f);
        seed.CanCarried = true;
    }
    private bool IsSeedbedBusy(Vector2 baseCoord, Vector2 offset) =>
        Objects.Seedbeds.ContainsKey(SquareSection.ConvertVectorToSection(baseCoord + offset)) &&
        !Objects.Seedbeds[SquareSection.ConvertVectorToSection(baseCoord + offset)].IsBusy;

    private void UpdateSeedPosition(Seed seed, Vector2 position)
    {
        seed.ThingObj.transform.position = position;
        seed.IsCarried = false;

        var spriteRenderer = seed.ThingObj.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "things";
        spriteRenderer.sortingOrder = 0;
    }
}