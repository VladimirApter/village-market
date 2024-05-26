using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMoving : MonoBehaviour
{
    public GameObject player = Player.PlayerObj;
    [SerializeField] public float speed;
    public Animator animator;
    public static Vector2 Direction;
    public static bool IsActionAtCurrentMoment;
    public static Vector2 CurrentActionPos;

    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");
    
    private const float MinXPosition = -14f;
    private const float MaxXPosition = 48f;
    private const float MinYPosition = -17.5f;
    private const float MaxYPosition = 17f;

    // Start is called before the first frame update
    void Start()
    {
        Direction = Vector2.down;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Direction.x = Input.GetAxisRaw("Horizontal");
        Direction.y = Input.GetAxisRaw("Vertical");

        if (IsActionAtCurrentMoment)
        {
            ChangeActionPlayerDirection(player.transform.position, CurrentActionPos);
        }
        
        if (Direction != Vector2.zero)
        {
            animator.SetFloat(Horizontal, Direction.x);
            animator.SetFloat(Vertical, Direction.y);
            animator.SetFloat(Speed, Direction.sqrMagnitude);
        }

        if (IsActionAtCurrentMoment)
        {
            IsActionAtCurrentMoment = false;
            return;
        }

        var position = player.transform.position;
        var movingDelta = new Vector3(Direction.x, Direction.y).normalized * (speed * Time.deltaTime);
        var newPosition = position + movingDelta;
        
        var sections = new[] { (-2, 5), (-2, 4),  (-3, 4), (-3, 3), (-4, 3), (-4, 2), (-4, 1), (-4, -1), (-4, -2), (-4, -3), (-4, -4), (-3, -4), (-3, -5), (-2, -5), (-2, -6), (-1, -5), (7, 2), (7, -1), (7, -3) };
        var square = SquareSection.SquareSectionScale;

        foreach (var section in sections)
        {
            var sectionCoords = SquareSection.ConvertSectionToVector(section);
            var halfSquare = square / 2.0f;

            var minX = sectionCoords.x - halfSquare.x;
            var maxX = sectionCoords.x + halfSquare.x;
            var minY = sectionCoords.y;
            var maxY = sectionCoords.y + 2*halfSquare.y;

            if (newPosition.x > minX && newPosition.x < maxX && newPosition.y > minY && newPosition.y < maxY)
            {
                return;
            }
            if (newPosition.x < MinXPosition)
            {
                newPosition.x = MinXPosition;
            }
            if (newPosition.y < MinYPosition )
            {
                newPosition.y = MinYPosition;
            }
            if (newPosition.y > MaxYPosition )
            {
                newPosition.y = MaxYPosition;
            }
            if (newPosition.x > MaxXPosition )
            {
                newPosition.x = MaxXPosition;
            }
        }
        
        player.transform.position = newPosition;
    }

    public static void ChangeActionPlayerDirection(Vector2 playerPos, Vector2 actionObjectPos)
    {
        var offset = actionObjectPos - playerPos;
        var result = Math.Abs(offset.x) > Math.Abs(offset.y) ? new Vector2(offset.x, 0) : new Vector2(0, offset.y);
        Direction = result.normalized;
    }
}