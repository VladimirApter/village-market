using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class DestroyRequest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var requestsToDestroy = new Dictionary<(int, int), Request>();
        foreach (var (requestCoords, request) in Objects.Requests)
        {
            request.DestroyFramesCount++;
            
            if (request.DestroyFramesCount >= request.FramesToDestroy)
                requestsToDestroy[requestCoords] = request;
        }

        foreach (var (requestCoords, request) in requestsToDestroy)
        {
            Player.TotalScore -= request.Price;
            foreach (var fruit in request.Fruits)
                Destroy(fruit);
            Objects.Requests.Remove(requestCoords);
            Destroy(request.RequestObj);
        }
        
    }
}
