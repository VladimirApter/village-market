using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class DestroySeedbed : MonoBehaviour
{
    public static bool IsBroken;

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

            if (seedbed.Value.DestroyFramesCount == Seedbed.FramesToDestroy) IsBroken = true;
            if (seedbed.Value.DestroyBar != null)
            {
                Debug.Log("sdfsdfsdf");
                var spriteRenderer = seedbed.Value.DestroyBar.GetComponent<SpriteRenderer>();
                if (seedbed.Value.DestroyFramesCount == 0) seedbed.Value.DestroyBar.SetActive(false);
                else seedbed.Value.DestroyBar.SetActive(true);

                var slider = seedbed.Value.DestroyBar.GetComponent<Slider>();
                slider.value = 1 - (float)seedbed.Value.DestroyFramesCount / Seedbed.FramesToDestroy;
                slider.enabled = false;
            }

            if (seedbed.Value.DestroyFramesCount >= Seedbed.FramesToDestroy)
            {
                var seed = (Seed)Objects.Things.FirstOrDefault(x =>
                    x is Seed { Seedbed: not null } seed && seed.Seedbed.Coords == seedbed.Value.Coords);
                var seedAppleTree = (Seed)Objects.Things.FirstOrDefault(x =>
                    x is Seed { Seedbeds: not null } seedApple &&
                    seedApple.Seedbeds.Any(seedbedApple => seedbedApple.Coords == seedbed.Value.Coords));
                if (seed != null)
                    seed.Seedbed = null;

                if (seedAppleTree != null)
                {
                    if (seedAppleTree.GrowingFramesCount != seedAppleTree.FramesToGrow)
                    {
                        foreach (var seedbed1 in seedAppleTree.Seedbeds)
                            seedbed1.IsBusy = false;
                        seedAppleTree.Seedbeds = null;

                        seedAppleTree.ThingObj.transform.position =
                            SquareSection.ConvertSectionToVector(
                                SquareSection.ConvertVectorToSection(Player.PlayerObj.transform.position));
                    }
                    else
                        return;
                }

                Destroy(seedbed.Value.SeedbedObj);
                Destroy(seedbed.Value.DestroyBar);
                Objects.Seedbeds.Remove(seedbed.Key);
                return;
            }
        }
    }
}