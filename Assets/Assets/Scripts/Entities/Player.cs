using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Animator animator;
    private Rigidbody2D rb;
    private BoxCollider2D b_collider;
    private bool can_move = false;
    public bool CanMove { set { can_move = value; } }
    private Vector2 last_dir;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        b_collider = GetComponent<BoxCollider2D>();
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
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawSphere(RayOrigin(), 0.15f);
    }
#endif

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.CircleCast(RayOrigin(), 0.15f, last_dir, 0.025f);

            if (!hit) return;

            if (hit.collider.tag != "Interactable") return;

            hit.transform.GetComponent<InteractableObject>().Interact();
        }
    }

    private Vector2 RayOrigin()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (last_dir == Vector2.up)
            return new Vector2(x, y + (b_collider.size.y + b_collider.offset.y));
        if (last_dir == Vector2.down || last_dir == Vector2.zero)
            return new Vector2(x, y - b_collider.offset.y);

        if (last_dir == Vector2.left)
            x -= (b_collider.size.x + b_collider.offset.x);
        if (last_dir == Vector2.right)
            x += (b_collider.size.x + b_collider.offset.x);
        return new Vector2(x, y + b_collider.offset.y);

    }

    public void ChangeOutfit(string id)
    {
        animator.runtimeAnimatorController = OutfitLibrary.instance.GetOutfit(id);
    }
}
