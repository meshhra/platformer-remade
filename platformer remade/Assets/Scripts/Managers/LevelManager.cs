
using GamePlay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private PlayerDeath playerDeath;
        [SerializeField] private PlayerMoveLevels playerMoveLevels;
        [SerializeField] private bool isStartTimer;
        [SerializeField] private float timer;

        [FormerlySerializedAs("currentScene")] [SerializeField] private int currentSceneBuildIndex;

        private void Start()
        {
            playerDeath = FindObjectOfType<PlayerDeath>();
            playerMoveLevels = FindObjectOfType<PlayerMoveLevels>();

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
            if (!isStartTimer)
            {
                timer = 0;
                isStartTimer = true;
            }

            switch (isStartTimer)
            {
                case true when timer < 1:
                    timer += Time.deltaTime;
                    break;
                case true when timer > 1:
                    isStartTimer = false;
                    timer = 0;
                    
                    SceneManager.LoadScene(currentSceneBuildIndex);
                    break;
            }
        }

        private void MovePlayerToNextLevel()
        {
            if (!playerMoveLevels.IsMoveToNextLevel) return;
            if (!isStartTimer)
            {
                timer = 0;
                isStartTimer = true;
            }

            switch (isStartTimer)
            {
                case true when timer < 0.5f:
                    timer += Time.deltaTime;
                    break;
                case true when timer > 0.5f:
                    isStartTimer = false;
                    timer = 0;
                    
                    SceneManager.LoadScene(currentSceneBuildIndex + 1);
                    break;
            }
        }
    }
}
