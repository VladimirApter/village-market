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
        public static Vector3 SquareSectionScale = new(4, 4, 1);

        public static IEnumerable<(int, int)> GetCurrentSectionCoordinates()
        {
            var playerPos = Player.PlayerObj.transform.position;
            var playerPosFix = ConvertSectionToVector(ConvertVectorToSection(playerPos));

            for (int dx = -4; dx <= 4; dx += 4)
            {
                for (int dy = -4; dy <= 4; dy += 4)
                {
                    var a = playerPosFix + new Vector2(dx, dy);
                    yield return ConvertVectorToSection(a);
                }
            }
        }

        public static Vector2 ConvertSectionToVector((int, int) coordinates)
        {
            var xSign = Math.Sign(coordinates.Item1);
            var ySign = Math.Sign(coordinates.Item2);
            return new Vector2(
                (coordinates.Item1 - 1 * xSign) * SquareSectionScale.x + SquareSectionScale.x / 2 * xSign,
                (coordinates.Item2 - 1 * ySign) * SquareSectionScale.y + SquareSectionScale.y / 2 * ySign);
        }

        public static (int, int) ConvertVectorToSection(Vector2 vector)
        {
            var xSign = Math.Sign(vector.x);
            var ySign = Math.Sign(vector.y);

            var sectionX = (int)Math.Floor((vector.x - SquareSectionScale.x * xSign) / SquareSectionScale.x) +
                           xSign;
            var sectionY = (int)Math.Floor((vector.y - SquareSectionScale.y / 2 * ySign) / SquareSectionScale.y) +
                           ySign;
            if (sectionY == 0) sectionY = -1;

            return (sectionX, sectionY);
        }
    }
}