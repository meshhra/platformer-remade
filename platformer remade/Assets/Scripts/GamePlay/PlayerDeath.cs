
using UnityEngine;

namespace GamePlay
{
    public class PlayerDeath : MonoBehaviour
    {
        public bool IsPlayerDead { get; private set; }
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag("trap")) return;
            Destroy(gameObject);
            IsPlayerDead = true;
        }
    }
}
