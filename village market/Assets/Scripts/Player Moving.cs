using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public GameObject player = Player.PlayerObj;
    [SerializeField] public float speed;
    public Animator animator;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);

        if (direction.x == 0 && direction.y == 0) return;

        var rotationDirection = Vector2.SignedAngle(Vector2.up, direction);
        //player.transform.eulerAngles = new Vector3(0, 0, rotationDirection);

        // Перемещаем персонажа в соответствии с направлением
        player.transform.position = new Vector3(
            player.transform.position.x + direction.x * speed * Time.deltaTime,
            player.transform.position.y + direction.y * speed * Time.deltaTime,
            player.transform.position.z
        );
    }
}