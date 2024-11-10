using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHP = 100;      // Maximum HP
    public int currentHP;        // Current HP
    public int expReward = 40;   // EXP awarded upon death

    public GameObject[] dropItems;  // Array of items that this enemy can drop (assigned in the Inspector)
    public float dropChance = 50f;  // Chance to drop an item (percentage, e.g., 50 means 50% chance)

    private EXPSystem expSystem;    // Reference to the EXPSystem script

    void Start()
    {
        currentHP = maxHP; // Initialize HP at the start
        expSystem = FindObjectOfType<EXPSystem>(); // Find the EXPSystem in the scene
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage; // Reduce current HP by damage amount
        currentHP = Mathf.Max(currentHP, 0); // Clamp currentHP to a minimum of 0

        // Check if the enemy is dead
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy has died.");

        // Grant EXP upon death
        if (expSystem != null)
        {
            expSystem.AddEXP(expReward);  // Award EXP
        }
        else
        {
            Debug.LogWarning("EXPSystem not found in the scene.");
        }

        // Drop a random item when the enemy dies
        DropItem();

        // Find the health bar and disable or destroy it
        HealthBarController healthBar = GetComponentInChildren<HealthBarController>();
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(false); // Disable the health bar
        }

        Destroy(gameObject); // Destroy the enemy game object
    }

    void DropItem()
    {
        if (dropItems.Length > 0)
        {
            // Determine whether to drop an item based on the drop chance
            float randomChance = Random.Range(0f, 100f); // Random float between 0 and 100

            if (randomChance <= dropChance) // If the random value is within the drop chance
            {
                // Choose a random item from the dropItems array
                GameObject itemToDrop = dropItems[Random.Range(0, dropItems.Length)];

                // Spawn the item at the enemy's position
                Vector3 dropPosition = transform.position;
                Instantiate(itemToDrop, dropPosition, Quaternion.identity);  // Instantiate item at enemy's position

                Debug.Log("Item dropped: " + itemToDrop.name);
            }
            else
            {
                Debug.Log("No item dropped this time.");
            }
        }
    }
}
