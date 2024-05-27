using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

public class PutOnTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var things = Objects.Things;
        var fruit = (Fruit)things.FirstOrDefault(x => x.IsCarried && x is Fruit);

        if (fruit == null) return;
        var fruitCoords = (Vector2)fruit.ThingObj.transform.position;

        var tables = Objects.Tables;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var tableCoordinates =
            SquareSection.ConvertVectorToSection(mousePosition +
                                                 new Vector3(0, SquareSection.SquareSectionScale.y / 2));

        if (!SquareSection.GetCurrentSectionCoordinates().Contains(tableCoordinates) ||
            Vector2.Distance(SquareSection.ConvertSectionToVector(tableCoordinates), mousePosition) >
            new Vector2(SquareSection.SquareSectionScale.x, SquareSection.SquareSectionScale.y)
                .magnitude || !tables.Keys.Contains(tableCoordinates)) return;


        var coordsTable = tables[tableCoordinates].Coords;

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K))
        {
            PlayerMoving.IsActionAtCurrentMoment = true;
            PlayerMoving.CurrentActionPos = coordsTable;

            switch (fruit)
            {
                case Wheat:
                    tables[tableCoordinates].FruitsCount["wheat"]++;
                    break;
                case Beet:
                    tables[tableCoordinates].FruitsCount["beet"]++;
                    break;
                case Apple:
                    tables[tableCoordinates].FruitsCount["apple"]++;
                    break;
                default:
                    tables[tableCoordinates].FruitsCount["fruit"]++;
                    break;
            }

            Objects.Fruits.Add(fruit);

            fruit.ThingObj.transform.position = coordsTable;
            fruit.IsCarried = false;
            var spriteRenderer = fruit.ThingObj.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = "seedbeds";
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.flipX = false;

            //table.IsBusy = true;
            tables[tableCoordinates].Fruits.Add(fruit);
            Player.IsCarrying = false;
        }
    }
}