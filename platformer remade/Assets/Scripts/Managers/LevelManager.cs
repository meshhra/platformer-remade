
using GamePlay;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]private PlayerDeath playerDeath;
        
        private void Start()
        {
            playerDeath = FindObjectOfType<PlayerDeath>();
        }

        private void Update()
        {
            if (playerDeath.IsPlayerDead)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
