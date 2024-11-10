using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI_Scripts;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI; // Reference to the inventory UI GameObject
    public GameObject inventorySlotPrefab; // Prefab for the inventory slots
    public Transform inventorySlotContainer; // The parent container where the slots will be placed

    public void UpdateUI(List<InventoryItem> items)
    {
        // Clear the existing slots
        foreach (Transform child in inventorySlotContainer)
        {
            Destroy(child.gameObject);
        }

        // Populate with new items
        foreach (InventoryItem item in items)
        {
            GameObject newSlot = Instantiate(inventorySlotPrefab, inventorySlotContainer);
            InventorySlot slotScript = newSlot.GetComponent<InventorySlot>();
            slotScript.SetItem(item);
        }
    }
}