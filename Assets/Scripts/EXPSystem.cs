using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EXPSystem : MonoBehaviour
{
    public Slider expBar;                      // Reference to the Slider for the EXP bar
    public TextMeshProUGUI levelText;          // Reference to the TMP text for level display
    public TextMeshProUGUI currentEXPText;     // Reference to the TMP text for current EXP display (e.g., "0XP/150XP")
    public float fillSpeed = 0.5f;             // Speed at which the bar fills up

    private int level = 1;                     // Starting player level
    private float currentEXP = 0;              // Current EXP amount
    private float requiredEXP = 100;           // EXP needed to level up (you can adjust this)

    public PlayerStats playerStats;            // Reference to PlayerStats script

    void Start()
    {
        UpdateLevelText();
        UpdateEXPBar();
        UpdateEXPTexts();   // Initial update of EXP texts

        // Disable interaction with the EXP slider
        if (expBar != null)
        {
            expBar.interactable = false; // This will prevent player from interacting with the slider
        }
    }

    // Call this method to add EXP
    public void AddEXP(float amount)
    {
        currentEXP += amount;

        // Check if the player leveled up
        while (currentEXP >= requiredEXP)
        {
            LevelUp();  // Level up while current EXP is enough to level up
        }

        StartCoroutine(FillEXPBar());  // Start the coroutine to gradually fill the bar
        UpdateEXPTexts();  // Update the EXP text immediately after adding EXP
    }

    // Coroutine to animate EXP bar filling
    private IEnumerator FillEXPBar()
    {
        float targetFill = currentEXP / requiredEXP;
        while (expBar.value < targetFill)
        {
            expBar.value += fillSpeed * Time.deltaTime;
            yield return null;
        }
    }

    // Method to handle leveling up
    private void LevelUp()
    {
        level++;
        currentEXP -= requiredEXP;       // Reset current EXP, keep excess EXP for next level
        requiredEXP *= 1.1f;             // Increase required EXP for the next level (optional scaling)

        // Add 1 Status Point to PlayerStats every level up
        playerStats.statusPoints++;      // Grant 1 Status Point

        UpdateLevelText();               // Update the displayed level text
        UpdateEXPBar();                  // Reset the EXP bar
        UpdateEXPTexts();                // Update the EXP texts
    }

    // Updates the level text UI
    private void UpdateLevelText()
    {
        levelText.text = "Level: " + level;
    }

    // Resets the EXP bar display
    private void UpdateEXPBar()
    {
        // Prevent overflow by ensuring the slider never goes beyond the max
        expBar.value = Mathf.Clamp(currentEXP / requiredEXP, 0f, 1f);
    }

    // Updates the current EXP and required EXP text UI in the format "currentEXP/requiredEXP"
    private void UpdateEXPTexts()
    {
        currentEXPText.text = Mathf.FloorToInt(currentEXP) + "XP/" + Mathf.FloorToInt(requiredEXP) + "XP";
    }
}
