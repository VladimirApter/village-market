using Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonLong : MonoBehaviour
{
    public InputField inputField;
    public void OnClick()
    {
        if (inputField.text == "") return;
        
        SceneManager.LoadScene("GameScene");
        Player.Time = 60;
        Player.Name = inputField.text;
    }
}
