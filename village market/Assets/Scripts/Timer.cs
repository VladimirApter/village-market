
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Model;
using UnityEngine.SceneManagement;

public class Timer: Sounds
{
    // Start is called before the first frame update

    public float timeStart;
    public Text timerText;
    private int counter = 0;
    private int int_time = 0;
    
    void Start()
    {
        timerText.text = timeStart.ToString();
        timeStart = Player.GameTime;
    }
        

    // Update is called once per frame
    void Update()
    {
        if (counter == 1) Play(sounds[0], loopFlag: true);
        //Debug.LogError("timer");
        if (timeStart > 0)
        {
            if (timeStart <= 30)
            {
                timerText.color = Color.red;
            }
            
            timeStart -= Time.deltaTime;
        }
        else if (timeStart < 0)
        {
            timeStart = 0;
            //game over
            if (Math.Abs(Player.GameTime - Init.ShortGameTime) < 1e-3)
                PlayerPrefs.SetInt($"{Player.Name}:3min", Player.TotalScore);
            if (Math.Abs(Player.GameTime - Init.LongGameTime) < 1e-3)
                PlayerPrefs.SetInt($"{Player.Name}:5min", Player.TotalScore);
            SceneManager.LoadScene("LeaderBoardScene");
        }
        
        int minutes = Mathf.FloorToInt(timeStart / 60);
        int seconds = Mathf.FloorToInt(timeStart % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        int_time = minutes * 60 + seconds;
        if (int_time <= 30) {counter++;}
    }
}