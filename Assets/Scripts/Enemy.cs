using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Animator slime;
    public float health;
    public float maxHealth = 3f;
    public float moveSpeed = 2f;
    public float damage = 1;
    public bool chasePlayer;
    private Transform target;
    private Vector2 moveDirection;



    // Start is called before the first frame update
    void Start()
    {
        if (target)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }

        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 2;
        health = maxHealth;
        target = GameObject.Find("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        slime.SetTrigger("slimeDamaged");
        if (health <= 0)
        {
            StartCoroutine(SlimeDeath());       
        }
    }
    IEnumerator SlimeDeath()
    {
        slime.SetTrigger("slimeDeath");
        rb.linearVelocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    public void SlimeMovement()
    {
        rb.linearVelocity = new Vector2(0, 0);
    }
    private void FixedUpdate()
    {
        if (target)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

}
