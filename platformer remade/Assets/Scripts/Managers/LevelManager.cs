
using GamePlay;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private PlayerDeath playerDeath;
        [SerializeField] private PlayerMoveLevels playerMoveLevels;
        [SerializeField] private bool isStartTimer;
        [SerializeField] private float timer;

        [SerializeField] private int currentScene;

        private void Start()
        {
            playerDeath = FindObjectOfType<PlayerDeath>();
            playerMoveLevels = FindObjectOfType<PlayerMoveLevels>();

            currentScene = SceneManager.GetActiveScene().buildIndex;
        }

        private void Update()
        {
            ReloadSceneWhenPlayerDies();

            if (playerMoveLevels.IsMoveToNextLevel)
            {
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

                        SceneManager.LoadScene(currentScene + 1);
                        break;
                }
            }
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
                    
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
            }
        }
    }
}
