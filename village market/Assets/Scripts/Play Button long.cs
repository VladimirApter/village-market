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
        
        var existingPlayers = PlayerPrefs.GetString("existingPlayers");
        if (!existingPlayers.Contains(playerName))
        {
            PlayerPrefs.SetString("existingPlayers", $"{existingPlayers}|{playerName}" );
        }
        
        SceneManager.LoadScene("GameScene");
        Player.GameTime = 60;
        Player.Name = inputField.text;
    }
}
