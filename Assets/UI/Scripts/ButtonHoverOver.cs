using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject HoverImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverImage.SetActive(false);
    }
}
