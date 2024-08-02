using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxTrigger : MonoBehaviour
{
    public ObjectDialogue dialogue;
    public Button interactButton;

    private void Awake()
    {
        if (interactButton != null)
        {
            interactButton.gameObject.SetActive(false); 
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("box"))
        {
            if (interactButton != null)
            {
                interactButton.gameObject.SetActive(true);                 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("box"))
        {
            if (interactButton != null)
            {
                interactButton.gameObject.SetActive(false);                 
            }
        }
    }

    public void TriggerDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }
}
