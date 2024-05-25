using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    private Dictionary<string, int> playersScores = new();
    public Text playerText;
    private int k;
    // Start is called before the first frame update
    void Start()
    {
        playersScores = new Dictionary<string, int>();
        foreach (var playerName in PlayerPrefs.GetString("existingPlayers")
                     .Split('|')
                     .Where(s => s != ""))
        {
            playersScores[playerName] = PlayerPrefs.GetInt(playerName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (k == playersScores.Count) return;
        var strBuilder = new StringBuilder();
        foreach (var playerScore in playersScores)
        {
            strBuilder.Append($"{playerScore.Key} - {playerScore.Value}\n");
            playerText.text = strBuilder.ToString();
            k++;
        }
        
    }
}
