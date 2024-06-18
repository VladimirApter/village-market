using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

public class CreateSeedbeds : Sounds
{
    public GameObject seedbedObjs;
    public GameObject destroyBars;
    private GameObject platform;
    
    // Start is called before the first frame update
    void Start()
    {
        platform = Platform.PlatformObj;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var seedbedCoordinates =
            SquareSection.ConvertVectorToSection(mousePosition +
                                                 new Vector3(0, SquareSection.SquareSectionScale.y / 2));
        var hoe = GetHoe(Objects.Instruments);
        var walkWay = new[] { (1, -1), (2, -1), (3, -1), (4, -1) };
        
        if (hoe is null || !hoe.IsCarried) return;
        if (!Input.GetKeyDown(KeyCode.Mouse0) || !Player.IsCarrying) return;
        if (seedbedCoordinates.Item1 < 1 || seedbedCoordinates.Item1 > 4 || seedbedCoordinates.Item2 < -5 || seedbedCoordinates.Item2 > 4 ||
        walkWay.Contains(seedbedCoordinates)) return;
        
        if (!SquareSection.GetCurrentSectionCoordinates().Contains(seedbedCoordinates) ||
            Vector2.Distance(SquareSection.ConvertSectionToVector(seedbedCoordinates), mousePosition) >
            new Vector2(SquareSection.SquareSectionScale.x, SquareSection.SquareSectionScale.y)
                .magnitude) return;

        PlayerMoving.IsActionAtCurrentMoment = true;
        PlayerMoving.CurrentActionPos = SquareSection.ConvertSectionToVector(seedbedCoordinates);

        if (!Seedbed.CanCreate) return;
        
        if (Objects.Seedbeds.Keys.Contains(seedbedCoordinates))
        {
            var seed = (Seed)Objects.Things.FirstOrDefault(x => x is Seed { Seedbed: not null } seed && seed.Seedbed.Coords == SquareSection.ConvertSectionToVector(seedbedCoordinates));
            var seedAppleTree = (Seed)Objects.Things.FirstOrDefault(x => x is Seed { Seedbeds: not null } seedApple && seedApple.Seedbeds.Any(seedbedApple => seedbedApple.Coords == SquareSection.ConvertSectionToVector(seedbedCoordinates)));
            if (seed != null)
                seed.Seedbed = null;

            if (seedAppleTree != null)
            {
                if (seedAppleTree.GrowingFramesCount != seedAppleTree.FramesToGrow)
                {
                    foreach (var seedbed1 in seedAppleTree.Seedbeds)
                        seedbed1.IsBusy = false;
                    seedAppleTree.Seedbeds = null;
                }
                else
                    return;
            }
            
            StartCoroutine(Seedbed.WaitAndCanCreate());
            
            Destroy(Objects.Seedbeds[seedbedCoordinates].SeedbedObj);
            Destroy(Objects.Seedbeds[seedbedCoordinates].DestroyBar);
            Objects.Seedbeds.Remove(seedbedCoordinates);
            return;
        }

        
        
        var newSeedbed = new Seedbed()
        {
            Coords = SquareSection.ConvertSectionToVector(seedbedCoordinates),
            SeedbedObj = Instantiate(Seedbed.SeedbedPrefab,
                SquareSection.ConvertSectionToVector(seedbedCoordinates),
                Quaternion.identity, seedbedObjs.transform),
            IsBusy = false,
            IsPoured = false,
            CanDestroy = false,
            DestroyBar = Instantiate(Request.DestroyBarPrefab,
                SquareSection.ConvertSectionToVector(seedbedCoordinates) + new Vector2(0, -1.5f),
                Quaternion.identity * Quaternion.Euler(0, 0, -90), destroyBars.transform)
        };
        newSeedbed.DestroyBar.transform.localScale = new Vector3(16, 4, 1);
        
        Play(sounds[0]);
        
        var spriteRenderer = newSeedbed.SeedbedObj.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "default";
        spriteRenderer.sortingOrder = 2;
        
        StartCoroutine(newSeedbed.WaitAndCanDestroy());
        Objects.Seedbeds.Add(seedbedCoordinates, newSeedbed);
    }

    private Hoe GetHoe(List<Instrument> instruments)
    {
        return instruments.OfType<Hoe>().FirstOrDefault();
    }
}