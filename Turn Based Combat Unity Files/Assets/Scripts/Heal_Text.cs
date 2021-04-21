using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Heal_Text : MonoBehaviour
{


    public Player_Controller player_Controller;
    public TextMeshProUGUI textboc;

    // Start is called before the first frame update
    void Start()
    {
        textboc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        textboc.text = "Heal: Restore your HP by " + player_Controller.playerHealed + "HP!";
    }
}
