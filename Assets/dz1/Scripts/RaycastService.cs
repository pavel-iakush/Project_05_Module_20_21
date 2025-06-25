using UnityEngine;

namespace Refactoring
{
    public class RaycastService
    {
        public bool HasHit(Vector3 origin, Vector3 direction, LayerMask layerMask, out RaycastHit hit)
        {
            Ray ray = new Ray(origin, direction);
            
            return Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
        }
    }
}