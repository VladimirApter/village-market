using Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonLong : MonoBehaviour
{
    public InputField inputField;
    public void OnClick()
    {
        var playerName = inputField.text;
        if (playerName == "") return;
        
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
