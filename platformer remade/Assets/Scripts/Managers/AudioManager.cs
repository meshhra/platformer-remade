using System;
using GamePlay;
using Movement;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        
        [Header("REFERENCES")]
        [SerializeField]private PlayerDeath playerDeath;
        [SerializeField]private PlayerController playerController;
        [SerializeField]private PlayerMoveLevels playerMoveLevels;
        
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
            playerDeath = playerController.gameObject.GetComponent<PlayerDeath>();
            playerMoveLevels = playerController.gameObject.GetComponent<PlayerMoveLevels>();

            playerController.OnPlayerLand += PlayLandAudio;
            playerController.OnPlayerJump += PlayerJumpAudio;

            playerDeath.OnPlayerDeath += PlayDeathAudio;
            playerMoveLevels.OnPlayerLeverChange += PlayLevelAudio;
            
           

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
