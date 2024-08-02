using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public SCInventory playerInventory;
    Inventory_UI inventoryui;

    public AudioClip collectSound;
    public AudioClip dropSound;
    private AudioSource audioSource;

    bool isSwapping;
    int tempIndex;
    Slot tempSlot;

    private void Start()
    {
        inventoryui=gameObject.GetComponent<Inventory_UI>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void DeleteItem()
    {
        if(isSwapping==true)
        {
            playerInventory.DeleteItem(tempIndex);
            isSwapping=false;
        }
    }

    public void DropItem()
    {
        if(isSwapping ==true)
        {
            playerInventory.DropItem(tempIndex, gameObject.transform.position + Vector3.forward);
            PlayDropSound();
            isSwapping =false;
            inventoryui.UpdateUI();
        }
    }

    public void SwapItem(int index)
    {
        if(isSwapping==false)
        {
            tempIndex = index;
            tempSlot = playerInventory.InventorySlots[tempIndex];
            isSwapping = true;
        }
        else if(isSwapping == true)
        {
            playerInventory.InventorySlots[tempIndex] = playerInventory.InventorySlots[index];
            playerInventory.InventorySlots[index] = tempSlot;
            isSwapping = false;
        }
        inventoryui.UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("item"))
        {
            if(playerInventory.AddItem(other.gameObject.GetComponent<Item>().item))
            {
                if (collectSound != null)
                {
                    audioSource.PlayOneShot(collectSound); 
                }
                Destroy(other.gameObject);
                ScoreMAnager.scoreCount += 1;
                inventoryui.UpdateUI();
            }
        }
    }

    private void PlayDropSound()
    {
        if (dropSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(dropSound);
        }
    }
}
