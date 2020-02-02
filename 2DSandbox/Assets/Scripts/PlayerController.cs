using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 300f;
    [Range(0, .3f)] [SerializeField] private float smoothMovement = 0.05f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool airControl = false;

    private bool on_the_ground;
    private const float ground_check_radius = 0.05f;
    private bool face_right = true;
    private Vector3 vel = Vector3.zero;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // determines if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, ground_check_radius, ground);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                on_the_ground = true;
            }
        }
    }

    public void Move(float move_x, bool jump)
    {
        // horizontal movement
        if (on_the_ground || airControl)
        {
            Vector3 targetVel = new Vector2(move_x * speed, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVel, ref vel, smoothMovement);

            // flip the player if changing directions
            if (move_x > 0 && !face_right)
            {
                Flip();
            }
            else if (move_x < 0 && face_right)
            {
                Flip();
            }
        }

        // jump movement
        if (on_the_ground && jump)
        {
            on_the_ground = false;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // pick up coins
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
        }
    }

    void Flip()
    {
        // flip the way the player is facing
        face_right = !face_right;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
