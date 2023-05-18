using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GamePlay
{
    /// <summary>
    /// this script checks of the player has entered the nextLevelTrigger,
    /// and if the player enters the trigger IsMoveToNextLevel is set to true;
    /// </summary>
    public class PlayerMoveLevels : MonoBehaviour
    {
        public bool IsMoveToNextLevel { get; private set; }
        public event EventHandler OnPlayerLeverChange;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("nextLevelTrigger"))
            {
                IsMoveToNextLevel = true;
                OnPlayerLeverChange?.Invoke(this,EventArgs.Empty);
            }
        }
    }
}
