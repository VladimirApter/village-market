
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer: MonoBehaviour
{
    // Start is called before the first frame update

    public float timeStart;
    public Text timerText;
    
    void Start()
    {
        timerText.text = timeStart.ToString();
        timeStart = Player.GameTime;
    }
        

    // Update is called once per frame
    void Update()
    {
        //Debug.LogError("timer");
        if (timeStart > 0)
        {
            timeStart -= Time.deltaTime;
        }
        else if (timeStart < 0)
        {
            timeStart = 0;
            //game over
            PlayerPrefs.SetInt(Player.Name, Player.TotalScore);
            SceneManager.LoadScene("LeaderBoardScene");

        }

        int minutes = Mathf.FloorToInt(timeStart / 60);
        int seconds = Mathf.FloorToInt(timeStart % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}