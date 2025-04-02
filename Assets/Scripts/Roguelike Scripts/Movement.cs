using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector3 input;

    Animator anim;
    private Vector3 lastMoveDirection;
    private bool facingLeft = true;

    [SerializeField] private Transform Aim;
    public bool isWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Animate();
        //Flips character walking sprite
        if (input.x > 0 && !facingLeft || input.x < 0 && facingLeft)
        {
            flip();
        }
        rb.linearVelocity = input * speed;
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if((moveX == 0 && moveY == 0) && (input.x !=0 || input.y !=0))
        {
            isWalking = false;
            lastMoveDirection = input;
            
        }
        else if(moveX != 0 || moveY != 0)
        {
            isWalking = true;
            //Keeps Attack in front of player at all times
            Vector3 vector3 = Vector3.left * input.x + Vector3.down * input.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();
    }

    
    void Animate()
    {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }
    


    void flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; //Flips sprite
        transform.localScale = scale;
        facingLeft = !facingLeft;
    }
}
