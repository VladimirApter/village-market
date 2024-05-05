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
        var tables = Objects.Table;
        var things = Objects.Things;
        var seedbedScale = Table.TablePrefab.transform.localScale;
        var product = (Fruit)things.FirstOrDefault(x => x.IsCarried && x is Fruit);

        if (product == null) return;
        var coordsThing = (Vector2)product.ThingObj.transform.position;

        foreach (var table in tables.Values)
        {
            var coordsTable = table.Coords;

            if (Vector2.Distance(coordsThing, coordsTable) <=
                new Vector2(seedbedScale.x / 2, seedbedScale.y / 2).magnitude &&
                Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (product is Fruit)
                {
                    table.CountProduct["fruit"]++;
                    Objects.Fruits.Add((coordsTable, product));
                }

                product.ThingObj.transform.position = coordsTable;
                product.Cords = coordsTable;
                product.IsCarried = false;

                table.IsBusy = true;
                Player.IsCarrying = false;
            }
        }
    }
}