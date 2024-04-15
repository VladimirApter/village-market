using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var moveDirection = GetMoveDirection();
        
        player.transform.Translate(moveDirection * (Player.Speed * Time.deltaTime));
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

        return moveDirection.normalized;
    }
}
