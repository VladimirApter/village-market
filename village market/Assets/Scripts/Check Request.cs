using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using Unity.VisualScripting;
using UnityEngine;

public class CheckRequest : Sounds
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var requestCoords in Objects.Requests.Keys)
        {
            var request = Objects.Requests[requestCoords];
            var table = Objects.Tables[(requestCoords.Item1 - 3, requestCoords.Item2)];


            foreach (var fruit in request.Fruits)
                fruit.ThingObj.GetComponent<SpriteRenderer>().color = Color.white;
                
            foreach (var (fruitName, fruitCount) in table.FruitsCount)
            {
                var i = fruitCount;
                foreach (var fruit in request.Fruits)
                {
                    if (i <= 0) break;
                    
                    if ((fruitName == "wheat" && fruit is Wheat) ||
                        (fruitName == "beet" && fruit is Beet) ||
                        (fruitName == "apple" && fruit is Apple))
                    {
                        fruit.ThingObj.GetComponent<SpriteRenderer>().color = new Color(0.25f, 0.25f, 0.25f);
                        i--;
                    }
                }
            }

            var isRequestCompleted = true; 
            foreach (var fruit in request.FruitsCount.Keys)
            {
                var tableFruitCount = table.FruitsCount[fruit];
                if (request.FruitsCount[fruit] > tableFruitCount)
                    isRequestCompleted = false;
            }
            if (!isRequestCompleted) 
                continue;
            Player.TotalScore += request.Price;
            PlayerInstructionController.isTutorialFinished = true;
            Play(sounds[0]);

            var fruitsToDestroy = new List<Fruit>();

            foreach (var (fruitName, destroyCount) in request.FruitsCount)
            {
                for (var i = 0; i < destroyCount; i++)
                {
                    foreach (var fruit in table.Fruits)
                    {
                        if (fruitsToDestroy.Contains(fruit)) continue;
                        
                        var isFruitDetected = false;
                        if ((fruitName == "beet" && fruit is Beet) || (fruitName == "wheat" && fruit is Wheat) ||
                            (fruitName == "apple" && fruit is Apple) || fruitName == "fruit")
                        {
                            table.FruitsCount[fruitName]--;
                            fruitsToDestroy.Add(fruit);
                            isFruitDetected = true;
                        }

                        if (isFruitDetected) break;
                    }
                }
            }
            foreach (var fruit in fruitsToDestroy)
            {
                table.Fruits.Remove(fruit);
                Objects.Fruits.Remove(fruit);
                Objects.Things.Remove(fruit);
                Destroy(fruit.ThingObj);
            }
            
            foreach (var fruit in request.Fruits)
                Destroy(fruit.ThingObj);
            Objects.Requests.Remove(requestCoords);
            Destroy(request.RequestObj);
            break;
        }
    }
}
