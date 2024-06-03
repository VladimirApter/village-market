using Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayButton : MonoBehaviour
{
    public InputField inputField;
    public void OnClick()
    {
        var playerName = inputField.text;
        if (playerName == "") return;
        
        var existingPlayers = PlayerPrefs.GetString("3minPlayers");
        if (!existingPlayers.Contains(playerName))
        {
            PlayerPrefs.SetString("3minPlayers", $"{existingPlayers}|{playerName}:3min" );
        }
        
        
        SceneManager.LoadScene("GameScene");
        Player.GameTime = Init.ShortGameTime;
        Player.Name = playerName;
    }
}
