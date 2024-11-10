using Item_Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

namespace UI_Scripts
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        public Image icon;
        public TextMeshProUGUI itemName;
        public Image borderImage;
        private InventoryItem item;
        private PlayerStats playerStats;

        // New reference for displaying item stats
        public TextMeshProUGUI itemStatsText; // Text element to display item stats
        private static TextMeshProUGUI currentlyDisplayedStatsText; // Store the currently displayed stats text

        public bool isEquipped = false;
        private static InventorySlot currentlyEquippedSlot = null;

        private StatusUIManager statusUIManager;

        void Start()
        {
            playerStats = FindObjectOfType<PlayerStats>();
            statusUIManager = FindObjectOfType<StatusUIManager>(); // Find the StatusUIManager instance
        }

        public void SetItem(InventoryItem newItem)
        {
            item = newItem;
            icon.sprite = item.icon;
            itemName.text = item.itemName;
            borderImage.color = Color.white;

            // Make sure the item stats text is hidden by default
            if (itemStatsText != null)
            {
                itemStatsText.gameObject.SetActive(false);
            }
        }

        private float lastClickTime;
        private const float doubleClickDelay = 0.3f;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Time.time - lastClickTime < doubleClickDelay)
            {
                ToggleEquipItem();  // Handle double click (for equipping)
            }
            else
            {
                ShowItemStats();  // Handle single click (for showing item stats)
            }

            lastClickTime = Time.time;
        }

        void ShowItemStats()
        {
            // If there's another stats text being shown, hide it
            if (currentlyDisplayedStatsText != null && currentlyDisplayedStatsText != itemStatsText)
            {
                StartCoroutine(FadeOutText(currentlyDisplayedStatsText)); // Fade out the old stats text
            }

            if (itemStatsText != null)
            {
                // Display the item's stats when clicked once
                itemStatsText.gameObject.SetActive(true); // Show the text element
                itemStatsText.text = $"Strength: {item.strengthModifier}\n" +
                                     $"Vitality: {item.vitalityModifier}\n" +
                                     $"Intelligence: {item.intelligenceModifier}\n" +
                                     $"Agility: {item.agilityModifier}";

                // Set the current itemStatsText to be the displayed one
                currentlyDisplayedStatsText = itemStatsText;

                // Start the fade-out process after a few seconds
                StartCoroutine(FadeOutText(itemStatsText));
            }
        }

        IEnumerator FadeOutText(TextMeshProUGUI text)
        {
            float timeToFade = 5f; // Time to fade out the text
            float elapsedTime = 0f;
            Color originalColor = text.color;

            while (elapsedTime < timeToFade)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / timeToFade);
                text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }

            // After fading out, deactivate the text object
            text.gameObject.SetActive(false);
        }

        void ToggleEquipItem()
        {
            if (!isEquipped && currentlyEquippedSlot != null)
            {
                Debug.Log("Another item is already equipped!");
                return;
            }

            if (isEquipped)
            {
                UnequipItem();
            }
            else
            {
                EquipItem();
            }

            // Update the stats UI in real-time
            if (statusUIManager != null)
            {
                statusUIManager.UpdateStatsUI();
            }
        }

        void EquipItem()
        {
            // If there is already an equipped item, unequip it first
            Inventory inventory = FindObjectOfType<Inventory>(); // Get the inventory instance
            if (inventory.currentlyEquippedSlot != null && inventory.currentlyEquippedSlot.isEquipped)
            {
                inventory.currentlyEquippedSlot.UnequipItem(); // Unequip and revert stats
            }

            // Now equip the current item
            playerStats.strength += item.strengthModifier;
            playerStats.intelligence += item.intelligenceModifier;
            playerStats.vitality += item.vitalityModifier;
            playerStats.agility += item.agilityModifier;

            borderImage.color = Color.green; // Highlight equipped item
            isEquipped = true;

            // Update the currently equipped item slot
            inventory.currentlyEquippedSlot = this; // Set the current slot as the equipped item

            Debug.Log("Item equipped: " + item.itemName);
        }

        // Unequip the item and revert stats
        public void UnequipItem()
        {
            playerStats.strength -= item.strengthModifier;
            playerStats.intelligence -= item.intelligenceModifier;
            playerStats.vitality -= item.vitalityModifier;
            playerStats.agility -= item.agilityModifier;

            borderImage.color = Color.white; // Remove highlight
            isEquipped = false;

            Debug.Log("Item unequipped: " + item.itemName);
        }
    }
}
