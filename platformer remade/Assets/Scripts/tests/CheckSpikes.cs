using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpikes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.CompareTag("spike"))
        {
            Debug.Log("mrr gaya");
        }
    }
}
