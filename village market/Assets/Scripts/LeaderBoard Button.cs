using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderBoardButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("LeaderBoardScene");
    }
}