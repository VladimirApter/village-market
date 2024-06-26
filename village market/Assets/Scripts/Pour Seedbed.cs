using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

public class PourSeedbed : Sounds
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var instruments = Objects.Instruments;
        var seedBeds = Objects.Seedbeds;
        var leica = instruments.FirstOrDefault(x => x is Leica);
        
        if (leica == null) return;
        
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var seedbedCoordinates =
            SquareSection.ConvertVectorToSection(mousePosition +
                                                 new Vector3(0, SquareSection.SquareSectionScale.y / 2));
        
        if (!SquareSection.GetCurrentSectionCoordinates().Contains(seedbedCoordinates) ||
            Vector2.Distance(SquareSection.ConvertSectionToVector(seedbedCoordinates), mousePosition) >
            new Vector2(SquareSection.SquareSectionScale.x, SquareSection.SquareSectionScale.y)
                .magnitude || !seedBeds.Keys.Contains(seedbedCoordinates)) return;

        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K)) &&
            !seedBeds[seedbedCoordinates].IsPoured && leica.IsCarried)
        {
            PlayerMoving.IsActionAtCurrentMoment = true;
            PlayerMoving.CurrentActionPos = SquareSection.ConvertSectionToVector(seedbedCoordinates);
            
            var seedbed = seedBeds[seedbedCoordinates];
            seedbed.IsPoured = true;
            Play(sounds[0], volume: 0.02f);
            seedbed.SeedbedObj.GetComponent<SpriteRenderer>().color = new Color(0.36f, 0.25f, 0.2f);
        }
    }
}