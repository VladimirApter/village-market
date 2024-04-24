using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

public class ThingsCarrying : MonoBehaviour
{
    public GameObject player = Player.PlayerObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var things = Objects.Things;
        if (Input.GetKeyDown(KeyCode.Mouse0) && !Player.IsCarrying)
        {
            var minDistance = float.PositiveInfinity;
            var bestThing = things.FirstOrDefault();
            foreach (var thing in things)
            {
                var distance = Vector3.Distance(player.transform.position, thing.ThingObj.transform.position);
                if (!(distance < minDistance)) continue;
                minDistance = distance;
                bestThing = thing;
            }
            if (minDistance < Player.TakingRadius)
            {
                bestThing!.IsCarried = true;
                Player.IsCarrying = true;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)  && Player.IsCarrying)
        {
            Player.IsCarrying = false;
            foreach (var thing in things.Where(x => x.IsCarried))
                thing.IsCarried = false;
        }
        
        foreach (var thing in things.Where(x => x.IsCarried))
        {
            thing.ThingObj.transform.position = player.transform.position + (player.transform.rotation * new Vector3(2.2f, 0, 0));
        }
    }
}
