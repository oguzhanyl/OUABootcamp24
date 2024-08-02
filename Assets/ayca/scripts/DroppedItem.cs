using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public AudioClip destroySound; 
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("npc"))
        {
            if (destroySound != null)
            {
                audioSource.PlayOneShot(destroySound);
            }
            Destroy(gameObject, destroySound != null ? destroySound.length : 0); 
        }
    }

    public void Initialize()
    {        
        Debug.Log("Item initialized.");
    }
}
