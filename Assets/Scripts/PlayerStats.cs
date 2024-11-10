using Item_Scripts;
using UI_Scripts;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int strength = 10;
    public int intelligence = 8;
    public int vitality = 12;
    public int agility = 6;
    public PlayerController playerController;
    public GameObject statusPanel;
    public StatusUIManager statusUIManager;
    // Calculated stats based on primary stats
    public int AttackPower => playerController.baseAttackDamage + strength * 2;             // Example: AttackPower based on Strength
    public int MagicPower => intelligence * 3;          // Example: MagicPower based on Intelligence
    public int MaxHealth => vitality * 10;              // Example: Health Points based on Vitality
    public float EvasionRate => agility * 0.5f;         // Example: Evasion Rate based on Agility
    
    // Status Points
    public int statusPoints;         // Status Points available to spend

    // Method to spend status points and upgrade a specific stat
    public void SpendStatusPoint(string statName)
    {
        if (statusPoints > 0) // Check if the player has status points to spend
        {
            switch (statName)
            {
                case "strength":
                    strength += 2;  // Increase strength by 2
                    break;
                case "intelligence":
                    intelligence += 2;  // Increase intelligence by 2
                    break;
                case "vitality":
                    vitality += 5;  // Increase vitality by 5
                    break;
                case "agility":
                    agility += 1;  // Increase agility by 1
                    break;
                default:
                    Debug.LogError("Invalid stat name: " + statName);
                    return;
            }

            statusPoints--;  // Spend 1 status point
            Debug.Log("Spent 1 Status Point on " + statName);
            if (statusUIManager != null)
            {
                statusUIManager.UpdateStatsUI();
            }
        }
        else
        {
            Debug.Log("No Status Points available to spend.");
        }
    }
}
