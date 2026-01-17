using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damageAmount = 10f;
    public string targetTag; // Set to "Enemy" on Player weapon, "Player" on Enemy

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Health healthParams = other.GetComponent<Health>();
            if (healthParams != null)
            {
                healthParams.TakeDamage(damageAmount);
            }
        }
    }

    // Optional: Collision based for physical objects
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Health healthParams = collision.gameObject.GetComponent<Health>();
            if (healthParams != null)
            {
                healthParams.TakeDamage(damageAmount);
            }
        }
    }
}