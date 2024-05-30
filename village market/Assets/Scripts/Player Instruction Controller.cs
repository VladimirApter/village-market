using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Model;
using TMPro;


public class PlayerInstructionController : MonoBehaviour
{
    public TMP_Text instructionText;
    public Image textImage;
    public Image instructionImage;
    
    public Sprite actionSprite0;
    public Sprite actionSprite1;
    public Sprite actionSprite2;
    public Sprite actionSprite3;
    public Sprite actionSprite4;
    public Sprite actionSprite5;
    public Sprite actionSprite6;
    public Sprite actionSprite7;
    public Sprite actionSprite8;
    public Sprite actionSprite9;
    public Sprite actionSprite10;
    public Sprite actionSprite11;
    public Sprite actionSprite12;
    
    
    public static bool isTutorialFinished;
    
    
    // Start is called before the first frame update
    void Start()
    {
        instructionText.text = "Возьми мотыгу и вскопай грядку";
        instructionImage.sprite = actionSprite0;
        isTutorialFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        var playerPosition = Player.PlayerObj.transform.position;
        textImage.transform.position = playerPosition + new Vector3(0, 12, 0);
        instructionImage.transform.position = playerPosition + new Vector3(0, 16, 0);
        if ("затоптал грядку" is null)
        {
            instructionText.text = "Ой, ты затоптал грядку! Теперь придется заново ее вскапывать.";
        }
        if (Objects.Seedbeds.Count > 0)
        {
            instructionText.text = "Возьми и посади семечко пшеницы на грядку";
            instructionImage.sprite = actionSprite1;
        }
        if (Objects.Seedbeds.Any(x => x.Value.IsBusy))
        {
            instructionText.text = "Возьми лейку и полей грядку";
            instructionImage.sprite = actionSprite2;
        }
        if (Objects.Seedbeds.Any(x => x.Value.IsBusy)
            && Objects.Seedbeds.Any(x => x.Value.IsPoured))
        {
            instructionText.text = "Подожди, когда вырастет пшеница";
            instructionImage.sprite = actionSprite3;
        }

        if (Objects.Things.Any(x => x is Wheat))
        {
            instructionText.text = "Возьми пшеницу с грядки и положи на стол для заказов";
            instructionImage.sprite = actionSprite4;
        }

        if ("пшеницу положили на стол" is null)
        {
            instructionText.text = "Теперь посадим яблоню. Вскопай 4 грядки";
            instructionImage.sprite = actionSprite5;
        }
        if ("вскопали 4 грядки и пшеница на столе" is null)
        {
            instructionText.text = "Возьми семечко яблони и посади его";
            instructionImage.sprite = actionSprite6;
        }
        if ("посадили яблоню и пшеница на столе" is null)
        {
            instructionText.text = "Возьми лейку и полей все грядки";
            instructionImage.sprite = actionSprite7;
        }
        if ("яблоня полита и пшеница на столе" is null)
        {
            instructionText.text = "Подожди, пока яблоня вырастет";
            instructionImage.sprite = actionSprite8;
        }
        
        if (Objects.Things.Any(x => x is Apple))
        {
            instructionText.text = "Возьми яблоко с грядки и положи на стол для заказов";
            instructionImage.sprite = actionSprite9;
        }
        if (isTutorialFinished)
        {
            instructionText.text = "Теперь возьми топор и сруби яблоню";
            instructionImage.sprite = actionSprite10;
        }

        if (Objects.Things.Any(x => x is Log))
        {
            instructionText.text = "Возьми бревно и положи на стол для бревен";
            instructionImage.sprite = actionSprite11;
        }
        if (Objects.Things.Any(x => x is Log && !x.CanCarried))
        {
            instructionText.text = "Поздравляем, ты прошел обучение!";
            instructionImage.sprite = actionSprite12;
        }
    }
}
