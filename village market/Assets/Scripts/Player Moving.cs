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
        
        if (Direction != new Vector2(0, 0))
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
        player.transform.position = position + movingDelta;
    }

    public static void ChangeActionPlayerDirection(Vector2 playerPos, Vector2 actionObjectPos)
    {
        var offset = actionObjectPos - playerPos;
        var result = Math.Abs(offset.x) > Math.Abs(offset.y) ? new Vector2(offset.x, 0) : new Vector2(0, offset.y);
        Direction = result.normalized;
    }
}