using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Model;
using TMPro;


public class PlayerInstructionController : Sounds
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

    private float waitTime = 300f;
    private float timer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        isTutorialFinished = false;
        instructionImage.sprite = actionSprite0;
    }

    // Update is called once per frame
    void Update()
    {
        var playerPosition = Player.PlayerObj.transform.position;
        textImage.transform.position = playerPosition + new Vector3(0, 10, 0);
        instructionImage.transform.position = playerPosition + new Vector3(0, 13f, 0);

        if (DestroySeedbed.IsBroken)
        {
            instructionText.text = "Ой, ты затоптал грядку! Теперь придется заново ее вскапывать.";
            timer = waitTime;
            DestroySeedbed.IsBroken = false;
        }
        if (timer > 0)
        {
            timer--;
            return;
        }
        
        if (Objects.Seedbeds.Count == 0)
        {
            instructionText.text = "Возьми мотыгу и вскопай грядку";
            instructionImage.sprite = actionSprite0;
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

        var isWheatOnTable = Objects.Tables.Values.Any(table => table.Fruits.Any(x => x is Wheat));

        if (isWheatOnTable)
        {
            instructionText.text = "Теперь посадим яблоню. Вскопай 4 грядки рядом";
            instructionImage.sprite = actionSprite5;
        }

        if (Objects.Seedbeds.Count >= 4 && isWheatOnTable)
        {
            instructionText.text = "Возьми семечко яблони и посади его";
            instructionImage.sprite = actionSprite6;
        }

        if (Objects.Things.Any(x => x is AppleTreeSeed { IsPlantedOnSeedbeds: true }) && isWheatOnTable)
        {
            instructionText.text = "Возьми лейку и полей все грядки";
            instructionImage.sprite = actionSprite7;
        }

        if (Objects.Things.Any(x => x is AppleTreeSeed { GrowingFramesCount: >= 1 }) && isWheatOnTable)
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
            instructionText.text = "Поздравляем, ты прошел обучение! Свекла растет как пшеница";
            instructionImage.sprite = actionSprite12;
            Play(sounds[0], volume: 0.3f);
        }
    }
}