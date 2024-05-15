using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class DestroySeedbed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var playerPos = Player.PlayerObj.transform.position;
        foreach (var seedbed in Objects.Seedbeds)
        {
            var seedbedCoords = SquareSection.ConvertSectionToVector(seedbed.Key);
            if (Vector2.Distance(seedbedCoords + new Vector2(0, 1.5f), playerPos) <=
                new Vector2(SquareSection.SquareSectionScale.x / 2, SquareSection.SquareSectionScale.y / 2).magnitude &&
                seedbed.Value.CanDestroy)
            {
                Destroy(Objects.Seedbeds[seedbed.Key].SeedbedObj);
                Objects.Seedbeds.Remove(seedbed.Key);
                return;
            }
        }
    }
}
