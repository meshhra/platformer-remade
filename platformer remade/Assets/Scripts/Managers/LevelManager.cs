
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
        
        [FormerlySerializedAs("collisionsAndTriggersCheck")]
        [FormerlySerializedAs("playerDeathScriptRef")]
        [Header("REFERENCES")]
        [SerializeField] private CheckForCollisionsAndTriggers collisionsAndTriggersEvents;
        [SerializeField] private Animator transitionAnimator;
        
        [Tooltip("Reloads the same scene when the player enters the next level trigger.")]
        [SerializeField]
        private bool alwaysReloadSameScene;

        [Header("SCENE INFO")] 
        [SerializeField] private bool isTestScene;
        [FormerlySerializedAs("currentScene")] [SerializeField] private int currentSceneBuildIndex;

        

        private void Start()
        {
            collisionsAndTriggersEvents = FindObjectOfType<CheckForCollisionsAndTriggers>();
            transitionAnimator = GameObject.Find("Image").GetComponent<Animator>();

            currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

            collisionsAndTriggersEvents.OnPlayerDeath += ReloadSceneWhenPlayerDies;
            
            if (alwaysReloadSameScene)
            {
                collisionsAndTriggersEvents.OnPlayerLevelChange += ReloadSceneWhenPlayerDies;
            }
            else
            {
                collisionsAndTriggersEvents.OnPlayerLevelChange += MovePlayerToNextLevel;
            }
        }

        private void MovePlayerToNextLevel()
        {
            StartCoroutine(LoadScene(currentSceneBuildIndex + 1));
        }


        private void ReloadSceneWhenPlayerDies()
        {
            StartCoroutine(LoadScene(currentSceneBuildIndex, 0.5f));
        }

        

        public IEnumerator LoadScene(int buildIndex)
        {
            
            transitionAnimator.Play("CrossFadeIn");
            yield return new WaitForSeconds(1);
            
            SceneManager.LoadScene(buildIndex);
        }
        
        public IEnumerator LoadScene(int buildIndex, float loadDelay)
        {
            yield return new WaitForSeconds(loadDelay);
            transitionAnimator.Play("CrossFadeIn");
            yield return new WaitForSeconds(1);
            
            SceneManager.LoadScene(buildIndex);
        }
    }
}
