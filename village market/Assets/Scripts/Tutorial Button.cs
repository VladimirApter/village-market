
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialButton : Sounds
{
    public void OnClick()
    {
        Play(sounds[0]);
        SceneManager.LoadScene("TutorialScene");
    }
}
