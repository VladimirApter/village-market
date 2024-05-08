using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using UnityEngine;

public class CreateRequests : MonoBehaviour
{
    public GameObject requestObjs;
    public GameObject requestFruits;

    private readonly (int, int)[] coordsRequests = new[] { (10, -1), (10, 2), (10, 4), (10, -3), (10, -5) };

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
            var request = new Request()
            {
                RequestObj = Instantiate(Request.RequestPrefab,
                    SquareSection.ConvertSectionToVector(coordRequest),
                    Quaternion.identity, requestObjs.transform)
            };
            var fruitsCount = rnd.Next(1, 6);
            var wheatCount = rnd.Next(0, fruitsCount);
            var beetsCount = fruitsCount - wheatCount;

            request.FruitsCount["wheat"] = wheatCount;
            
            for (var i = 0; i < wheatCount; i++)
            {
                var wheat = Instantiate(Wheat.WheatPrefab,
                    SquareSection.ConvertSectionToVector(coordRequest) + new Vector2((i - 2) * 2, 0),
                    Quaternion.identity, requestFruits.transform);
                request.Fruits.Add(wheat);
            }

            request.FruitsCount["beet"] = beetsCount;
            for (var i = 0; i < beetsCount; i++)
            {
                var beet = Instantiate(Beet.BeetPrefab,
                    SquareSection.ConvertSectionToVector(coordRequest) + new Vector2((wheatCount + i - 2) * 2, 0),
                    Quaternion.identity, requestFruits.transform);
                request.Fruits.Add(beet);
            }

            Objects.Requests.Add(coordRequest, request);
        }
    }
}