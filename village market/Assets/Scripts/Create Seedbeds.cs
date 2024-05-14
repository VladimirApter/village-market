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
        var hoe = GetHoe(Objects.Instruments);
        DestroySeedbed();
        if (hoe is null || !hoe.IsCarried) return;
        if (!(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K)) || !Player.IsCarrying) return;
        if (Player.PlayerObj.transform.position.x > 0) return;

        var seedbedCoordinates = SquareSection.GetCurrentSectionCoordinates();

        if (Objects.Tables.ContainsKey(seedbedCoordinates)) return;
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

    private void DestroySeedbed()
    {
        var playerPos = Player.PlayerObj.transform.position;
        foreach (var seedbed in Objects.Seedbeds)
        {
            var seedbedCoords = SquareSection.ConvertSectionToVector(seedbed.Key);
            if (Vector2.Distance(seedbedCoords + new Vector2(0, 1.5f), playerPos) <=
                new Vector2(SquareSection.SquareSectionScale.x / 2, SquareSection.SquareSectionScale.y / 2).magnitude &&
                seedbed.Value.CanDestroy)
            {
                Destroy(Objects.Seedbeds[seedbed.Key].SeedbedObj);
                Objects.Seedbeds.Remove(seedbed.Key);
                return;
            }
        }
    }

    private Hoe GetHoe(List<Instrument> instruments)
    {
        return instruments.OfType<Hoe>().FirstOrDefault();
    }
}