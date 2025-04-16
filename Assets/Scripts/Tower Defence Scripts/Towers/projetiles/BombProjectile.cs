using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float explosionRadius = 2f;

    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log("Target set: " + target?.name);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            Explosion();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void Explosion()
    {
        Collider2D[] hitCollider = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hitCollider)
        {
            EnemyMovement1 enemey = hit.GetComponent<EnemyMovement1>();
            if (enemey != null)
            {
                enemey.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}