using System;
using UnityEngine;

namespace Visuals.Parallax
{
    [ExecuteInEditMode]
    public class ParallaxCamera : MonoBehaviour 
    {
        public delegate void ParallaxCameraDelegate(float deltaMovement);
        public ParallaxCameraDelegate OnCameraTranslate;
        private float oldPosition;

        private void Start()
        {
            oldPosition = transform.position.x;
        }
        
        private const float TOLERANCE = 0.01f;

        private void Update()
        {
            if (!(Math.Abs(transform.position.x - oldPosition) > TOLERANCE)) return;
            if (OnCameraTranslate != null)
            {
                var _delta = oldPosition - transform.position.x;
                OnCameraTranslate(_delta);
            }
            oldPosition = transform.position.x;
        }

        
    }
}
