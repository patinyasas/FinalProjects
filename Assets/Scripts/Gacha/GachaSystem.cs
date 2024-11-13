using System.Collections.Generic;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    public List<InventoryItem> itemPool; // All items available in the gacha
    public int pityThreshold = 10; // Number of pulls until guaranteed rare
    private int pullCount = 0;

    // Define probabilities (percentages)
    private readonly Dictionary<int, float> dropRates = new Dictionary<int, float>
    {
        {1, 70f},  // Common: 70%
        {2, 20f},  // Rare: 20%
        {3, 8f},   // Epic: 8%
        {4, 2f}    // Legendary: 2%
    };

    public InventoryItem PerformGacha()
    {
        pullCount++;
        float roll = Random.Range(0f, 100f);
        InventoryItem rewardedItem = null;

        // Check for pity
        if (pullCount >= pityThreshold)
        {
            rewardedItem = GetGuaranteedRareItem();
            pullCount = 0; // Reset pity counter
        }
        else
        {
            rewardedItem = GetRandomItemByRarity(roll);
        }

        return rewardedItem;
    }

    private InventoryItem GetGuaranteedRareItem()
    {
        // Get items with rarity 3 and above (Epic or Legendary)
        List<InventoryItem> rareItems = itemPool.FindAll(item => item.rarity >= 3);
        return rareItems[Random.Range(0, rareItems.Count)];
    }

    private InventoryItem GetRandomItemByRarity(float roll)
    {
        float cumulativeRate = 0;
        foreach (var rate in dropRates)
        {
            cumulativeRate += rate.Value;
            if (roll <= cumulativeRate)
            {
                // Get items of the matching rarity
                List<InventoryItem> itemsOfRarity = itemPool.FindAll(item => item.rarity == rate.Key);
                return itemsOfRarity[Random.Range(0, itemsOfRarity.Count)];
            }
        }
        return null;
    }
}