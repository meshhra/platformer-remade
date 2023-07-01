using System;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;

        private void Start()
        {
            if (levelManager == null)
            {
                levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
            }
        }

        public void OnPlayButton()
        {
            StartCoroutine(levelManager.LoadScene(0));
        }
    
    
    }
}
