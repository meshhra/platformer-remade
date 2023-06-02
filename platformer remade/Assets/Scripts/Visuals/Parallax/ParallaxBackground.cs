using System.Collections.Generic;
using UnityEngine;

namespace Visuals.Parallax
{
    [ExecuteInEditMode]
    public class ParallaxBackground : MonoBehaviour
    {
        public ParallaxCamera parallaxCamera;
        [SerializeField]private List<ParallaxLayer> parallaxLayers;

        private void Start()
        {
            if (parallaxCamera == null)
            {
                if (Camera.main != null)
                {
                    parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();
                }
            }
            if (parallaxCamera != null)
                parallaxCamera.OnCameraTranslate += Move;
            SetLayers();
        }

        public void SetLayers()
        {
            parallaxLayers.Clear();
            for (var _i = 0; _i < transform.childCount; _i++)
            {
                ParallaxLayer _layer = transform.GetChild(_i).GetComponent<ParallaxLayer>();

                if (_layer == null) continue;
                _layer.name = "Layer-" + _i;
                parallaxLayers.Add(_layer);
            }
        }

        private void Move(float delta)
        {
            foreach (ParallaxLayer _layer in parallaxLayers)
            {
                _layer.Move(delta);
            }
        }
    }
}
