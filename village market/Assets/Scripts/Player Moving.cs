using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Model;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
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
        var moveDirection = GetMoveDirection();
        if (moveDirection.x == 0 && moveDirection.y == 0) return;
        var rotationDirection = Vector2.Angle(moveDirection, Vector2.right);
        if (moveDirection.y < 0)
            rotationDirection = -rotationDirection;
        player.transform.eulerAngles = new Vector3(0, 0, (float)rotationDirection);
        player.transform.Translate(Vector2.right * (Player.Speed * Time.deltaTime));
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

}
