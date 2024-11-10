using UnityEngine;

public class IgnoreEnemyCollision : MonoBehaviour
{
    public GameObject enemy;  // Reference to the enemy GameObject

    void Start()
    {
        // Get the colliders attached to both player and enemy
        Collider playerCollider = GetComponent<Collider>();
        Collider enemyCollider = enemy.GetComponent<Collider>();

        // Ignore collision between player and enemy
        if (playerCollider != null && enemyCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, enemyCollider);
        }
    }
}