using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

public class PourSeedbed : MonoBehaviour
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
        var seedbedScale = Seedbed.SeedbedPrefab.transform.localScale;
        
        var leica = instruments.FirstOrDefault(x => x is Leica);
        if (leica == null) return;

        foreach (var coords in seedBeds.Keys)
        {
            var coordLeica = (Vector2)leica.ThingObj.transform.position;
            var cordSeedBed = SquareSection.ConvertSectionToVector(coords);

            if (Vector2.Distance(coordLeica, cordSeedBed) <= new Vector2(seedbedScale.x / 2, seedbedScale.y / 2).magnitude &&
                (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K)) && !seedBeds[coords].IsPoured && leica.IsCarried)
            {
                var seedbed = seedBeds[coords];
                seedbed.IsPoured = true;
                seedbed.SeedbedObj.GetComponent<SpriteRenderer>().color = new Color(0.36f, 0.25f, 0.2f);
            }
        }
    }
}
