using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpikes : MonoBehaviour
{

    public bool isPlayerDead = false;

    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.collider.gameObject.CompareTag("spike"))
        {
            Debug.Log("mrr gaya");
            isPlayerDead = true;
            Destroy(player);
        }
    }
}
