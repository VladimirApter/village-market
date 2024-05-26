using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

public class CreateSeedbeds : MonoBehaviour
{
    public GameObject seedbedObjs;
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
        if (hoe is null || !hoe.IsCarried) return;
        if (!(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K)) || !Player.IsCarrying) return;
        if (seedbedCoordinates.Item1 < 1 || seedbedCoordinates.Item1 > 4 || seedbedCoordinates.Item2 < -5 || seedbedCoordinates.Item2 > 4) return;
        
        if (!SquareSection.GetCurrentSectionCoordinates().Contains(seedbedCoordinates) ||
            Vector2.Distance(SquareSection.ConvertSectionToVector(seedbedCoordinates), mousePosition) >
            new Vector2(SquareSection.SquareSectionScale.x, SquareSection.SquareSectionScale.y)
                .magnitude) return;

        PlayerMoving.IsActionAtCurrentMoment = true;
        PlayerMoving.CurrentActionPos = SquareSection.ConvertSectionToVector(seedbedCoordinates);

        if (Objects.Seedbeds.Keys.Contains(seedbedCoordinates))
        {
            Destroy(Objects.Seedbeds[seedbedCoordinates].SeedbedObj);
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
        };
        StartCoroutine(newSeedbed.WaitAndCanDestroy());
        Objects.Seedbeds.Add(seedbedCoordinates, newSeedbed);
    }

    private Hoe GetHoe(List<Instrument> instruments)
    {
        return instruments.OfType<Hoe>().FirstOrDefault();
    }
}