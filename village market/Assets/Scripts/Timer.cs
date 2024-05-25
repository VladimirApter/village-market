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
    public Text timerText;
    
    void Start()
    {
        timerText.text = Player.CurrentTime.ToString();
    }
        

    // Update is called once per frame
    void Update()
    {
        if (Player.CurrentTime > 0)
        {
            Player.CurrentTime -= Time.deltaTime;
        }
        else if (Player.CurrentTime < 0)
        {
            PlayerPrefs.SetInt(Player.Name, Player.TotalScore);
            
            Player.CurrentTime = 0;
            timerText.color = Color.red;
            SceneManager.LoadScene("LeaderBoardScene");
        }

        var minutes = Mathf.FloorToInt(Player.CurrentTime / 60);
        var seconds = Mathf.FloorToInt(Player.CurrentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
