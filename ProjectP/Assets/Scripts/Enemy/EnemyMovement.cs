using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform target;

    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        moveDirection = direction;

        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed", moveDirection.normalized.magnitude);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2 (moveDirection.x, moveDirection.y) * moveSpeed;
    }
}
