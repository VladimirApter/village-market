using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public Text game3MinText;
    public Text game5MinText;
    
    // Start is called before the first frame update
    void Start()
    {
        var playersScores3Min = new Dictionary<string, int>();
        foreach (var playerName in PlayerPrefs.GetString("3minPlayers")
                     .Split('|')
                     .Where(s => s != "")
                     .Select(s => s.Split(':')[0]))
            playersScores3Min[playerName] = PlayerPrefs.GetInt($"{playerName}:3min");
        
        var playersScores5Min = new Dictionary<string, int>();
        foreach (var playerName in PlayerPrefs.GetString("5minPlayers")
                     .Split('|')
                     .Where(s => s != "")
                     .Select(s => s.Split(':')[0]))
            playersScores5Min[playerName] = PlayerPrefs.GetInt($"{playerName}:5min");

        var strBuilder = new StringBuilder();
        var i = 1;
        foreach (var (player, score) in playersScores3Min.OrderBy(x => -x.Value).Take(6))
        {
            strBuilder.Append($"{i}. {player}: {score}\n");
            i++;
        }
        game3MinText.text = strBuilder.ToString();
        
        strBuilder = new StringBuilder();
        i = 1;
        foreach (var (player, score) in playersScores5Min.OrderBy(x => -x.Value).Take(6))
        {
            strBuilder.Append($"{i}. {player}: {score}\n");
            i++;
        }
        game5MinText.text = strBuilder.ToString();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
