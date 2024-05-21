using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                seedbed.Value.DestroyFramesCount++;
            }

            if (seedbed.Value.DestroyFramesCount >= Seedbed.FramesToDestroy)
            {
                var seed = (Seed)Objects.Things.FirstOrDefault(x => x is Seed { Seedbed: not null } seed && seed.Seedbed.Coords == seedbed.Value.Coords);
                var seedAppleTree = (Seed)Objects.Things.FirstOrDefault(x => x is Seed { Seedbeds: not null } seedApple && seedApple.Seedbeds.Any(seedbedApple => seedbedApple.Coords == seedbed.Value.Coords));
                if (seed != null)
                {
                    seed.Seedbed = null;
                }

                if (seedAppleTree != null)
                {
                    foreach (var seedbed1 in seedAppleTree.Seedbeds)
                    {
                        seedbed1.IsBusy = false;
                    }
                    seedAppleTree.Seedbeds = null;
                }
                
                Destroy(seedbed.Value.SeedbedObj);
                Objects.Seedbeds.Remove(seedbed.Key);
                return;
            }
        }
    }
}
