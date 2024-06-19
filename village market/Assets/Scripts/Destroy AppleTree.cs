using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

public class DestroyAppleTree : Sounds
{
    public GameObject logObjs;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var seedbedCoordinates =
            SquareSection.ConvertVectorToSection(mousePosition +
                                                 new Vector3(0, SquareSection.SquareSectionScale.y / 2));
        var axe = Objects.Instruments.FirstOrDefault(x => x.IsCarried && x is Axe);
        foreach (var appleTreeSeed in Objects.Things.OfType<AppleTreeSeed>())
        {
            var coordsAppleTree = appleTreeSeed.Cords;
            var seedb = appleTreeSeed.Seedbeds;
            if (seedb != null && seedb.Where(x => SquareSection.GetCurrentSectionCoordinates().Contains(SquareSection.ConvertVectorToSection(x.Coords)) && SquareSection.ConvertVectorToSection(x.Coords) == seedbedCoordinates).Any())
            {
                if (axe != null && Input.GetKeyDown(KeyCode.Mouse0) && appleTreeSeed.Seedbeds != null)
                {
                    Play(sounds[0], destroyed:true);
                    PlayerMoving.IsActionAtCurrentMoment = true;
                    PlayerMoving.CurrentActionPos = appleTreeSeed.Cords;
                    
                    foreach (var seedbed in appleTreeSeed.Seedbeds)
                    {
                        seedbed.CanDestroy = true;
                        seedbed.IsBusy = false;
                    }
                    Destroy(appleTreeSeed.ThingObj);
                    Objects.Things.Remove(appleTreeSeed);
                    
                    Objects.Things.Add(new Log
                    {
                        ThingObj = Instantiate(Log.LogPrefab, SquareSection.ConvertSectionToVector(seedbedCoordinates),
                            Quaternion.identity, logObjs.transform),
                    });
                    break;
                }
            } 
        }
    }
}