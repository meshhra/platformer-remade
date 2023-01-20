using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int _startingHealth;
    public float currentHealth;
    
    //public bool isdeathscreenactive;
    void Start()
    {
        currentHealth = _startingHealth;
        
        
        //uI = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //isdeathscreenactive = uI.isDeathScreenActive;
        
        if (currentHealth <=0)
        {
            isPlayerDead = true;
            
            currentHealth = 0;
        }
        else if (currentHealth > 0)
        {
            isPlayerDead = false;
            
        }
        CalculateDamage();
        
        
    }

    
    public bool isDamageTaken;
    public string collisionTag;

    public UIManager uI;
    public bool isPlayerDead;
    void CalculateDamage()
    {
        if(isDamageTaken)
        {
            if (collisionTag == "Spike")
            {
                //Debug.Log("skipe lagi bkl tko");
                currentHealth -= _startingHealth + 20 * Time.deltaTime ;
                
            }
            
        }
        
    }


    void OnCollisionEnter2D(Collision2D collision2D)
    {
        //Debug.Log("Enterd collison with "+collision2D.gameObject.name);
        collisionTag = collision2D.gameObject.tag;
        if(collision2D.gameObject.layer == 6)
        {
            
            isDamageTaken = true;  
        }
        else if (collision2D.gameObject.layer != 6)
        {
            isDamageTaken = false;
        }
        else if(collision2D.gameObject == null)
        {
            isDamageTaken = false;
        }
        else{
            
            isDamageTaken = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision2D)
    {
        //Debug.Log("Enterd collison with "+collision2D.gameObject.name);
        collisionTag = collision2D.gameObject.tag;
        if(collision2D.gameObject.layer == 6)
        {
            
            isDamageTaken = true;  
        }
        else if (collision2D.gameObject.layer != 6)
        {
            isDamageTaken = false;
        }
        else if(collision2D.gameObject == null)
        {
            isDamageTaken = false;
        }
        else{
            
            isDamageTaken = false;
        }
    }



    void OnCollisionExit2D(Collision2D collision2D)
    {
        //Debug.Log("kood gaya shabas");
        //Debug.Log("exited collision with "+collision2D.gameObject.name);
        collisionTag = null;
        isDamageTaken = false;
    }
    

}    
    
