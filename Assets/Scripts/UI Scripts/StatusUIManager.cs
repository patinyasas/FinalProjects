using Item_Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
    public class StatusUIManager : MonoBehaviour
    {
        public GameObject statusPanel;  // Panel to display player stats
        public PlayerStats playerStats; // Reference to player stats
        public PlayerController playerController;


        public TextMeshProUGUI strengthText;
        public TextMeshProUGUI intelligenceText;
        public TextMeshProUGUI vitalityText;
        public TextMeshProUGUI agilityText;

        public TextMeshProUGUI attackPowerText;
        public TextMeshProUGUI magicPowerText;
        public TextMeshProUGUI maxHealthText;
        public TextMeshProUGUI evasionRateText;

        void Start()
        {
            // Ensure the status panel is hidden at the start
            if (statusPanel != null)
            {
                statusPanel.SetActive(false);
            }

            // Find PlayerStats component if not already assigned
            if (playerStats == null)
            {
                playerStats = FindObjectOfType<PlayerStats>();
            }
        }

        void Update()
        {
            // Toggle status panel visibility with a key press
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (statusPanel != null)
                {
                    bool isActive = !statusPanel.activeSelf;
                    statusPanel.SetActive(isActive);

                    // Update stats display if the panel is activated
                    if (isActive)
                    {
                        UpdateStatsUI();
                    }
                }
            }
        }

        // Update the displayed player stats on the UI
        public void UpdateStatsUI()
        {
            if (playerStats != null)
            {
                strengthText.text = "Strength: " + playerStats.strength;
                intelligenceText.text = "Intelligence: " + playerStats.intelligence;
                vitalityText.text = "Vitality: " + playerStats.vitality;
                agilityText.text = "Agility: " + playerStats.agility;

                attackPowerText.text = "ATK : " + playerStats.AttackPower;
                magicPowerText.text = "MP : " + playerStats.MagicPower;
                maxHealthText.text = "HP : " + playerStats.MaxHealth;
                evasionRateText.text = "SPD : " + playerStats.EvasionRate;
            }
        }
    }
}
