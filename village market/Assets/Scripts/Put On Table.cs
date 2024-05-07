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
        var tables = Objects.Tables;
        var things = Objects.Things;
        var seedbedScale = Table.TablePrefab.transform.localScale;
        var fruit = (Fruit)things.FirstOrDefault(x => x.IsCarried && x is Fruit);

        if (fruit == null) return;
        var fruitCoords = (Vector2)fruit.ThingObj.transform.position;

        foreach (var table in tables.Values)
        {
            var coordsTable = table.Coords;

            if (Vector2.Distance(fruitCoords, coordsTable) <=
                new Vector2(seedbedScale.x / 2, seedbedScale.y / 2).magnitude &&
                (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K)))
            {
                switch (fruit)
                {
                    case Wheat:
                        table.FruitsCount["wheat"]++;
                        break;
                    case Beet:
                        table.FruitsCount["beet"]++;
                        break;
                    default:
                        table.FruitsCount["fruit"]++;
                        break;
                }
                
                Objects.Fruits.Add(fruit);

                fruit.ThingObj.transform.position = coordsTable;
                fruit.Cords = coordsTable;
                fruit.IsCarried = false;

                //table.IsBusy = true;
                table.Fruits.Add(fruit);
                Player.IsCarrying = false;
            }
        }
    }
}