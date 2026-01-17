using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    public Transform playerTransform;
    public Transform respawnPoint;
    public float respawnDelay = 3f;

    private Vector3 startPosition;
    private Health playerHealth;

    void Start()
    {
        if (playerTransform != null)
        {
            startPosition = playerTransform.position;
            playerHealth = playerTransform.GetComponent<Health>();
            if(playerHealth != null)
            {
                playerHealth.OnDeath += HandlePlayerDeath;
            }
        }
    }

    void HandlePlayerDeath()
    {
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        Debug.Log("Respawning in " + respawnDelay + " seconds...");
        yield return new WaitForSeconds(respawnDelay);

        // Reset Position
        playerTransform.position = respawnPoint != null ? respawnPoint.position : startPosition;
        
        // Reset Health
        // (You might need to make currentHealth public or add a Reset function in Health.cs)
        // For prototype simplicity, we just reload the scene or reset vars manually
        playerHealth.isDead = false;
        playerHealth.maxHealth = 100; // Reset logic here
        
        Debug.Log("Player Respawned");
    }
}