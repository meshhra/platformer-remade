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
        private CheckForCollisionsAndTriggers collisionsAndTriggersEvents;

        private Animator fallingBlockAnimator;
        private AudioSource audioSource;
        [FormerlySerializedAs("boxVibrateSound")] [SerializeField] private AudioClip blockVibrateSound;
        [FormerlySerializedAs("boxLandSound")] [SerializeField] private AudioClip blockLandSound;

        public event Action OnBlockLand;

        private void Start()
        {
            fallingBlockAnimator = GetComponent<Animator>();
            audioSource = gameObject.AddComponent<AudioSource>();
            collisionsAndTriggersEvents = GameObject.Find("Player Character").
                GetComponent<CheckForCollisionsAndTriggers>();
            blockRigidbody2D = GetComponent<Rigidbody2D>();
            blockRigidbody2D.gravityScale = 0;
            blockRigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition | 
                                           RigidbodyConstraints2D.FreezeRotation;

            collisionsAndTriggersEvents.OnPlayerEnterFallBlockTrigger += StartFalling;
            collisionsAndTriggersEvents.OnPlayerEnterFallBlockTrigger += PlayVibrationAnimationAndSound;

            OnBlockLand += PlayBlockLandSound;
            OnBlockLand += PlayBlockLandAnimation;
        }

        private void PlayBlockLandAnimation()
        {
            fallingBlockAnimator.CrossFade("Block Land",0 );
        }

        private void PlayBlockLandSound()
        {
            audioSource.Stop();
            audioSource.PlayOneShot(blockLandSound);
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
            }
        }

        private void PlayVibrationAnimationAndSound()
        {
            fallingBlockAnimator.Play("Block Vibrate");
            audioSource.PlayOneShot(blockVibrateSound);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.layer == 4) // water layer is ground for now hence 4
            {
                OnBlockLand?.Invoke();
            }
        }
    }
}
