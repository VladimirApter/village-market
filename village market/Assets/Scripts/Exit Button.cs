using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : Sounds
{
    public void ExitGame()
    {
        Play(sounds[0]);
        Application.Quit();
        Debug.Log("Exit is completed");
    }
}