using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Reroll_Text : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textboc;
    public Player_RNG player_RNG;
    void Start()
    {
        textboc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        textboc.text = "Reroll: Click to reroll and get different card values! ";
    }
}
