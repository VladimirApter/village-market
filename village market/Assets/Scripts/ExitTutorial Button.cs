using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTutorialButton : Sounds
{
    
    public void OnClick()
    {
        //
        Play(sounds[0]);
        SceneManager.LoadScene("MenuScene");
        
    }
}