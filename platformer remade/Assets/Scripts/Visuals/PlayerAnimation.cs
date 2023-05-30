using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay;
using Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Visuals
{
    public class PlayerAnimation : MonoBehaviour
    {
        
        [Header("REFERENCES")] 
        [SerializeField] private CheckForCollisionsAndTriggers collisionAndTriggerEvents;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Animator playerAnimator;

        private void Start()
        {
            playerAnimator = GetComponent<Animator>();
            collisionAndTriggerEvents = GetComponent<CheckForCollisionsAndTriggers>();
            playerController = GetComponent<PlayerController>();
            
            playerController.OnPlayerJump += PlayJumpAnimation;
            playerController.OnPlayerLand += PlayLandAnimation;
            
            //just testing 
            playerController.OnPlayerJump += StartTimer;

        }

        private void Update()
        {
            if (start)
            {
                timer += Time.deltaTime;
            }
        }

        private void PlayJumpAnimation()
        {
            playerAnimator.CrossFade("Player Jump Up", 0);
        }

        private void PlayLandAnimation()
        {
            playerAnimator.CrossFade("Player Land", 0);
        }

        public void PlayIdleAnimation()
        {
            playerAnimator.CrossFade("Player Idle", 0);
        }

        private void PrintJumpApex()
        {
            start = false;  //just test
            print("top" + timer);//just tet
        }

        public bool start;
        public float timer;
        private void StartTimer()
        {
            timer = 0;
            start = true;
        }
        
        

    }
}
