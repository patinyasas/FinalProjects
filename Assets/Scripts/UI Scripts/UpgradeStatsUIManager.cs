using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeStatsUIManager : MonoBehaviour
{
    public GameObject upgradePanel;                // Reference to the panel for stat upgrades
    public TextMeshProUGUI statusPointsText;       // Text to show current status points
    public PlayerStats playerStats;                // Reference to the PlayerStats script

    public Button strengthButton;
    public Button intelligenceButton;
    public Button vitalityButton;
    public Button agilityButton;

    void Start()
    {
        // Initialize buttons and set listeners
        strengthButton.onClick.AddListener(() => UpgradeStat("strength"));
        intelligenceButton.onClick.AddListener(() => UpgradeStat("intelligence"));
        vitalityButton.onClick.AddListener(() => UpgradeStat("vitality"));
        agilityButton.onClick.AddListener(() => UpgradeStat("agility"));

        // Hide the upgrade panel by default
        upgradePanel.SetActive(false);
    }

    void Update()
    {
        // Toggle panel visibility on "K" key press
        if (Input.GetKeyDown(KeyCode.K))
        {
            upgradePanel.SetActive(!upgradePanel.activeSelf);
        }

        // Update the status points text every frame
        UpdateStatusPointsText();
    }

    // Method to upgrade the selected stat
    private void UpgradeStat(string statName)
    {
        if (playerStats.statusPoints > 0)
        {
            playerStats.SpendStatusPoint(statName);
            UpdateStatusPointsText();  // Update the UI after spending points
        }
        else
        {
            Debug.Log("No Status Points available.");
        }
    }

    // Update the status points text UI
    private void UpdateStatusPointsText()
    {
        statusPointsText.text = "Status Points: " + playerStats.statusPoints;
    }
}