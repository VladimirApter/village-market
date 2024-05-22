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
    
    

    //private string spriteName = "";
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
        //Sprite loadedSprite = Resources.Load<Sprite>("Assets/Sprites/Objects/Basic_tools_and_meterials 1.png");
        //myImage2.sprite = loadedSprite;
    }

    // Update is called once per frame
    void Update()
    {
        myImage.transform.position = Player.PlayerObj.transform.position+new Vector3(0, 10, 0);
        myImage2.transform.position = Player.PlayerObj.transform.position+new Vector3(0, 20, 0);
        if (Objects.Seedbeds.Count > 0){ myText2.text = textToDisplay5;}
        if (Objects.Seedbeds.Any(x => x.Value.IsBusy)) myText2.text = textToDisplay7;
        if (Objects.Seedbeds.Any(x => x.Value.IsBusy) 
            && Objects.Seedbeds.Any(x => x.Value.IsPoured)) myText2.text = textToDisplay9;
        if (Objects.Things.Any(x => x is Fruit)) myText2.text = textToDisplay11;
        //Tables.Any(x => x.Value.Fruits.Count > 0)
        if (isTutorialFinished) myText2.text = textToDisplay12;
    }
}