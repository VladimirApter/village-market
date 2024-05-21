using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class CheckRequest : MonoBehaviour
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

            var isRequestCompleted = true; 
            foreach (var fruit in request.FruitsCount.Keys)
            {
                var tableFruitCount = table.FruitsCount[fruit];
                if (request.FruitsCount[fruit] != tableFruitCount)
                    isRequestCompleted = false;
            }
            if (!isRequestCompleted) 
                continue;
            Player.TotalScore += request.Price;
            TutorialScript.isTutorialFinished = true;

            foreach (var fruit in table.Fruits)
            {
                Objects.Fruits.Remove(fruit);
                Objects.Things.Remove(fruit);
                Destroy(fruit.ThingObj);
            }
            table.Fruits.Clear();
            table.FruitsCount = new Dictionary<string, int> { { "beet", 0 }, { "wheat", 0 }, { "apple", 0 }, { "fruit", 0 } };

            foreach (var fruit in request.Fruits)
                Destroy(fruit);
            Objects.Requests.Remove(requestCoords);
            Destroy(request.RequestObj);
            break;
        }
    }
}
