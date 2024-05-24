using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UIElements.Slider;

public class CreateRequests : MonoBehaviour
{
    public GameObject requestObjs;
    public GameObject requestFruits;
    public GameObject destroyBars;

    private readonly (int, int)[] coordsRequests = new[] { (10, -1), (10, 2), (10, -3) };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var coordRequest in coordsRequests)
        {
            if (Objects.Requests.ContainsKey(coordRequest)) continue;
            
            var rnd = new System.Random();
            var maxFruitsCount = 6;
            var fruitsTypesCount = 3;
            
            var wheatCount = rnd.Next(0, maxFruitsCount / fruitsTypesCount + 1);
            var beetsCount = rnd.Next(0, maxFruitsCount / fruitsTypesCount + 1);
            var applesCount = rnd.Next(0, maxFruitsCount / fruitsTypesCount + 1);
            while (wheatCount + beetsCount + applesCount == 0)
            {
                var count = rnd.Next(1, maxFruitsCount / fruitsTypesCount + 1);
                if (wheatCount == 0)
                    wheatCount = count;
                else if (beetsCount == 0)
                    beetsCount = count;
                else if (applesCount == 0)
                    applesCount = count;
            }
            var totalFruitsCount = wheatCount + beetsCount + applesCount;
            
            var request = new Request
            {
                RequestObj = Instantiate(Request.RequestPrefab,
                    SquareSection.ConvertSectionToVector(coordRequest),
                    Quaternion.identity, requestObjs.transform),
                FruitsCount = { ["wheat"] = wheatCount, ["beet"] = beetsCount, ["apple"] = applesCount},
                Price = totalFruitsCount * 100,
                FramesToDestroy = 1000 * totalFruitsCount,
                DestroyBar = Instantiate(Request.DestroyBarPrefab,
                    SquareSection.ConvertSectionToVector(coordRequest) + new Vector2(8, 0),
                    Quaternion.identity, destroyBars.transform)
            };

            for (var i = 0; i < wheatCount; i++)
            {
                var wheat = Instantiate(Wheat.WheatPrefab,
                    SquareSection.ConvertSectionToVector(coordRequest) + new Vector2((i - 2) * 2, 0),
                    Quaternion.identity, requestFruits.transform);
                request.Fruits.Add(wheat);
            }

            for (var i = 0; i < beetsCount; i++)
            {
                var beet = Instantiate(Beet.BeetPrefab,
                    SquareSection.ConvertSectionToVector(coordRequest) + new Vector2((wheatCount + i - 2) * 2, 0),
                    Quaternion.identity, requestFruits.transform);
                request.Fruits.Add(beet);
            }
            
            for (var i = 0; i < applesCount; i++)
            {
                var apple = Instantiate(Apple.ApplePrefab,
                    SquareSection.ConvertSectionToVector(coordRequest) + new Vector2((wheatCount + beetsCount + i - 2) * 2, 0),
                    Quaternion.identity, requestFruits.transform);
                request.Fruits.Add(apple);
            }

            Objects.Requests.Add(coordRequest, request);
        }
    }
}