using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Enemy_RNG enemy_RNG;
    public Player_RNG player_RNG;

    public int playerCurrentHealth;
    public int playerMaxHealth;

    public int playerHealed =0;

    public int playerShield;
    public bool playerHasShield;

    //THIS WILL BE A RANDOMLY GENERATED NUMBER 
    public int enemyDamageGiven;

   


    void Start()
    {
        playerHasShield = false;
    }


    void Update()
    {
        
    }

    //ENEMY DAMAGE NUMBER
    public void playerTakenDamage()
    {
        enemyDamageGiven = enemy_RNG.enemyAttackHealStatNumber;
        playerCurrentHealth = playerCurrentHealth - enemyDamageGiven ;
       
    }


    //PLAYER HEALED NUMBER 
    public void playerHealedUp()
    {
        playerCurrentHealth = playerCurrentHealth + playerHealed;
        if(playerCurrentHealth > playerMaxHealth)
        {
            playerCurrentHealth = playerMaxHealth;
        }
    }


    //PLAYER SHIELD NUMBER
    public void playerShielded()
    {
        
        
        {
            playerHasShield = true;
            enemyDamageGiven = enemy_RNG.enemyAttackHealStatNumber;
           
            
        }
    }

    public void playerTakesShieldDamage()
    {
        enemyDamageGiven = enemy_RNG.enemyAttackHealStatNumber;
        playerCurrentHealth = playerCurrentHealth - (enemyDamageGiven - playerShield);
    }

}
