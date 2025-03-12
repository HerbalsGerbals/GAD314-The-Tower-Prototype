using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    private GameObject target;

    public void SetTarget(GameObject enemy)
    {
        target = enemy;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Destroy projectile if enemy is gone
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, target.transform.position) < 0.2f)
        {
            Destroy(target);  // Destroy the enemy
            Destroy(gameObject);  // Destroy the projectile
        }
    }
}
