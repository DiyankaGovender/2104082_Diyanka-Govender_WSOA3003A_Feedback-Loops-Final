using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Button_Hover : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
    public TextMeshProUGUI attackDescription;
  

    public void start()
    {
        mouse_over = false;
        if (mouse_over == false)
        {
            attackDescription.enabled = false;
        }
    }
    void Update()
    {
        if (mouse_over)
        {
            attackDescription.enabled = true;
            

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        attackDescription.enabled = true;
       
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        attackDescription.enabled = false;
        mouse_over = false;
        
    }
}
