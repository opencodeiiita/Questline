using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Settings")]
    public float detectionRadius = 10f;
    public float attackRange = 2f;
    public float moveSpeed = 3f;
    public float attackCooldown = 1.5f;

    [Header("References")]
    public Transform player; // Drag player here or find in Start
    private Health myHealth;
    private float lastAttackTime;

    void Start()
    {
        myHealth = GetComponent<Health>();
        
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        // Don't do anything if dead or player is missing
        if (myHealth.isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // 1. Check Detection Radius
        if (distance <= detectionRadius)
        {
            // Face the player
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // Keep enemy upright
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // 2. Check Attack Range
            if (distance <= attackRange)
            {
                AttackPlayer();
            }
            else
            {
                // Chase Player
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }

    void AttackPlayer()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            Debug.Log("Enemy Attacks Player!");
            // Apply damage directly or trigger an animation that enables a DamageDealer collider
            Health playerHealth = player.GetComponent<Health>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(5f); // 5 damage per hit
            }
            
            lastAttackTime = Time.time;
        }
    }

    // Visualize ranges in Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}