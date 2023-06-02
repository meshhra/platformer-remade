using UnityEngine;

namespace Visuals.Parallax
{
    [ExecuteInEditMode]
    public class ParallaxLayer : MonoBehaviour
    {
        public float parallaxFactor;
        public void Move(float delta)
        {
            var _transform = transform;
            var _newPos = _transform.localPosition;
            _newPos.x -= delta * parallaxFactor;
            _transform.localPosition = _newPos;
        }
    }

}
