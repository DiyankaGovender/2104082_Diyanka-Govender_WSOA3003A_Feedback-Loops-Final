using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class Shield_Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
    public TextMeshProUGUI shieldDescription;


    public void start()
    {
        mouse_over = false;
        if (mouse_over == false)
        {
            shieldDescription.enabled = false;
        }
    }
    void Update()
    {
        if (mouse_over)
        {
            shieldDescription.enabled = true;
            

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        shieldDescription.enabled = true;


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        shieldDescription.enabled = false;
        mouse_over = false;
        
    }
}
