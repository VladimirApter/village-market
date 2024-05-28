using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class CreateLogTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var logTable = new LogTable
        {
            TableObj = Instantiate(LogTable.LogTablePrefab, SquareSection.ConvertSectionToVector((-1, -5)),
                Quaternion.identity),
            Coords = SquareSection.ConvertSectionToVector((-1, -5))
        };

        Objects.LogTable = logTable;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
