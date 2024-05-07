/*using System.Collections;

using UnityEngine;

public class PlayerController: MonoBehaviour
{
    [SerializeField] public float speed;
    public Animator animator;
    private Vector2 direction;
    private Rigidbody2D rb;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    void FixedUpdate()
    {
        //rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));
    }
}*/