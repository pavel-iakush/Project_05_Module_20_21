using UnityEngine;

namespace Refactoring
{
    public class RaycastService
    {
        public bool HasHit(Vector3 position, LayerMask layerMask, out RaycastHit hit)
        {
            Ray ray = Camera.main.ScreenPointToRay(position);
            return Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
        }
    }
}