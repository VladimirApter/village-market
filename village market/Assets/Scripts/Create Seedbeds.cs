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
        
        if (hoe is null || !hoe.IsCarried) return;
        if (!Input.GetKeyDown(KeyCode.Mouse0) || !Player.IsCarrying) return;
        
        var seedbedCoordinates = GetCurrentSeedbedCoordinates();
        if (Objects.Seedbeds.Keys.Contains(seedbedCoordinates))
        {
            var a = Objects.Seedbeds[seedbedCoordinates].SeedbedObj;
            Destroy(a);
            Objects.Seedbeds.Remove(seedbedCoordinates);
            return;
        }
        
        var newSeedbed = new Seedbed()
        {
            SeedbedObj = Instantiate(Seedbed.SeedbedPrefab,
                ConvertSeedbedCoordinatesToVector(seedbedCoordinates),
                Quaternion.identity, seedbedObjs.transform),
        };
        Objects.Seedbeds.Add(seedbedCoordinates, newSeedbed);
    }

    private Hoe GetHoe(List<Instrument> instruments)
    {
        return instruments.OfType<Hoe>().FirstOrDefault();
    }


    private (int, int) GetCurrentSeedbedCoordinates()
    {
        var playerPos = Player.PlayerObj.transform.position;
        var seedbedScale = Seedbed.SeedbedPrefab.transform.localScale;

        var x = Math.Max((int)(Math.Abs(playerPos.x) / seedbedScale.x) + 1, 1) * Math.Sign(playerPos.x);
        var y = Math.Max((int)(Math.Abs(playerPos.y) / seedbedScale.y) + 1, 1) * Math.Sign(playerPos.y);
        return (x, y);
    }

    private Vector2 ConvertSeedbedCoordinatesToVector((int, int) coordinates)
    {
        var seedbedScale = Seedbed.SeedbedPrefab.transform.localScale;
        var xSign = Math.Sign(coordinates.Item1);
        var ySign = Math.Sign(coordinates.Item2);
        return new Vector2((coordinates.Item1 - 1 * xSign) * seedbedScale.x + seedbedScale.x / 2 * xSign,
            (coordinates.Item2 - 1 * ySign) * seedbedScale.y + seedbedScale.y / 2 * ySign);
    }
}
