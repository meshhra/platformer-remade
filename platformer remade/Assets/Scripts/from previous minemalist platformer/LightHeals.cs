using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHeals : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Health playerHealth;
    public bool heal;

    public Animator anim;
    void Start()
    {
        anim = GameObject.Find("CM StateDrivenCamera1").GetComponent<Animator>();
        playerHealth = gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heal)
        {
            playerHealth.currentHealth += 10 * Time.deltaTime;
        }
        if (playerHealth.currentHealth > playerHealth._startingHealth)
        {
            playerHealth.currentHealth = playerHealth._startingHealth;

        }
    }
    public string s;
    void OnTriggerEnter2D(Collider2D colli)
    {
        if(colli.gameObject.tag == "Light Trigger" && !playerHealth.isPlayerDead)
        {
            heal = true;
        }

        if (colli.gameObject.tag == "Vcam1 Trigger")
        {
            s = "vcam1";
            anim.Play("vcam1");       
        }
        if (colli.gameObject.tag == "Vcam2 Trigger")
        {
            s = "vcam2";
            anim.Play("vcam2");
        }
        
    }

    void OnTriggerExit2D(Collider2D colli)
    {
        if (colli.gameObject.tag == "Light Trigger")
        {
            heal = false;
        }
    }
}
