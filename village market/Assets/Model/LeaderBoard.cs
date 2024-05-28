using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public Text playerText;
    
    // Start is called before the first frame update
    void Start()
    {
        var playersScores = new Dictionary<string, int>();
        foreach (var playerName in PlayerPrefs.GetString("existingPlayers")
                     .Split('|')
                     .Where(s => s != ""))
            playersScores[playerName] = PlayerPrefs.GetInt(playerName);

        var strBuilder = new StringBuilder();
        var i = 1;
        foreach (var (player, score) in playersScores.OrderBy(x => -x.Value).Take(6))
        {
            strBuilder.Append($"{i}. {player}: {score}\n");
            i++;
        }
        playerText.text = strBuilder.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
