using System.Collections;
using System.Collections.Generic;
using System.Text;
using Model;
using UnityEngine;

public class CreateRequest : MonoBehaviour
{
    public GameObject requestObjs;

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
                        Quaternion.identity, requestObjs.transform),
                    CountFruits = rnd.Next(1, 3)
                };
                for (int i = 0; i < request.CountFruits; i++)
                {
                    Instantiate(Fruit.FruitPrefab,
                        SquareSection.ConvertSectionToVector(coordRequest) + new Vector2(i * 2, i * 2),
                        Quaternion.identity);
                }

                Objects.Requests.Add(coordRequest, request);
            }
        }


        foreach (var requestCoords in Objects.Requests.Keys)
        {
            foreach (var tableCoords in Objects.Table.Keys)
            {
                if (requestCoords.Item1 == tableCoords.Item1 + 2 && requestCoords.Item2 == tableCoords.Item2 &&
                    Objects.Requests[requestCoords].CountFruits == Objects.Table[tableCoords].CountProduct["fruit"])
                {
                    for (int i = 0; i < Objects.Requests[requestCoords].CountFruits; i++)
                    {
                        foreach (var fruit in Objects.Fruits)
                        {
                            if (Objects.Table[tableCoords].Coords == fruit.Item1)
                            {
                                foreach (var thing in Objects.Things)
                                {
                                    if (thing == fruit.Item2)
                                    {
                                        Objects.Things.Remove(thing);
                                        break;
                                    }
                                }

                                Destroy(fruit.Item2.ThingObj);
                                Objects.Fruits.Remove(fruit);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}