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
        var existingPlayers = PlayerPrefs.GetString("existingPlayers");
        if (!existingPlayers.Contains(playerName))
        {
            PlayerPrefs.SetString("existingPlayers", $"{existingPlayers}|{playerName}" );
        }
        
        
        SceneManager.LoadScene("GameScene");
        Player.GameTime = 30;
        Player.Name = playerName;
    }
}
