using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Animator animator;
    private Rigidbody2D rb;
    private bool can_move = false;
    public bool CanMove { set { can_move = value; } }
    private Vector2 last_dir;

    private

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (can_move)
        {
            Move();
            Interact();
        }
    }

    private void Move()
    {
        // THIS IS PRETTY MUCH THE SAME BASIC CONTROLLERS PROVIDED WITH THE TILEMAP ASSET BUNDLE
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
            animator.SetInteger("Direction", 3);
            last_dir = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
            animator.SetInteger("Direction", 2);
            last_dir = Vector2.right;
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
            animator.SetInteger("Direction", 1);
            last_dir = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
            animator.SetInteger("Direction", 0);
            last_dir = Vector2.down;
        }

        dir.Normalize();
        animator.SetBool("IsMoving", dir.magnitude > 0);

        rb.velocity = speed * dir;
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, last_dir, 1.25f);
            Debug.LogError(hit.collider.name);
        }
        // UiManager.instance.counter.Show();
    }
}
