using UnityEngine;

namespace Refactoring
{
    public class GrabService
    {
        private IGrabbable _currentGrabbable;
        private Transform _currentTransform;

        public bool TryGrabFromHit(RaycastHit hit)
        {
            IGrabbable grabbable = hit.collider.GetComponent<IGrabbable>();

            if (grabbable != null)
            {
                GrabCurrent(grabbable, hit.transform);
                return true;
            }

            return false;
        }

        public void GrabCurrent(IGrabbable grabbable, Transform objectTransform)
        {
            _currentGrabbable = grabbable;
            _currentTransform = objectTransform;
            _currentGrabbable.OnGrab();
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

        public void UpdatePosition(Vector3 position)
        {
            if (_currentTransform != null)
                _currentTransform.position = position;
        }
    }
}