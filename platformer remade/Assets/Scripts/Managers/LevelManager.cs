
using GamePlay;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]private PlayerDeath playerDeath;
        [SerializeField]private bool isStartTimer;
        [SerializeField]private float timer;

        private void Start()
        {
            playerDeath = FindObjectOfType<PlayerDeath>();
        }

        private void Update()
        {
            ReloadSceneWhenPlayerDies();
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
