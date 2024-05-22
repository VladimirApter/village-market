
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}
