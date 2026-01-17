using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("VFX")]
    public ParticleSystem hitEffect; // Drag a simple particle prefab here if available
    
    public bool isDead = false;

    // Event to notify other scripts (like AI or UI) that death happened
    public delegate void DeathAction();
    public event DeathAction OnDeath;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Current Health: {currentHealth}");

        // VFX: Simple Hit Feedback (Flash Red)
        StartCoroutine(FlashRed());
        if(hitEffect != null) hitEffect.Play();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        OnDeath?.Invoke(); // Notify listeners
        
        // Lifecycle requirement: Clean removal or respawn
        // For enemies, we usually destroy. For players, we might respawn via Manager.
        if (gameObject.CompareTag("Player"))
        {
             // Handled by CombatManager usually, but for now just log it
             Debug.Log("Player Died!");
        }
        else
        {
            Destroy(gameObject, 0.5f); // Destroy enemy after small delay
        }
    }

    IEnumerator FlashRed()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            Color original = rend.material.color;
            rend.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            rend.material.color = original;
        }
    }
}