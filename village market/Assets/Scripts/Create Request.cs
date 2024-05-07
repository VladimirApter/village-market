using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using UnityEngine;

public class CreateRequest : MonoBehaviour
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
            if (!Objects.Requests.ContainsKey(coordRequest))
            {
                var rnd = new System.Random();
                var request = new Request()
                {
                    RequestObj = Instantiate(Request.RequestPrefab,
                        SquareSection.ConvertSectionToVector(coordRequest),
                        Quaternion.identity, requestObjs.transform)
                };
                request.FruitsCount["fruit"] = rnd.Next(1, 6);
                for (int i = 0; i < request.FruitsCount["fruit"]; i++)
                {
                    var fruit = Instantiate(Fruit.FruitPrefab,
                        SquareSection.ConvertSectionToVector(coordRequest) + new Vector2((i - 2) * 2, 0),
                        Quaternion.identity, requestFruits.transform);
                    request.Fruits.Add(fruit);
                }

                Objects.Requests.Add(coordRequest, request);
            }
        }

        foreach (var requestCoords in Objects.Requests.Keys)
        {
            var request = Objects.Requests[requestCoords];
            var table = Objects.Tables[(requestCoords.Item1 - 2, requestCoords.Item2)];

            var isRequestCompleted = true; 
            foreach (var fruit in request.FruitsCount.Keys)
                if (request.FruitsCount[fruit] != table.FruitsCount[fruit])
                    isRequestCompleted = false;
            if (!isRequestCompleted) 
                continue;

            foreach (var fruit in table.Fruits)
            {
                Objects.Fruits.Remove(fruit);
                Objects.Things.Remove(fruit);
                Destroy(fruit.ThingObj);
            }
            table.Fruits.Clear();
            table.FruitsCount = new Dictionary<string, int> { { "fruit", 0 } };

            foreach (var fruit in request.Fruits)
                Destroy(fruit);
            Objects.Requests.Remove(requestCoords);
            Destroy(request.RequestObj);
            break;
        }
    }
}