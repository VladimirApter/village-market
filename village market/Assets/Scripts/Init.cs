using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    public GameObject Player;
    public GameObject Platform;
    public GameObject SeedPrefab;
    public GameObject LeicaPrefab;
    public GameObject HoePrefab;
    public GameObject SeedbedPrefab;
    
    // Start is called before the first frame update
    void Awake()
    {
        Model.Player.PlayerObj = Player;
        Model.Platform.PlatformObj = Platform;
        Model.Seed.SeedPrefab = SeedPrefab;
        Model.Leica.LeicaPrefab = LeicaPrefab;
        Model.Hoe.HoePrefab = HoePrefab;
        Model.Seedbed.SeedbedPrefab = SeedbedPrefab;
    }
}
