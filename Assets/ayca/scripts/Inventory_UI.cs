using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public List<SlotUI> uiList = new List<SlotUI>();
    Inventory userInventory;

    private void Start()
    {
        userInventory=gameObject.GetComponent<Inventory>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        for(int i=0;i<uiList.Count;i++) 
        
        {
            if (userInventory.playerInventory.InventorySlots[i].itemCount>0)
            {
                uiList[i].itemImage.sprite = userInventory.playerInventory.InventorySlots[i].item.itemIcon;
                if (userInventory.playerInventory.InventorySlots[i].item.canStackable==true)
                {
                    uiList[i].itemCountText.gameObject.SetActive(true);
                    uiList[i].itemCountText.text = userInventory.playerInventory.InventorySlots[i].itemCount.ToString();
                }
                else
                {
                    uiList[i].itemCountText.gameObject.SetActive(false);
                }
            }
            else
            {
                uiList[i].itemImage.sprite = null;
                uiList[i].itemCountText.gameObject.SetActive(false);
            }
        }
    }
}
