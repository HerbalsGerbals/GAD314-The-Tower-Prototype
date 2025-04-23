using UnityEngine;

public class SpikeProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float SpikeRadius = 0.1f;
    [SerializeField] private float damage = 5f;


    [SerializeField] private bool disableGravityOnReach = true;
    private Vector3 targetPosition;
    private bool hasReachedTarget = false;
    private Rigidbody rb;


    public void Launch(Vector3 targetPos)
    {
        targetPosition = targetPos;
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = false;
        }
    }

    private void Update()
    {
        if (hasReachedTarget)
        {
            return;
        }

        Vector3 direction = new Vector3(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y, 0f);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        if (transform.position == targetPosition)
        {
            hasReachedTarget = true;
            FreezeSpikePosition();
        }
    }

    private void FreezeSpikePosition()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            if (disableGravityOnReach)
            {
                rb.useGravity = false;
            }
        }
    }

  /*  private void Spiked()
    {
        Collider2D[] hitCollider = Physics2D.OverlapCircleAll(transform.position, SpikeRadius);
        foreach (Collider2D hit in hitCollider)
        {
            EnemyMovement1 enemey = hit.GetComponent<EnemyMovement1>();
            if (enemey != null)
            {
                enemey.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    } */

    private void FixedUpdate()
    {
        // Spiked();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Slimes"))
        {
            Destroy(gameObject);
        }
    }
}
