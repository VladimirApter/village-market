using Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public InputField inputField;
    public void OnClick()
    {
        if (inputField.text == "") return;
        
        SceneManager.LoadScene("GameScene");
        Player.Name = inputField.text;
    }
}
