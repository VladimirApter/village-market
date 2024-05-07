using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public Text myText1;
    public Text myText2;

    private string textToDisplay1 = "Правая кнопка мыши - брать предметы\n" +
                                    "Левая кнопка мыши - действие предметом в руке";

    private string textToDisplay3 = "Возьмите мотыгу и вскопайте грядку";
    private string textToDisplay5 = "Возьмите и посадите семечко";
    private string textToDisplay7 = "Возьмите лейку и полейте грядку";
    private string textToDisplay9 = "Подождите, когда вырастет семечко";
    private string textToDisplay11 = "Возьмите и положите плод на стол";
    private string textToDisplay12 = "Поздравляем! Вы прошли обучение";


    void Start()
    {
        myText1.text = textToDisplay1;
        myText2.text = textToDisplay3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Objects.Seedbeds.Count > 0) myText2.text = textToDisplay5;
        if (Objects.Seedbeds.Any(x => x.Value.IsBusy)) myText2.text = textToDisplay7;
        if (Objects.Seedbeds.Any(x => x.Value.IsBusy) && Objects.Seedbeds.Any(x => x.Value.IsPoured)) myText2.text = textToDisplay9;
        if (Objects.Things.Any(x => x is Fruit)) myText2.text = textToDisplay11;
        if (Objects.Tables.Any(x => x.Value.IsBusy)) myText2.text = textToDisplay12;
    }
}