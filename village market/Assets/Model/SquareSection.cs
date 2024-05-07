using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model
{

    public class SquareSection
    {
        public bool IsBusy { get; set; }
        public UnityEngine.Vector2 Coords { get; set; }
        public static (int, int) GetCurrentSectionCoordinates()
        {
            var playerPos = Player.PlayerObj.transform.position;
            var seedbedScale = Seedbed.SeedbedPrefab.transform.localScale;

            var x = Math.Max((int)(Math.Abs(playerPos.x) / seedbedScale.x) + 1, 1) * Math.Sign(playerPos.x);
            var y = Math.Max((int)(Math.Abs(playerPos.y) / seedbedScale.y) + 1, 1) * Math.Sign(playerPos.y);
            return (x, y);
        }

        public static Vector2 ConvertSectionToVector((int, int) coordinates)
        {
            var seedbedScale = Seedbed.SeedbedPrefab.transform.localScale;
            var xSign = Math.Sign(coordinates.Item1);
            var ySign = Math.Sign(coordinates.Item2);
            return new Vector2((coordinates.Item1 - 1 * xSign) * seedbedScale.x + seedbedScale.x / 2 * xSign,
                (coordinates.Item2 - 1 * ySign) * seedbedScale.y + seedbedScale.y / 2 * ySign);
        }
    }
}