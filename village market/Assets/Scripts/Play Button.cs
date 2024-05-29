using Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayButton : Sounds
{
    public InputField inputField;
    public void OnClick()
    {
        var playerName = inputField.text;
        if (playerName == "") return;
        Play(sounds[0]);
        
        var existingPlayers = PlayerPrefs.GetString("3minPlayers");
        if (!existingPlayers.Contains(playerName))
        {
            PlayerPrefs.SetString("3minPlayers", $"{existingPlayers}|{playerName}:3min" );
        }
        
        
        SceneManager.LoadScene("GameScene");
        Player.GameTime = 30;
        Player.Name = playerName;
    }
}
