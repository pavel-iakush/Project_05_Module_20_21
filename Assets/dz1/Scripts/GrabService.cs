using UnityEngine;

public class GrabService
{
    private readonly RaycastService _raycast;
    private readonly LayerMask _draggableLayer;
    private readonly LayerMask _groundLayer;

    private IDraggable _currentDragged;
    private Vector3 _dragOffset;

    public GrabService(RaycastService raycast, LayerMask draggableLayer, LayerMask groundLayer)
    {
        _raycast = raycast;
        _draggableLayer = draggableLayer;
        _groundLayer = groundLayer;
    }

    public void TryStartDrag(Vector3 mousePosition)
    {
        if (_raycast.TryGetRaycastHit(mousePosition, _draggableLayer, out RaycastHit hit))
        {
            IDraggable draggable = hit.collider.GetComponent<IDraggable>();
            if (draggable != null)
            {
                _currentDragged = draggable;
                draggable.OnGrab();

                if (_raycast.TryGetRaycastHit(mousePosition, _groundLayer, out RaycastHit groundHit))
                {
                    _dragOffset = hit.transform.position - groundHit.point;
                }
            }
        }
    }

    public void UpdateDrag(Vector3 mousePosition)
    {
        if (_currentDragged == null) return;

        if (_raycast.TryGetRaycastHit(mousePosition, _groundLayer, out RaycastHit groundHit))
        {
            ((MonoBehaviour)_currentDragged).transform.position = groundHit.point + _dragOffset;
        }
    }

    public void EndDrag()
    {
        _currentDragged?.OnRelease();
        _currentDragged = null;
    }
}