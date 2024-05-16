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
        Debug.Log(Player.TotalScore);
        foreach (var pair in Objects.Requests)
        {
            var requestCoords = pair.Key;
            var request = pair.Value;

            request.DestroyFramesCount++;
            
            if (request.DestroyFramesCount >= request.FramesToDestroy)
            {
                Player.TotalScore -= request.Price;
                foreach (var fruit in request.Fruits)
                    Destroy(fruit);
                Objects.Requests.Remove(requestCoords);
                Destroy(request.RequestObj);
            }
        }
    }
}
