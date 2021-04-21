using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Heal_Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
    public TextMeshProUGUI healDescription;


    public void start()
    {
        mouse_over = false;
        if (mouse_over == false)
        {
            healDescription.enabled = false;
        }
    }
    void Update()
    {
        if (mouse_over)
        {
            healDescription.enabled = true;
          

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        healDescription.enabled = true;

     
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        healDescription.enabled = false;
        mouse_over = false;
      
    }
}
