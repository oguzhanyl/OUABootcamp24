using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable/Inventory")]
public class SCInventory : ScriptableObject
{
    public List<Slot> InventorySlots = new List<Slot>();
    int stackLimit = 6;

    public void DeleteItem(int index)
    {
        InventorySlots[index].isFull = false;
        InventorySlots[index].itemCount = 0;
        InventorySlots[index].item = null;
    }

    public void DropItem(int index, Vector3 position)
    {
        if (InventorySlots[index].item != null && InventorySlots[index].item.itemPrefab != null)
        {
            for (int i = 0; i < InventorySlots[index].itemCount; i++)
            {

                Vector3 dropPosition = new Vector3(position.x + i * 1.0f, position.y * 1.1f, position.z);

                GameObject tempItem = Instantiate(InventorySlots[index].item.itemPrefab, dropPosition, Quaternion.identity);

                if (tempItem.GetComponent<DroppedItem>() == null)
                {
                    tempItem.AddComponent<DroppedItem>();
                }

                DroppedItem droppedItemScript = tempItem.GetComponent<DroppedItem>();
            }
            DeleteItem(index);

        }
    }


    public bool AddItem(SCITem item)
    {
        foreach (Slot slot in InventorySlots)
        {
            if (slot.item == item)
            {
                if (slot.item.canStackable)
                {
                    if (slot.itemCount < stackLimit)
                    {
                        slot.itemCount++;
                        if (slot.itemCount >= stackLimit)
                        {
                            slot.isFull = true;
                        }
                        return true;
                    }
                }
            }
            else if (slot.itemCount == 0)
            {
                slot.AddItemStoSlot(item);
                return true;
            }
        }
        return false;
    }

}

[System.Serializable]
public class Slot
{
    public bool isFull;
    public int itemCount;
    public SCITem item;

    public void AddItemStoSlot(SCITem item)
    {
        this.item = item;
        if (item.canStackable == false)
        {
            isFull = false;
        }
        itemCount++;
    }
}
