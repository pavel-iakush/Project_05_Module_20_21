using Unity.Burst.CompilerServices;
using UnityEngine;

namespace Refactoring
{
    public class GrabService
    {
        private IGrabbable _currentGrabbable;

        private Transform _currentTransform;
        private Vector3 _dragOffset;

        public void GrabCurrent(RaycastHit hit)
        {
            _currentGrabbable = hit.collider.GetComponent<IGrabbable>();
            _currentGrabbable.OnGrab();

            _currentTransform = hit.transform;
            _dragOffset = _currentTransform.position - hit.point;
        }

        public void HoldCurrent(RaycastHit hit)
        {
            if (_currentGrabbable == null)
                return;
            
            if (_currentTransform != null)
            {               
                _currentTransform.position = hit.point + _dragOffset;
            }    
        }

        public void ReleaseCurrent()
        {
            if (_currentGrabbable != null)
            {
                _currentGrabbable.OnRelease();

                _currentGrabbable = null;
                _currentTransform = null;
            }
        }
    }
}