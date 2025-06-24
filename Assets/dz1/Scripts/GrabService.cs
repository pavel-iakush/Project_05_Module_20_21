using System.Drawing;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace Refactoring
{
    public class GrabService
    {
        private IGrabbable _currentGrabbable;

        private Transform _currentTransform;
        private Vector3 _liftUp = new Vector3(0f, 0.75f, 0f);
        private Vector3 _dragOffset;

        public void GrabCurrent(RaycastHit objectHit, RaycastHit groundHit)
        {
            _currentGrabbable = objectHit.collider.GetComponent<IGrabbable>();
            _currentGrabbable.OnGrab();

            _currentTransform = objectHit.transform;

            _dragOffset = _currentTransform.position - groundHit.point;
        }

        public void HoldCurrent(RaycastHit hit)
        {
            if (_currentGrabbable == null)
                return;

            _currentTransform.position = hit.point + _dragOffset;
        }

        public void ReleaseCurrent()
        {
            if (_currentGrabbable != null)
            {
                _currentGrabbable.OnRelease();

                _currentGrabbable = null;
            }
        }
    }
}