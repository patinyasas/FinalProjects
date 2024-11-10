using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public EnemyStats enemyStats; // Reference to the EnemyStats component
    private Image healthBarImage;  // Reference to the Image component of the health bar

    void Start()
    {
        healthBarImage = GetComponent<Image>(); // Get the Image component
    }

    void Update()
    {
        // Update the health bar fill amount based on the enemy's health
        if (enemyStats != null)
        {
            healthBarImage.fillAmount = (float)enemyStats.currentHP / enemyStats.maxHP;

            // Optional: Change color based on health percentage
            healthBarImage.color = Color.Lerp(Color.red, Color.green, healthBarImage.fillAmount);
        }
    }

    void LateUpdate()
    {
        if (enemyStats != null)
        {
            // Position the health bar above the enemy
            transform.position = Camera.main.WorldToScreenPoint(enemyStats.transform.position + new Vector3(0, 1.5f, 0));
        }
    }
}