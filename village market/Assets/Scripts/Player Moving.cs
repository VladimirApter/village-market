using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Model;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMoving : MonoBehaviour
{
    public GameObject player = Player.PlayerObj;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var moveDirection = GetMoveDirection().normalized;
        if (moveDirection.x == 0 && moveDirection.y == 0) return;
        // var moveDirectionAngle = GetMoveDirectionAngle(moveDirection);
        // var playerAngle = player.transform.rotation.z;
        //
        // var rotationDirection = GetRotationDirection(moveDirectionAngle, playerAngle);
        //
        // switch (rotationDirection)
        // {
        //     case rotationDirection.none:
        //         player.transform.rotation = new Quaternion(0, 0, moveDirectionAngle, 0);
        //         break;
        //     case rotationDirection.right:
        //         player.transform.Rotate(new Vector3(0, 0, 1), -Player.RotationSpeed);
        //         break;
        //     case rotationDirection.left:
        //         player.transform.Rotate(new Vector3(0, 0, 1), Player.RotationSpeed);
        //         break;
        // }
        //
        // var resultRotation = new Quaternion(0, 0, player.transform.rotation.z, 0);
        //var resultMove = resultRotation * Vector2.right;
        var resultMove = moveDirection;

        player.transform.Translate(resultMove * (Player.Speed * Time.deltaTime));
    }

    private Vector2 GetMoveDirection()
    {
        var moveDirection = new Vector2(0, 0);
        
        if (Input.GetKey(KeyCode.W))
            moveDirection.y += 1;
        if (Input.GetKey(KeyCode.A))
            moveDirection.x += -1;
        if (Input.GetKey(KeyCode.S))
            moveDirection.y += -1;
        if (Input.GetKey(KeyCode.D))
            moveDirection.x += 1;

        return moveDirection;
    }

    private float GetMoveDirectionAngle(Vector2 moveDirection)
    {
        var x = moveDirection.x;
        var y = moveDirection.y;
        return x switch
        {
            0 => y switch
            {
                0 => 0,
                1 => 90,
                -1 => -90,
                _ => throw new ArgumentException("Incorrect moveDirection Vector")
            },
            1 => y switch
            {
                0 => 0,
                1 => 45,
                -1 => -45,
                _ => throw new ArgumentException("Incorrect moveDirection Vector")
            },
            -1 => y switch
            {
                0 => 180,
                1 => 135,
                -1 => -135,
                _ => throw new ArgumentException("Incorrect moveDirection Vector")
            },
            _ => throw new ArgumentException("Incorrect moveDirection Vector")
        };
    }

    private float GetAngleInCorrectFormat(float angle, string format)
    {
        float formatValue1;
        float formatValue2;
        switch (format)
        {
            case "-180 180":
                formatValue1 = -180;
                formatValue2 = 180;
                break;
            case "0 360":
                formatValue1 = 0;
                formatValue2 = 360;
                break;
            default:
                throw new ArgumentException($"Unknown angle format: {format}");
        }
        while (angle < formatValue1)
            angle += 360;
        while (angle > formatValue2)
            angle -= 360;
        return angle;
    }

    private enum rotationDirection
    {
        right,
        left,
        none
    }

    private rotationDirection GetRotationDirection(float targetAngle, float currentAngle)
    {
        var delta = targetAngle - currentAngle;
        delta = GetAngleInCorrectFormat(delta, "-180 180");
        if (delta < Player.RotationSpeed)
            return rotationDirection.none;
        return delta < 0 ? rotationDirection.right : rotationDirection.left;
    }
}
