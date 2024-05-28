using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderBoardButton : Sounds
{
    public void OnClick()
    {
        Play(sounds[0]);
        SceneManager.LoadScene("LeaderBoardScene");
    }
}