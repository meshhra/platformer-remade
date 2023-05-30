using System;
using GamePlay;
using UnityEngine;
using UnityEngine.Serialization;

namespace Visuals
{
    public class PlayerAnimation : MonoBehaviour
    {
        [FormerlySerializedAs("collisionsAndTriggers")]
        [Header("REFERENCES")] 
        [SerializeField] private CheckForCollisionsAndTriggers collisionAndTriggerEvents;

        private void Start()
        {
            collisionAndTriggerEvents = GetComponent<CheckForCollisionsAndTriggers>();
            
        }
    }
}
