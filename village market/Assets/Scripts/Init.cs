using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.Serialization;

public class Init : Sounds
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
    public GameObject DestroyBarPrefab;
    public GameObject ApplePrefab;
    public GameObject AppleTreeSeedPrefab;
    public GameObject AxePrefab;
    public GameObject LogPrefab;
    public GameObject LogTablePrefab;
    
    // Start is called before the first frame update
    void Awake()
    {
        Model.Player.PlayerObj = Player;
        Model.Platform.PlatformObj = Platform;
        Seed.SeedPrefab = SeedPrefab;
        Leica.LeicaPrefab = LeicaPrefab;
        Hoe.HoePrefab = HoePrefab;
        Seedbed.SeedbedPrefab = SeedbedPrefab;
        Fruit.FruitPrefab = FruitPrefab;
        Table.TablePrefab = TablePrefab;
        Request.RequestPrefab = RequestPrefab;
        BeetSeed.BeetSeedPrefab = BeetSeedPrefab;
        Beet.BeetPrefab = BeetPrefab;
        WheatSeed.WheatSeedPrefab = WheatSeedPrefab;
        Wheat.WheatPrefab = WheatPrefab;
        Request.DestroyBarPrefab = DestroyBarPrefab;
        Apple.ApplePrefab = ApplePrefab;
        AppleTreeSeed.AppleTreeSeedPrefab = AppleTreeSeedPrefab;
        Axe.AxePrefab = AxePrefab;
        Log.LogPrefab = LogPrefab;
        LogTable.LogTablePrefab = LogTablePrefab;

        Objects.Instruments = new List<Instrument>();
        Objects.Tables = new Dictionary<(int, int), Table>();
        Objects.Things = new List<Thing>();
        Objects.Seedbeds = new Dictionary<(int, int), Seedbed>();
        Objects.Fruits = new List<Fruit>();
        Objects.Requests = new Dictionary<(int, int), Request>();
        Model.Player.CurrentTime = Model.Player.GameTime;
        Model.Player.TotalScore = 0;
        Play(sounds[0]);
    }
}
