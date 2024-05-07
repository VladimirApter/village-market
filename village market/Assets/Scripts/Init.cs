using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Init : MonoBehaviour
{
    public GameObject Player;
    public GameObject Platform;
    public GameObject SeedPrefab;
    public GameObject LeicaPrefab;
    public GameObject HoePrefab;
    public GameObject SeedbedPrefab;
    public GameObject FruitPrefab;
    public GameObject TablePrefab;
    public GameObject RequestPrefab;
    public GameObject BeetSeedPrefab;
    public GameObject BeetPrefab;
    public GameObject WheatSeedPrefab;
    public GameObject WheatPrefab;
    
    // Start is called before the first frame update
    void Awake()
    {
        Model.Player.PlayerObj = Player;
        Model.Platform.PlatformObj = Platform;
        Model.Seed.SeedPrefab = SeedPrefab;
        Model.Leica.LeicaPrefab = LeicaPrefab;
        Model.Hoe.HoePrefab = HoePrefab;
        Model.Seedbed.SeedbedPrefab = SeedbedPrefab;
        Model.Fruit.FruitPrefab = FruitPrefab;
        Model.Table.TablePrefab = TablePrefab;
        Model.Request.RequestPrefab = RequestPrefab;
        Model.BeetSeed.BeetSeedPrefab = BeetSeedPrefab;
        Model.Beet.BeetPrefab = BeetPrefab;
        Model.WheatSeed.WheatSeedPrefab = WheatSeedPrefab;
        Model.Wheat.WheatPrefab = WheatPrefab;
    }
}
