using System;
using GamePlay;
using Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        
        [FormerlySerializedAs("collisionsAndTriggersCheck")]
        [FormerlySerializedAs("playerDeath")]
        [Header("REFERENCES")]
        [SerializeField]private CheckForCollisionsAndTriggers collisionsAndTriggersEvents;
        [SerializeField]private PlayerController playerController;
        
        [Header("AUDIO")]
        [SerializeField]private AudioSource audioSource;

        [SerializeField] private AudioClip landSound;
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip deathSound;
        [SerializeField] private AudioClip nextLevelAudio;

       

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            playerController = FindObjectOfType<PlayerController>();
            collisionsAndTriggersEvents = playerController.gameObject.GetComponent<CheckForCollisionsAndTriggers>();

            playerController.OnPlayerLand += PlayLandAudio;
            playerController.OnPlayerJump += PlayerJumpAudio;

            collisionsAndTriggersEvents.OnPlayerDeath += PlayDeathAudio;
            collisionsAndTriggersEvents.OnPlayerLevelChange += PlayLevelAudio;



        }

        private void PlayLevelAudio()
        {
            audioSource.PlayOneShot(nextLevelAudio);
        }

        private void PlayDeathAudio()
        {
            audioSource.PlayOneShot(deathSound);
        }

        private void PlayerJumpAudio()
        {
            audioSource.PlayOneShot(jumpSound);
        }

        private void PlayLandAudio()
        {
            audioSource.PlayOneShot(landSound);
        }
        
    }
}
