
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    private Vector2 movement;

    void Update()
    {
        // Get input
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        movement = new Vector2(moveX, moveY) * moveSpeed;
    }

    void FixedUpdate()
    {
        // Move player
        rb.linearVelocity = movement;
    }
}
