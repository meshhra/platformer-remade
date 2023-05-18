using System;
using GamePlay;
using Movement;
using UnityEngine;
using UnityEngine.Audio;
namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        private PlayerDeath playerDeath;
        private PlayerController playerController;
        private PlayerMoveLevels playerMoveLevels;
        
        [Header("AUDIO")]
        [SerializeField]private AudioSource audioSource;

        [SerializeField] private AudioClip landSound;
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip deathSound;
        [SerializeField] private AudioClip nextLevelAudio;

        private void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
            playerDeath = playerController.gameObject.GetComponent<PlayerDeath>();
            playerMoveLevels = playerController.gameObject.GetComponent<PlayerMoveLevels>();

            playerController.OnPlayerLand += PlayLandAudio;
            playerController.OnPlayerJump += PlayerJumpAudio;

            playerDeath.OnPlayerDeath += PlayDeathAudio;
            playerMoveLevels.OnPlayerLeverChange += PlayLevelAudio;

        }

        private void PlayLevelAudio(object sender, EventArgs e)
        {
            audioSource.PlayOneShot(nextLevelAudio);
        }

        private void PlayDeathAudio()
        {
            audioSource.PlayOneShot(deathSound);
        }

        private void PlayerJumpAudio(object sender, EventArgs e)
        {
            audioSource.PlayOneShot(jumpSound);
        }

        private void PlayLandAudio(object sender, EventArgs e)
        {
            audioSource.PlayOneShot(landSound);
        }
        
    }
}
