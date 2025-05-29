using UnityEngine;

public class RaycastService
{
    public bool TryGetRaycastHit(Vector3 mousePosition, LayerMask layerMask, out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
    }
}