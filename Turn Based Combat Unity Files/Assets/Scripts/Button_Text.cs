using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_Text : MonoBehaviour
{
    public Enemy_AI enemy_AI;
    public TextMeshProUGUI textboc;
    // Start is called before the first frame update
    void Start()
    {
        textboc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       textboc.text = "Attack: Damage the enemy by " + enemy_AI.playerDamageAttackGiven + "HP!";
  
    }
}
