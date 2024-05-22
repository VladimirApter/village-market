using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update

    public float timeStart = 60;
    public Text timerText;
    
    void Start()
    {
        timerText.text = timeStart.ToString();
    }
        

    // Update is called once per frame
    void Update()
    {
        if (timeStart > 0)
        {
            timeStart -= Time.deltaTime;
        }
        else if (timeStart < 0)
        {
            timeStart = 0;
            timerText.color = Color.red;
            SceneManager.LoadScene("LeaderBoardScene");
        }

        int minutes = Mathf.FloorToInt(timeStart / 60);
        int seconds = Mathf.FloorToInt(timeStart % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
