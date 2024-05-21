using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

public class DestroyAppleTree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var axe = Objects.Instruments.FirstOrDefault(x => x.IsCarried && x is Axe);
        foreach (var appleTreeSeed in Objects.Things.OfType<AppleTreeSeed>())
        {
            var coordsAppleTree = appleTreeSeed.Cords;
            if (Vector2.Distance(Player.PlayerObj.transform.position, coordsAppleTree + new Vector2(0, 1.5f)) <=
                new Vector2(SquareSection.SquareSectionScale.x, SquareSection.SquareSectionScale.y).magnitude)
            {
                if (axe != null && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K)))
                {
                    foreach (var seedbed in appleTreeSeed.Seedbeds)
                    {
                        seedbed.CanDestroy = true;
                    }
                    Destroy(appleTreeSeed.ThingObj);
                    Objects.Things.Remove(appleTreeSeed);
                    break;
                }
            } 
        }
    }
}