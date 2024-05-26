using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class CreateInstruments : MonoBehaviour
{
    public GameObject instrumentsObjs;
    private GameObject platform;
    
    // Start is called before the first frame update
    void Start()
    {
        platform = Platform.PlatformObj;
        var instruments = new List<Instrument>();
        
        instruments.Add(new Leica()
        {
            IsCarried = false,
            ThingObj = Instantiate(Leica.LeicaPrefab,SquareSection.ConvertSectionToVector((-3, 1)),
                Quaternion.identity, instrumentsObjs.transform),
        });
        
        instruments.Add(new Hoe()
        {
            IsCarried = false,
            ThingObj = Instantiate(Hoe.HoePrefab, SquareSection.ConvertSectionToVector((-3, -1)),
                Quaternion.identity, instrumentsObjs.transform),
        });
        
        instruments.Add(new Axe()
        {
            IsCarried = false,
            ThingObj = Instantiate(Axe.AxePrefab, SquareSection.ConvertSectionToVector((-3, -2)),
                Quaternion.identity, instrumentsObjs.transform),
        });

        Objects.Things.AddRange(instruments);
        Objects.Instruments.AddRange(instruments);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
