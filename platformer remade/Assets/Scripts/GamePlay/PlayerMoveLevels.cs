using System;
using UnityEngine;

namespace GamePlay
{
    public class PlayerMoveLevels : MonoBehaviour
    {
        public bool IsMoveToNextLevel { get; private set; }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("nextLevelTrigger"))
            {
                IsMoveToNextLevel = true;
            }
        }
    }
}
