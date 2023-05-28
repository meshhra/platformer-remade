using System;
using System.Collections;
using UnityEngine;

namespace GamePlay
{
    public class FallingBlock : MonoBehaviour
    {
        [SerializeField]private Rigidbody2D blockRigidbody2D;
        [SerializeField]private float gravityScale;
        private PlayerDeath playerDeath;

        private void Start()
        {
            playerDeath = GameObject.Find("Player Character").GetComponent<PlayerDeath>();
            blockRigidbody2D = GetComponent<Rigidbody2D>();
            blockRigidbody2D.gravityScale = 0;

            playerDeath.OnPlayerEnterFallBlockTrigger += StartFalling;

        }

        public float waitTime = 10;

        private void StartFalling()
        {
            StartCoroutine(FallingCoroutine());
        }
        
        private IEnumerator FallingCoroutine()
        {
            yield return new WaitForSeconds(waitTime);
            blockRigidbody2D.gravityScale = gravityScale;
        }
    }
}
