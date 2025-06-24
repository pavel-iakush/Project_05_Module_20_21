using UnityEngine;

namespace Refactoring
{
    public class RaycastService
    {
        private Camera _camera;

        public RaycastService(Camera camera)
        {
            _camera = camera;
        }

        public bool HasHit(Vector3 position, LayerMask layerMask, out RaycastHit hit)
        {
            Ray ray = _camera.ScreenPointToRay(position);
            return Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
        }
    }
}