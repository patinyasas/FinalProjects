using System.Collections.Generic;
using UI_Scripts;
using UnityEngine;

namespace Item_Scripts
{
    public class Inventory : MonoBehaviour
    {
        public List<InventoryItem> items = new List<InventoryItem>();
        public InventoryUI inventoryUI;
        private const int maxSlots = 4; // Maximum number of slots

        // Reference to the currently equipped item slot
        public InventorySlot currentlyEquippedSlot;
        public StatusUIManager statusUIManager; // Reference to StatusUIManager

        void Start()
        {
            if (inventoryUI != null)
            {
                inventoryUI.UpdateUI(items);
            }

            // Ensure the StatusUIManager is referenced
            if (statusUIManager == null)
            {
                statusUIManager = FindObjectOfType<StatusUIManager>();
            }
        }

        public void AddItem(InventoryItem item)
        {
            if (items.Count < maxSlots) // Check if there is space in the inventory
            {
                // Unequip the currently equipped item before adding a new item
                if (currentlyEquippedSlot != null && currentlyEquippedSlot.isEquipped)
                {
                    currentlyEquippedSlot.UnequipItem(); // Unequip and revert stats
                    currentlyEquippedSlot = null; // Clear the equipped slot reference
                }

                items.Add(item);

                // Update the UI
                if (inventoryUI != null)
                {
                    inventoryUI.UpdateUI(items);
                }

                // Update the status UI with the current player stats
                if (statusUIManager != null)
                {
                    statusUIManager.UpdateStatsUI();
                }
            }
            else
            {
                Debug.Log("Inventory is full!");
            }
        }
    }
}