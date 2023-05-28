using System;
using UnityEngine;

namespace GamePlay
{
    public class FallingBlock : MonoBehaviour
    {
        private Rigidbody2D blockRigidbody2D;
        [SerializeField]private float gravityScale;
        private PlayerDeath playerDeath;

        private void Start()
        {
            playerDeath = GameObject.Find("Player").GetComponent<PlayerDeath>();
            blockRigidbody2D = GetComponent<Rigidbody2D>();
            blockRigidbody2D.gravityScale = 0;

            playerDeath.OnPlayerEnterFallBlockTrigger += StartFalling;

        }

        private void StartFalling()
        {
            blockRigidbody2D.gravityScale = gravityScale;
        }
    }
}
