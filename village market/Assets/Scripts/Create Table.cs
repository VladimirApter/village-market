using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class CreateTable : MonoBehaviour
{
    public GameObject tableObjs;
    private readonly (int, int)[] coordsTable = new[] {(7, 4), (7, 2), (7, -1), (7, -3), (7, -5) };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var partTable in coordsTable)
        {
            if (!Objects.Tables.ContainsKey(partTable))
            {
                var table = new Table()
                {
                    Coords = SquareSection.ConvertSectionToVector(partTable),
                    TableObj = Instantiate(Table.TablePrefab,
                        SquareSection.ConvertSectionToVector(partTable),
                        Quaternion.identity, tableObjs.transform),
                    IsBusy = false
                };
                Objects.Tables.Add(partTable, table);
            }
            
        }
    }
}
