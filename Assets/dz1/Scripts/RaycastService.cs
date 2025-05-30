using UnityEngine;

public class RaycastService
{
    public bool HasHit(Vector3 mousePosition, out RaycastHit hit, LayerMask layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
    }
}
