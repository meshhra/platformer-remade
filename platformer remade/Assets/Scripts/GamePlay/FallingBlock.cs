using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

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
        public float timer = 0;

        public bool fall;
        [FormerlySerializedAs("doStartFalling")] [SerializeField] private bool startTimer;

        private void StartFalling()
        {
            startTimer = true;
            timer = 0;
        }

        private void Update()
        {
            if (!startTimer) return;
            if (fall) blockRigidbody2D.gravityScale = gravityScale;
            if (timer < waitTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                fall = true;
                print("fall");
            }
        }

        
    }
}
