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

    public static Quaternion rotation = new Quaternion(-5, 0, 0, 0);
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        
        if (direction != new Vector2(0, 0))
        {
            animator.SetFloat(Horizontal, direction.x);
            animator.SetFloat(Vertical, direction.y);
            animator.SetFloat(Speed, direction.sqrMagnitude);
        }
        
        var position = player.transform.position;
        position = new Vector3(
            position.x + direction.x * speed * Time.deltaTime,
            position.y + direction.y * speed * Time.deltaTime,
            position.z
        );
        player.transform.position = position;
    }
}