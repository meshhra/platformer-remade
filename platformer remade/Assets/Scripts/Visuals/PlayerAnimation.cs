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
        
        

    }
}
