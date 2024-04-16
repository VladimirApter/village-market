using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    public GameObject Player;
    public GameObject Platform;
    public GameObject ThingPrefab;
    
    // Start is called before the first frame update
    void Awake()
    {
        Model.Player.PlayerObj = Player;
        Model.Platform.PlatformObj = Platform;
        Model.Thing.ThingPrefab = ThingPrefab;
    }
}
