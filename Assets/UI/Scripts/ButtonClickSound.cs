using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public AudioSource ClickSource;
    public AudioClip clickEffect;
    public AudioClip hoverEffect;

    public void HoverSound()
    {
        ClickSource.PlayOneShot(hoverEffect);
    }

    public void ClickSound()
    {
        ClickSource.PlayOneShot(clickEffect);
    }
}
