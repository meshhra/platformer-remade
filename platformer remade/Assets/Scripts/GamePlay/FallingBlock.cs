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
            blockRigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

            playerDeath.OnPlayerEnterFallBlockTrigger += StartFalling;
        }

        public float waitTime = 10;
        public float timer ;

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
            if (fall)
            {
                blockRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | 
                                               RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                blockRigidbody2D.gravityScale = gravityScale;
            }
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
