
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
        
        [FormerlySerializedAs("playerDeathScriptRef")]
        [Header("REFERENCES")]
        [SerializeField] private CheckForCollisionsAndTriggers collisionsAndTriggersCheck;
        [SerializeField] private Animator transitionAnimator;
        
        [Tooltip("Reloads the same scene when the player enters the next level trigger.")]
        [SerializeField]
        private bool alwaysReloadSameScene;

        [Header("SCENE INFO")] 
        [SerializeField] private bool isTestScene;
        [FormerlySerializedAs("currentScene")] [SerializeField] private int currentSceneBuildIndex;

        

        private void Start()
        {
            collisionsAndTriggersCheck = FindObjectOfType<CheckForCollisionsAndTriggers>();
            transitionAnimator = GameObject.Find("Image").GetComponent<Animator>();

            currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

            collisionsAndTriggersCheck.OnPlayerDeath += ReloadSceneWhenPlayerDies;
            
            if (alwaysReloadSameScene)
            {
                collisionsAndTriggersCheck.OnPlayerLevelChange += ReloadSceneWhenPlayerDies;
            }
            else
            {
                collisionsAndTriggersCheck.OnPlayerLevelChange += MovePlayerToNextLevel;
            }
        }

        private void MovePlayerToNextLevel()
        {
            StartCoroutine(LoadScene(currentSceneBuildIndex + 1));
        }


        private void ReloadSceneWhenPlayerDies()
        {
            StartCoroutine(LoadScene(currentSceneBuildIndex));
        }

        

        private IEnumerator LoadScene(int buildIndex)
        {
            transitionAnimator.Play("CrossFadeIn");
            yield return new WaitForSeconds(1);
            
            SceneManager.LoadScene(buildIndex);
        }
    }
}
