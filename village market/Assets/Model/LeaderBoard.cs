using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public Dictionary<string, int> PlayersScores = new();
    public Text playerText;
    private int k;
    // Start is called before the first frame update
    void Start()
    {
        PlayersScores = new Dictionary<string, int> { { "Yura", 12 }, {"Liza", 1}};
    }

    // Update is called once per frame
    void Update()
    {
        
        if (k == PlayersScores.Count) return;
        var strBuilder = new StringBuilder();
        foreach (var playerScore in PlayersScores)
        {
            strBuilder.Append($"{playerScore.Key} - {playerScore.Value}\n");
            playerText.text = strBuilder.ToString();
            k++;
        }
        
    }
}
