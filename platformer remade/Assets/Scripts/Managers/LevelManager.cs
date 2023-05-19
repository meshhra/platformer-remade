
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
       
        [Header("REFERENCES")]
        [SerializeField] private PlayerDeath playerDeath;
        [SerializeField] private PlayerMoveLevels playerMoveLevels;
        [SerializeField] private Animator transitionAnimator;
        
        [Header("SCENE INFO")]
        [FormerlySerializedAs("currentScene")] [SerializeField] private int currentSceneBuildIndex;

        

        private void Start()
        {
            playerDeath = FindObjectOfType<PlayerDeath>();
            playerMoveLevels = FindObjectOfType<PlayerMoveLevels>();
            transitionAnimator = GameObject.Find("Image").GetComponent<Animator>();

            currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

            playerDeath.OnPlayerDeath += ReloadSceneWhenPlayerDies;
            playerMoveLevels.OnPlayerLeverChange += MovePlayerToNextLevel;
        }

        private void MovePlayerToNextLevel(object sender, EventArgs e)
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
