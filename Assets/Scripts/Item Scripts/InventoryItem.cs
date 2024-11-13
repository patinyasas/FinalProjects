using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    // Stat modifiers for the item
    public int strengthModifier;
    public int intelligenceModifier;
    public int vitalityModifier;
    public int agilityModifier;

    // Rarity of the item (e.g., 1 = Common, 2 = Rare, 3 = Epic, 4 = Legendary)
    public int rarity; 
}