using Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonLong : Sounds
{
    public InputField inputField;
    public void OnClick()
    {
        var playerName = inputField.text;
        if (playerName == "") return;
        Play(sounds[0]);
        
        var existingPlayers = PlayerPrefs.GetString("5minPlayers");
        if (!existingPlayers.Contains(playerName))
        {
            PlayerPrefs.SetString("5minPlayers", $"{existingPlayers}|{playerName}:5min" );
        }
        
        SceneManager.LoadScene("GameScene");
        Player.GameTime = Init.LongGameTime;
        Player.Name = inputField.text;
    }
}
