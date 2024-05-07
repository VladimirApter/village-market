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

    private readonly (int, int)[] coordsRequests = new[] { (9, -1), (9, 2), (9, 4), (9, -3), (9, -5) };

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
            var fruitsCount = rnd.Next(1, 2);

            request.FruitsCount["fruit"] = fruitsCount;
            for (var i = 0; i < fruitsCount; i++)
            {
                var fruit = Instantiate(Fruit.FruitPrefab,
                    SquareSection.ConvertSectionToVector(coordRequest) + new Vector2((i - 2) * 2, 0),
                    Quaternion.identity, requestFruits.transform);
                request.Fruits.Add(fruit);
            }

            Objects.Requests.Add(coordRequest, request);
        }
    }
}
