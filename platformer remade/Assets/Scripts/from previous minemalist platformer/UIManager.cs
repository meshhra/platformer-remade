using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    

    public TextMeshProUGUI _text;
    void Start()
    {
        _text = GameObject.Find("Health").GetComponent<TextMeshProUGUI>();
        deathScreen = GameObject.Find("Death Screen");
        
        health = GameObject.Find("Player").GetComponent<Health>();
        playerMovement = health.gameObject.GetComponent<PlayerMovement>();

        _activeScene = SceneManager.GetActiveScene();
    }

    public GameObject deathScreen;
    public GameObject menu; 

    public Health health;
    public PlayerMovement playerMovement;

    public float PlayerHealth;
    public bool isDeathScreenActive;
    
    public bool isPlayerMovementActive;
    public bool _isPlayerDead; 
    void Update()
    {
        PlayerHealth = health.currentHealth;
        _isPlayerDead = health.isPlayerDead;

        _text.text = "Health : "+ ((int)health.currentHealth).ToString();
        if (PlayerHealth <=0)
        {
            isDeathScreenActive = true;
        }
        else if (PlayerHealth > 0)
        {
            isDeathScreenActive = false;
        }
    }
    void LateUpdate()
    {
        //activate the Death Screen if the isDeathScreenActive boolean is true
        if (isDeathScreenActive)
        {
            deathScreen.SetActive(true);
        }
        else if (!isDeathScreenActive)
        {
            deathScreen.SetActive(false);  
        }

        


        if (isDeathScreenActive )
        {
            isPlayerMovementActive = false;
        }
        else{
            isPlayerMovementActive = true;
        }
        
        //enable the playerMovement script if the isPlayerMovementAcive boolean true
        if (isPlayerMovementActive)
        {
            playerMovement.enabled = true;
        }
        else if (!isPlayerMovementActive)
        {
            playerMovement.enabled = false;
        }
        
    }

    

    private Scene _activeScene;
    public void OnRetryButton()
    {
        SuplimentHealh();
    }

    void SuplimentHealh()
    {
        SceneManager.LoadScene(_activeScene.name);
        
    }
}
