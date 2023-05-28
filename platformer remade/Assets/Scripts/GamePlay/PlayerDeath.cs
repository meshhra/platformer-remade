
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GamePlay
{
    public class PlayerDeath : MonoBehaviour
    {
        public event Action OnPlayerDeath;
        public event Action OnPlayerEnterFallBlockTrigger;
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag("trap")) return;
            Destroy(gameObject);
            OnPlayerDeath?.Invoke();
           
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            
            if (col.gameObject.CompareTag("fallingBlockTrigger"))
            {
                OnPlayerEnterFallBlockTrigger?.Invoke();
            }

            if (!col.gameObject.CompareTag("trap")) return;
            Destroy(gameObject);//change this in future.
            OnPlayerDeath?.Invoke();
        }
        
        
    }
}
