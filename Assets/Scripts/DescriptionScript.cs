using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DescriptionScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    public TextMeshProUGUI DescriptionText;

    void Start()
    {
        DescriptionText.gameObject.SetActive(false);
    } 
    public void OnPointerEnter(PointerEventData eventData)
    {
        DescriptionText.gameObject.SetActive(true); 
    } 
    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptionText.gameObject.SetActive(false);
    }
}
