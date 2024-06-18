using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

public class PutLogOnTable : Sounds
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var things = Objects.Things;
        var log = (Log)things.FirstOrDefault(x => x.IsCarried && x is Log);

        if (log == null) return;

        var logTable = Objects.LogTable;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var logTableCoordinates =
            SquareSection.ConvertVectorToSection(mousePosition +
                                                 new Vector3(0, SquareSection.SquareSectionScale.y / 2));

        if (!SquareSection.GetCurrentSectionCoordinates().Contains(logTableCoordinates) ||
            Vector2.Distance(SquareSection.ConvertSectionToVector(logTableCoordinates), mousePosition) >
            new Vector2(SquareSection.SquareSectionScale.x, SquareSection.SquareSectionScale.y)
                .magnitude || logTable.Coords != SquareSection.ConvertSectionToVector(logTableCoordinates)) return;


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerMoving.IsActionAtCurrentMoment = true;
            PlayerMoving.CurrentActionPos = logTable.Coords;

            Player.TotalScore += 50;
            Play(sounds[0]);

            log.ThingObj.transform.position = logTable.Coords;
            log.IsCarried = false;
            log.CanCarried = false;
            var spriteRenderer = log.ThingObj.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = "seedbeds";
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.flipX = false;

            Player.IsCarrying = false;
        }
    }
}
