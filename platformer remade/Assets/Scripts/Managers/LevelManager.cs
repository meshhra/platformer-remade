
using System;
using System.Collections;
using GamePlay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
       
        
        [SerializeField] private PlayerDeath playerDeath;
        private Rigidbody2D playerRigidBody2D;
        [SerializeField] private PlayerMoveLevels playerMoveLevels;
        [SerializeField] private bool isStartTimer;
        [SerializeField] private float timer;

        [SerializeField] private Animator transitionAnimator;

        [FormerlySerializedAs("currentScene")] [SerializeField] private int currentSceneBuildIndex;

        

        private void Start()
        {
            playerDeath = FindObjectOfType<PlayerDeath>();
            playerMoveLevels = FindObjectOfType<PlayerMoveLevels>();
            playerRigidBody2D = playerDeath.gameObject.GetComponent<Rigidbody2D>();

            currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        }

        private void Update()
        {
            ReloadSceneWhenPlayerDies();
            MovePlayerToNextLevel();
        }

        private void ReloadSceneWhenPlayerDies()
        {
            if (!playerDeath.IsPlayerDead) return;
            StartCoroutine(LoadScene(currentSceneBuildIndex));
        }

        private void MovePlayerToNextLevel()
        {
            if (!playerMoveLevels.IsMoveToNextLevel) return;
            StartCoroutine(LoadScene(currentSceneBuildIndex + 1));
        }

        private IEnumerator LoadScene(int buildIndex)
        {
            transitionAnimator.Play("CrossFadeIn");
            yield return new WaitForSeconds(1);
            
            SceneManager.LoadScene(buildIndex);
        }
    }
}
