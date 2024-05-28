using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using UnityEngine;

public class CreateRequestsForTutorial : MonoBehaviour
{
    public GameObject requestObjs;
    public GameObject requestFruits;

    private readonly (int, int)[] coordsRequests = new[] { (10, -1) };

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
            
            var fruitsCount = 1;
            var request = new Request
            {
                RequestObj = Instantiate(Request.RequestPrefab,
                    SquareSection.ConvertSectionToVector(coordRequest),
                    Quaternion.identity, requestObjs.transform),
                FruitsCount = { ["wheat"] = fruitsCount, ["apple"] = fruitsCount },
                Price = fruitsCount * 100,
                FramesToDestroy = 10000000 * fruitsCount
            };

            for (var i = 0; i < fruitsCount; i++)
            {
                var wheat = Instantiate(Wheat.WheatPrefab,
                    SquareSection.ConvertSectionToVector(coordRequest) + new Vector2((i - 2) * 2, 0),
                    Quaternion.identity, requestFruits.transform);
                request.Fruits.Add(wheat);
            }
            for (var i = 0; i < fruitsCount; i++)
            {
                var apple = Instantiate(Apple.ApplePrefab,
                    SquareSection.ConvertSectionToVector(coordRequest) + new Vector2((1 + i - 2) * 2, 0),
                    Quaternion.identity, requestFruits.transform);
                request.Fruits.Add(apple);
            }

            Objects.Requests.Add(coordRequest, request);
        }
    }
}
