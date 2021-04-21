using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum GameState
{ 
    gameStart,


    battleSetup,
    
    howtoPlay, 

    coinToss,

   
    playerTurn,
    enemyTurn,

    playerWins,
    playerLost,
    draw,

}


public class BattleState : MonoBehaviour
{
    //ENUM GAME STATES 
    public GameState gameState;
    


    //RELATED SCRIPTS 
    public Enemy_AI enemy_AI;
    public Player_Controller player_Controller;
    public Enemy_RNG enemy_RNG;
    public Player_RNG player_RNG;

    //PLAYER AND ENEMY SPRITES
    public GameObject player;
    public GameObject enemy;


    //PLAYER AND ENEMY HEALTH BAR
    public Slider enemySlider;
    public Slider playerSlider;

    //ENEMY AND PLAYER HEALTH
    public TextMeshProUGUI playerUI;
    public TextMeshProUGUI playerUI2;
    public TextMeshProUGUI enemyUI;
    public TextMeshProUGUI enemyUI2;

    //PLAYER AND ENEMY ANIMATIONS 
    public Animator playerAnim;
    public Animator enemyAnim;

    //AUDIO SOURCES
    public AudioSource click;
    public AudioSource damage;
    public AudioSource heal;
    public AudioSource happy;
    public AudioSource sad;

   
    //OTHER UI
    public GameObject square;
    public GameObject square2;


  
    //MAIN TEXT BOX AND OTHER TEXT BOXES
    public TextMeshProUGUI maintextBox;
    public TextMeshProUGUI pickNumber;
    public TextMeshProUGUI roundtextBox;
    
    

    //BUTTONS
    public GameObject startButton;
   
    public GameObject playerAttackButton;
    public GameObject playerHealButton;
    public GameObject playerShieldButton;

    public GameObject randomiseButton;

    //COIN TOSS ELEMMENTS 
    public GameObject coinTossButton;
    public GameObject coin;
    public TextMeshProUGUI coinText;
    public int coinNumber;
    public Sprite coinHead;
    public Sprite coinTail;


    //PLAYER AND ENEMY NUMBER CHECKER
    public int cardNumberPlayerChose;
    public int cardNumberEnemyChose;


    public int playerturncount;


    public GameObject closeButton;
    public GameObject closeImg;

    //START SCENE 
    void Start()
    {
        
        gameState = GameState.gameStart;

        
        //PLAYER AND ENEMY DISBALED 
        player.SetActive(false);
        playerUI.enabled = false;
        playerUI2.enabled = false;


        enemy.SetActive(false);
        enemyUI.enabled = false;
        enemyUI2.enabled = false;

        enemySlider.gameObject.SetActive(false);
        playerSlider.gameObject.SetActive(false);

        //UI DISABLED 
        maintextBox.enabled = false;
        pickNumber.enabled = false;
        roundtextBox.enabled = false;

        //START BUTTON ENABLED 
        startButton.SetActive(true);
 
        //PLAYER BUTTONS DISABLED 
        playerAttackButton.SetActive(false);
        playerHealButton.SetActive(false);
        playerShieldButton.SetActive(false);
        randomiseButton.SetActive(false);

        //COIN ELEMENTS DISABLED 
        coin.SetActive(false);
        coinText.enabled = false;
        coinTossButton.SetActive(false);

        playerturncount = 0;

        closeButton.SetActive(false);
        closeImg.SetActive(false);
    }

    public void Update()
    {
       // Debug.Log("Enemy Number " + cardNumberEnemyChose);
        //Debug.Log("Player Number " + cardNumberPlayerChose);
        //Debug.Log("Player Shield " + player_Controller.playerHasShield); 

        //STOP PLAYER AND ENEMY HEALTH FROM BEING ABOVE X
        if(player_Controller.playerCurrentHealth > player_Controller.playerMaxHealth)
        {
            player_Controller.playerCurrentHealth = player_Controller.playerMaxHealth =25;
        }

        if(enemy_AI.enemyCurrentHealth > enemy_AI.enemyMaxHealth)
        {
            enemy_AI.enemyCurrentHealth = enemy_AI.enemyMaxHealth =20;
        }

        //UI ENEMY AND PLAYER HEALTH BAR UPDATED 
        playerUI.text = player_Controller.playerCurrentHealth + "/" + player_Controller.playerMaxHealth;
        enemyUI.text = enemy_AI.enemyCurrentHealth + "/" + enemy_AI.enemyMaxHealth;
    }



    public void onStartButtonState()
    {
       
        if (gameState == GameState.gameStart)
        {
            startButton.SetActive(false);
           
            closeButton.SetActive(true);
            closeImg.SetActive(true);

            gameState = GameState.howtoPlay;
        }
    }

    public void closeHowtoPlay()
    {
        if(gameState == GameState.howtoPlay)
        {
            closeButton.SetActive(false);
            closeImg.SetActive(false);

            coin.SetActive(true);
            coinText.enabled = true;
            coinTossButton.SetActive(true);

        }
    }

        //COIN TOSS
        public void onCoinToss()
    {
        gameState = GameState.coinToss;

        coinNumber = Random.Range(1, 3);


        StartCoroutine(postCoinToss());
        
    }

   public IEnumerator postCoinToss()
    {
        //HEADS = PLAYER TURN 
        if (coinNumber == 0)
        {
            coinNumber = 1;
        }
        if (coinNumber == 1)
        {
            coin.GetComponent<SpriteRenderer>().sprite = coinHead;
            coinText.text = "<color=#0000ffff><b>YOU</b></color> go <b>FIRST</b>!";
            yield return new WaitForSeconds(3f);
            battleSetupState();
        }

        //TAIL = ENEMY TURN
        if (coinNumber == 2)
        {
            coin.GetComponent<SpriteRenderer>().sprite = coinTail;
            coinText.text = "<color=#ff0000ff><b>ENEMY</b></color> goes <b>FIRST</b>!";
            yield return new WaitForSeconds(3f);
            battleSetupState();
        }
    }

    //POST-COIN TOSS BATTLE SETUP
    public void battleSetupState()
    {
        //COIN STUFF DISABLED
        coin.SetActive(false);
        coinText.enabled = false;
        coinTossButton.SetActive(false);
        
        //PLAYER ENABLED 
        player.SetActive(true);
        playerUI.enabled = true;
        playerUI2.enabled = true;
        playerSlider.gameObject.SetActive(true);

        //ENENMY ENABLED
        enemy.SetActive(true);
        enemyUI.enabled = true;
        enemyUI2.enabled = true;
        enemySlider.gameObject.SetActive(true);


        //UI ENABLED 
        maintextBox.enabled = true;
        pickNumber.enabled = true;

        //RANDMISE CARDS
        player_RNG.activateRNG();
        player_RNG.activateNumber();


        //IF HEADS=PLAYER GOES FIRST
        if (coinNumber == 1)
        {
            gameState = GameState.playerTurn;
            StartCoroutine(playerTurnState());
        }
        //IF TAILS =ENEMY GOES FIRST
        if (coinNumber == 2)
        {
            gameState = GameState.enemyTurn;
            
            StartCoroutine(enemyTurnState());
        }
       


    }


    

    //PLAYER GAME TURN STATE
    IEnumerator playerTurnState()
    {
        if(gameState == GameState.playerTurn)
        {
            
            //PLAYER PICKS CARD 
            yield return new WaitForSeconds(1.4f);
            maintextBox.text = "It's " + "<b><color=#0000ffff> YOUR TURN</color></b>" + ", pick <b>ONE</b>" + "<b> CARD</b>";
            pickNumber.enabled = true;

            //UI UPDATED 2 
            yield return new WaitForSeconds(4f);
            pickNumber.enabled = false;

            //BUTTONS ACTIVATED 
            yield return new WaitForSeconds(1f);
            maintextBox.text = "What do" + "<color=#0000ffff><b> YOU</b></color>" + " want to" + "<b> DO</b>?";
            playerAttackButton.SetActive(true);
            playerHealButton.SetActive(true);
            playerShieldButton.SetActive(true);
        }
       



    }


    //PLAYER ATTACK
    public void onPlayerAttackButton()
    {
        click.Play();
        if (gameState != GameState.playerTurn)
            return;
        else StartCoroutine(PlayerAttack());

      
    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(2);


        //UI UPDATED 
        maintextBox.richText = enabled;
        maintextBox.text = "<color=#0000ffff><b>PLAYER</b></color>" + "<b> ATTACKS!</b>";
       

        yield return new WaitForSeconds(2);

        //ENEMY DAMAGED 
        //DOES ENEMY HAVE SHIELD?
        //YES
        if (enemy_AI.enemyHasShield == true)
        {
            enemy_AI.enemyHasShield = false;
            enemy_AI.enemyTakesShieldDamage();

            //UI UPDATED 
            maintextBox.text = "<b><color=#ff0000ff>ENEMY </color></b>" + " <b>takes</b> " + "<b>" +
                (enemy_AI.playerDamageAttackGiven - enemy_AI.enemyShield) + "HP DAMAGE!" + "</b>" + " instead of "
                + enemy_AI.playerDamageAttackGiven;


            if (enemy_AI.enemyShield > enemy_AI.playerDamageAttackGiven)
            {
                heal.Play();
                enemyAnim.Play("Enemy_Heals");
            }
            else
            {
                damage.Play();
                enemyAnim.Play("Enemy_Damages");
            }
           



        }
        else
        {
            enemy_AI.enemyTakenDamage();

            //UI UPDATED
            maintextBox.text = "<color=#ff0000ff><b>ENEMY</b></color>" + " has taken " + "<b>" + enemy_AI.playerDamageAttackGiven.ToString() + "</b>" + "<b>HP</b>" + " DAMAGE!";
           
            damage.Play();
            enemyAnim.Play("Enemy_Damages");
        }
        




        //PLAYER CARD CHECKER
        cardNumberPlayerChose = enemy_AI.playerDamageAttackGiven; 





        //HAS ENEMY BEEN KILLED?
        if (enemy_AI.enemyCurrentHealth <= 0)
        {
          
            enemy_AI.enemyCurrentHealth = 0;
            gameState = GameState.playerWins;
            endState();
        }



    

        //ENEMY TURN CHECK 
        else 
        {
            Debug.Log("Enemy Turn State");
            
            

            if(cardNumberPlayerChose > cardNumberEnemyChose)
            {
                gameState = GameState.enemyTurn;
                StartCoroutine(enemyTurnState());
               
            }
            else 
            {
                gameState = GameState.playerTurn;
                StartCoroutine(playerTurnState());
                playerturncount++;
                if(playerturncount == 4)
                {
                    playerturncount = 0;
                    gameState = GameState.enemyTurn;
                    StartCoroutine(enemyTurnState());
                }
            }
        }

       
      
    }



    //PLAYER HEAL
    public void onPlayerHealButton()
    {
        click.Play();
        if (gameState != GameState.playerTurn)
            return;
        else StartCoroutine(PlayerHeal());
    }


    IEnumerator PlayerHeal()
    {
        yield return new WaitForSeconds(2);

        
        //UI UPDATED 
        maintextBox.text = "<b><color=#0000ffff>PLAYER</color></b>" + "<b> HEALS!</b>";

        yield return new WaitForSeconds(2);

        //PLAYER HEALED
        player_Controller.playerHealedUp();

        //UI UPDATED
        maintextBox.text = "<b><color=#0000ffff>PLAYER</color></b>" + " has" + "<b> HEALED</b>" + " by " + "<b>" + player_Controller.playerHealed.ToString()+ "</b>" + "<b> HP!</b>";
        
        //SOUND AND ANIMATION
        heal.Play();
        playerAnim.Play("Player_Heals");



        //PLAYER CARD CHECKER
        cardNumberPlayerChose = player_Controller.playerHealed;



        //HAS ENEMY BEEN KILLED?
        if (enemy_AI.enemyCurrentHealth <= 0)
        {
            enemy_AI.enemyCurrentHealth = 0;
            gameState = GameState.playerWins;
            endState();
        }

       

        //ENEMY TURN CHECK
        else
        {

            if (cardNumberPlayerChose > cardNumberEnemyChose)
            {
                gameState = GameState.enemyTurn;
                StartCoroutine(enemyTurnState());
            }
            else
            {
                gameState = GameState.playerTurn;
                StartCoroutine(playerTurnState());

                playerturncount++;
                if (playerturncount == 4)
                {
                    playerturncount = 0;
                    gameState = GameState.enemyTurn;
                    StartCoroutine(enemyTurnState());
                }
            }


        }



    }


    //PLAYER SHIELD 
    public void onPlayerShieldButton()
    {
        click.Play();
        if (gameState != GameState.playerTurn)
            return;
        else StartCoroutine(PlayerShield());
    }

    public IEnumerator PlayerShield()
    {
        yield return new WaitForSeconds(2);
        

        //UI UPDATED 
        maintextBox.text = "<b><color=#0000ffff>PLAYER</color></b>" + "<b> SHIELDS!</b>";

        yield return new WaitForSeconds(2);

        //PLAYER SHIELDS
        player_Controller.playerShielded();

        //UI UPDATED
        maintextBox.text = "<b><color=#0000ffff>PLAYER</color></b>" + " will " + "take " + "<b>" +player_Controller.playerShield + "</b>" 

            +"<b> LESS DAMAGE </b> the next time the " + "<b><color=#ff0000ff>ENEMY</color> ATTACKS!</b>";
       
        yield return new WaitForSeconds(2.5f);



        //PLAYER CARD CHECKER
        cardNumberPlayerChose = player_Controller.playerHealed;



        //HAS ENEMY BEEN KILLED?
        if (enemy_AI.enemyCurrentHealth <= 0)
        {
            enemy_AI.enemyCurrentHealth = 0;
            gameState = GameState.playerWins;
            endState();
        }

       
        //ENEMY TURN CHECK
        else
        {

            if (cardNumberPlayerChose > cardNumberEnemyChose)
            {
                gameState = GameState.enemyTurn;
                StartCoroutine(enemyTurnState());
            }
            else
            {
                gameState = GameState.playerTurn;
                StartCoroutine(playerTurnState());

                playerturncount++;
                if (playerturncount ==4)
                {
                    playerturncount = 0;
                    gameState = GameState.enemyTurn;
                    StartCoroutine(enemyTurnState());
                }
            }


        }

      
    }

    //ENEMY TURN STATE 
    IEnumerator enemyTurnState()
    {
        playerturncount = 0;

        enemy_RNG.generateRNGAttackHealStateNumber();
        enemy_RNG.generateEnemyCardPosition();
        enemy_RNG.generateEnemyAttackHealStatNumber();
        

        //PLAYER BUTTONS DISABLED
        playerAttackButton.SetActive(false);
        playerHealButton.SetActive(false);
        playerShieldButton.SetActive(false);
        pickNumber.enabled = false;

        yield return new WaitForSeconds(2);
        maintextBox.text = "It's the" + "<b><color=#ff0000ff> ENEMY'S TURN</color></b>"; 

        yield return new WaitForSeconds(2);

        //ENEMY ATTACK
        if (enemy_RNG.enemyRNGAttackHealStateNumber == 1)
        {
            
            enemy_RNG.generateEnemyAttackHealStatNumber();
            
            
            //UI UPDATED
            maintextBox.text = "<b><color=#ff0000ff>ENEMY</color></b>" + "<b> ATTACKS!</b>";
            Debug.Log("Enemy Attacks");


            yield return new WaitForSeconds(2);


            //PLAYER DAMAGED DOES PLAYER HAVE SHIELD?
            //YES
            if (player_Controller.playerHasShield ==true)
            {
                player_Controller.playerTakesShieldDamage();
                player_Controller.playerHasShield = false;
                

                //UI UPDATED 
                maintextBox.text = "<b><color=#0000ffff>PLAYER </color></b>" + " <b>takes</b> " + "<b>" +
                    (player_Controller.enemyDamageGiven - player_Controller.playerShield) + "HP DAMAGE!" + "</b>" + " instead of " 
                    + player_Controller.enemyDamageGiven;
               
               

                if (player_Controller.playerShield > player_Controller.enemyDamageGiven)
                {
                    heal.Play();
                    playerAnim.Play("Player_Heals");
                }
                else
                {
                    damage.Play();
                    playerAnim.Play("Player_Damages");
                }

            }
            //NO
            else
            {
                
                player_Controller.playerTakenDamage();
                maintextBox.text = "<b><color=#0000ffff>PLAYER </color></b>" + " has taken " + "<b>" + player_Controller.enemyDamageGiven + "HP DAMAGE!" + "</b>";
                
                damage.Play();
                playerAnim.Play("Player_Damages");
            }

           





            //ENEMY CARD CHECKER 
            cardNumberEnemyChose = enemy_RNG.enemyAttackHealStatNumber;






            //IS PLAYER DEAD?
            if (player_Controller.playerCurrentHealth <= 0)
            {
                player_Controller.playerCurrentHealth = 0;
                gameState = GameState.playerLost;
                endState();
            }
            
            //PLAYER TURN CHECK
            else
            {
                yield return new WaitForSeconds(2f);

                yield return new WaitForSeconds(2f);
                if (cardNumberEnemyChose >= cardNumberPlayerChose)
                {
                    gameState = GameState.playerTurn;
                    StartCoroutine(playerTurnState());
                }
                else
                {


                    gameState = GameState.enemyTurn;
                    StartCoroutine(enemyTurnState());


                }


            }




        }
        //ENEMY HEALS
        else if (enemy_RNG.enemyRNGAttackHealStateNumber == 2)
        {
            
            enemy_RNG.generateEnemyAttackHealStatNumber();

            //UI UPDATED
            maintextBox.text = "<b><color=#ff0000ff>ENEMY</color></b>" + "<b> HEALS!</b>";
            Debug.Log("Enemy HEALS");

            yield return new WaitForSeconds(2);
            //ENEMY HEALS 
            enemy_AI.enemyHealedUp();

            //UI UPDATED 
            maintextBox.text = "<b><color=#ff0000ff>ENEMY</color></b>" + " has" +"<b> HEALED</b>" + " by " + "<b>" + enemy_AI.enemyHealed + "HP" +"</b>";
            enemyUI.text = enemy_AI.enemyCurrentHealth + "/" + enemy_AI.enemyMaxHealth;
            
            
            //SOUND AND ANIMATION
            heal.Play();
            enemyAnim.Play("Enemy_Heals");






            //ENEMY CARD CHECKER 
            cardNumberEnemyChose = enemy_RNG.enemyAttackHealStatNumber;






            //IS PLAYER DEAD?
            if (player_Controller.playerCurrentHealth <= 0)
            {
                Debug.Log("Player Dead");
                gameState = GameState.playerLost;
                endState();
            }

           

            //PLAYER TURN CHECK
            else
            {
                yield return new WaitForSeconds(2f);
                if (cardNumberEnemyChose >= cardNumberPlayerChose)
                {
                    gameState = GameState.playerTurn;
                    StartCoroutine(playerTurnState());
                }
                else
                {
                    
                    
                        gameState = GameState.enemyTurn;
                        StartCoroutine(enemyTurnState());
                   
                    
                }


            }
           





        }


        //ENEMY SHIELDS 
        else if (enemy_RNG.enemyRNGAttackHealStateNumber == 3)
        {

            enemy_RNG.generateEnemyAttackHealStatNumber();
            
            //UI UPDATED
            maintextBox.text = "<b><color=#ff0000ff>ENEMY</color></b>" + "<b> SHIELDS!</b>";
            Debug.Log("Enemy Shields");

            yield return new WaitForSeconds(2);

            //ENEMY SHIELDS
            enemy_AI.enemyShielded();

            //UI UPDATED
            maintextBox.text = "<b><color=#ff0000ff>ENEMY</color></b>" + " will " + "take " + "<b>" + enemy_AI.enemyShield + "</b>"

                + "<b> LESS DAMAGE </b> the next time the " + "<b><color=#0000ffff>PLAYER</color> ATTACKS!</b>";

            yield return new WaitForSeconds(2.5f);

            //ENEMY CARD CHECKER
            print(cardNumberPlayerChose);
           
            cardNumberEnemyChose = enemy_AI.enemyShield;


            //IS PLAYER DEAD?
            if (player_Controller.playerCurrentHealth <= 0)
            {
                player_Controller.playerCurrentHealth = 0;
                gameState = GameState.playerLost;
                endState();
            }

            
            //PLAYER TURN CHECK
            else
            {
                yield return new WaitForSeconds(2f);

                
                StartCoroutine(playerTurnState());
                yield return new WaitForSeconds(2f);
                if (cardNumberEnemyChose >= cardNumberPlayerChose)
                {
                    gameState = GameState.playerTurn;
                    StartCoroutine(playerTurnState());
                }
                else
                {


                    gameState = GameState.playerTurn;
                    StartCoroutine(playerTurnState());


                }


            }
            
        }

    }





    //END OF THE GAME 
    void endState()
    {
       
        if(gameState == GameState.playerWins)
        {
            happy.Play();
            maintextBox.text = "<b><color=#0000ffff>PLAYER</color></b>" + "<b> WINS!</b>";

            playerAttackButton.SetActive(false);
            playerHealButton.SetActive(false);
            playerShieldButton.SetActive(false);
            randomiseButton.SetActive(false);
        }

        
        if (gameState == GameState.playerLost)
        {
            sad.Play();
            maintextBox.text = "<b><color=#ff0000ff>ENEMY</color></b>" +  "<b> WINS!</b>";

            playerAttackButton.SetActive(false);
            playerHealButton.SetActive(false);
            playerShieldButton.SetActive(false);
            randomiseButton.SetActive(false);
           
        }

        if (gameState == GameState.draw)
        {
            Debug.Log("Draw");
            maintextBox.text = "THE GAME IS A <b>DRAW!</b>";

            playerAttackButton.SetActive(false);
            playerHealButton.SetActive(false);
            playerShieldButton.SetActive(false);
            randomiseButton.SetActive(false);

        }
      
    }

 
}
