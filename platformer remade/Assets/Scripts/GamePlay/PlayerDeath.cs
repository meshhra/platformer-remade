
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GamePlay
{
    public class PlayerDeath : MonoBehaviour
    {
        public bool IsPlayerDead { get; private set; }
        public event Action OnPlayerDeath; 
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag("trap")) return;
            Destroy(gameObject);
            IsPlayerDead = true;
            OnPlayerDeath?.Invoke();
        }
    }
}
