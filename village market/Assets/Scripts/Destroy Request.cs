using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

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
            if (request.DestroyBar != null)
            {
                var slider = request.DestroyBar.GetComponent<Slider>();
                slider.value = 1 - (float)request.DestroyFramesCount / request.FramesToDestroy;
            }
            
            if (request.DestroyFramesCount >= request.FramesToDestroy)
                requestsToDestroy[requestCoords] = request;
        }

        foreach (var (requestCoords, request) in requestsToDestroy)
        {
            Player.TotalScore -= (int)(request.Price * 0.5);
            foreach (var fruit in request.Fruits)
                Destroy(fruit);
            Objects.Requests.Remove(requestCoords);
            Destroy(request.RequestObj);
        }
        
    }
}
