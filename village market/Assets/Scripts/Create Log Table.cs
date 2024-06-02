using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class CreateLogTable : MonoBehaviour
{
    public GameObject tableObjs;
    // Start is called before the first frame update
    void Start()
    {
        var logTable = new LogTable
        {
            TableObj = Instantiate(LogTable.LogTablePrefab, SquareSection.ConvertSectionToVector((-2, -4)),
                Quaternion.identity, tableObjs.transform),
            Coords = SquareSection.ConvertSectionToVector((-2, -4))
        };

        Objects.LogTable = logTable;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
