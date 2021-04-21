using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shield_Text : MonoBehaviour
{
    public Player_Controller player_controller;
    public TextMeshProUGUI textboc;
    // Start is called before the first frame update
    void Start()
    {
        textboc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        textboc.text = "Shield: Absorb " + player_controller.playerShield + " less damage the next time the enemy attacks!";

    }
}