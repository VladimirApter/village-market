using UnityEngine;

namespace Model
{
    public static class Player
    {
        public static GameObject PlayerObj { get; set; }
        public static float Speed { get; set; } = 30;
        public static float RotationSpeed { get; set; } = 1;
        public static bool IsCarrying { get; set; } = false;
        public static float TakingRadius { get; set; } = SquareSection.SquareSectionScale.x * 1.5f;
        public static int TotalScore = 0;
        public static string Name { get; set; }
        public static float CurrentTime { get; set; }
        public static float GameTime { get; set; }
    }
}