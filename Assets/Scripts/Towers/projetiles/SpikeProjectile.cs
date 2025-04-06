using UnityEngine;

public class SpikeProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float SpikeRadius = 0.1f;
    [SerializeField] private float damage = 5f;


    [SerializeField] private bool disableGravityOnReach = true; // Whether to disable gravity after reaching target

    private Vector3 targetPosition; // The target position for the spike
    private bool hasReachedTarget = false; // Flag to check if the spike has reached its target
    private Rigidbody rb; // Reference to the Rigidbody component

    // Set the target position for the spike
    public void Launch(Vector3 targetPos)
    {
        targetPosition = targetPos; // Set the target position
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component (if any)

        if (rb != null)
        {
            rb.isKinematic = false; // Ensure physics is active when moving the spike
            rb.useGravity = false;  // Disable gravity to prevent the spike from falling
        }
    }

    private void Update()
    {
        if (hasReachedTarget)
        {
            return; // If spike has reached its target, stop moving
        }

        // Calculate the direction towards the target but ignore the Z-axis
        Vector3 direction = new Vector3(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y, 0f);

        // Move towards the target position on the X and Y axes only
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // Ensure the spike moves only on the X and Y axes, keeping the Z position constant
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        // Check if we've reached the target position
        if (transform.position == targetPosition)
        {
            hasReachedTarget = true; // Stop the movement once we reach the target
            FreezeSpikePosition(); // Freeze the spike at its position
        }
    }

    private void FreezeSpikePosition()
    {
        // If the spike has a Rigidbody, freeze its position and rotation to prevent movement
        if (rb != null)
        {
            rb.isKinematic = true; // Make the Rigidbody kinematic so it doesn't respond to physics anymore
            rb.linearVelocity = Vector3.zero; // Stop any residual velocity
            rb.angularVelocity = Vector3.zero; // Stop any residual rotational velocity

            if (disableGravityOnReach)
            {
                rb.useGravity = false; // Disable gravity completely after reaching the target
            }
        }
    }

    private void Spiked()
    {
        Collider2D[] hitCollider = Physics2D.OverlapCircleAll(transform.position, SpikeRadius);
        foreach (Collider2D hit in hitCollider)
        {
            BalloonEnemey enemey = hit.GetComponent<BalloonEnemey>();
            if (enemey != null)
            {
                enemey.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        
    }

    private void FixedUpdate()
    {
        Spiked();
    }
}
