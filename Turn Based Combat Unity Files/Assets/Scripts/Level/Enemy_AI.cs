using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    //Need to make this a random variable
    public Enemy_RNG enemy_RNG;

    
    public int enemyCurrentHealth;
    public int enemyMaxHealth;

    //THIS WILL BE A CARD DRAFTED NUMBER 
    public int playerDamageAttackGiven =5;

    //THIS WILL BE A RANDOM NUMBER 
    public int enemyHealed;

    //SHIELD STUFF
    public int enemyShield; //RANDOM NUMBER
    public bool enemyHasShield;


    void Start()
    {
        enemyHasShield = false;
    }

    void Update()
    {
        
    }

    //PLAYER ATTACKS ENEMY
    public void enemyTakenDamage()
    {
        enemyCurrentHealth = enemyCurrentHealth - playerDamageAttackGiven;
        //Debug.Log("enemy" + enemyCurrentHealth);
    }

    public void enemyHealedUp()
    {
        enemyHealed = enemy_RNG.enemyAttackHealStatNumber;
        enemyCurrentHealth = enemyCurrentHealth + enemyHealed;
        if (enemyCurrentHealth > enemyMaxHealth)
        {
            enemyCurrentHealth = enemyMaxHealth;
        }
    }

  
   public void enemyShielded()
    {
        enemyHasShield = true;
        enemyShield = enemy_RNG.enemyAttackHealStatNumber;
    }

    public void enemyTakesShieldDamage()
    {
        enemyShield = enemy_RNG.enemyAttackHealStatNumber;
        enemyCurrentHealth = enemyCurrentHealth - (playerDamageAttackGiven - enemyShield);
    }
}
