using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public Image LeftClick;
    public Image RightClick;
    public Text myText2;
    public Image myImage;
    public Image myImage2;
    
    //public Image ImageTest;

    private string textToDisplay3 = "Возьмите мотыгу и вскопайте грядку";
    private string textToDisplay5 = "Возьмите и посадите семечко";
    private string textToDisplay7 = "Возьмите лейку и полейте грядку";
    private string textToDisplay9 = "Подождите, когда вырастет семечко";
    private string textToDisplay11 = "Возьмите и положите плод на стол";
    private string textToDisplay12 = "Поздравляем! Вы прошли обучение";
    public static bool isTutorialFinished;

    void Start()
    {
        myText2.text = textToDisplay3;
        isTutorialFinished = false;
        //LoadSprites(); // Вызываем метод для загрузки спрайтов
    }

    void LoadSprites()
    {
        Sprite hoeSprite = Resources.Load<Sprite>("Hoe.png");
        if (hoeSprite != null)
        {
            //ImageTest.sprite = hoeSprite; // Присваиваем спрайт ImageTest
        }
        else
        {
            Debug.LogError("Sprite 'Hoe' not found in Resources folder.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ImageTest.transform.position = new Vector3(0, 0, 0);
        myImage.transform.position = Player.PlayerObj.transform.position + new Vector3(0, 10, 0);
        myImage2.transform.position = Player.PlayerObj.transform.position + new Vector3(0, 20, 0);
        if (Objects.Seedbeds.Count > 0)
        {
            myText2.text = textToDisplay5;
            myImage2.sprite = Resources.Load<Sprite>("Seed.png");
        }

        if (Objects.Seedbeds.Any(x => x.Value.IsBusy))
        {
            myText2.text = textToDisplay7;
            myImage2.sprite = Resources.Load<Sprite>("Leica.png");
        }

        if (Objects.Seedbeds.Any(x => x.Value.IsBusy)
            && Objects.Seedbeds.Any(x => x.Value.IsPoured))
        {
            myText2.text = textToDisplay9;
        }
        if (Objects.Things.Any(x => x is Fruit)) myText2.text = textToDisplay11;
        //Tables.Any(x => x.Value.Fruits.Count > 0)
        if (isTutorialFinished) myText2.text = textToDisplay12;
    }
}