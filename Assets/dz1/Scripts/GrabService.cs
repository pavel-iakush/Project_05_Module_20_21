using UnityEngine;

public class GrabService : IGrabbable
{
    private readonly RaycastService _raycast;
    private readonly LayerMask _grabbableLayer;
    private readonly LayerMask _groundLayer;

    private Transform _currentGrabbed;
    private Rigidbody _rigidbody;
    private Vector3 _dragOffset;

    private Vector3 _originalScale;
    private float _grabbedScale = 1.1f;

    public GrabService(RaycastService raycast, LayerMask grabbableLayer, LayerMask groundLayer)
    {
        _raycast = raycast;
        _grabbableLayer = grabbableLayer;
        _groundLayer = groundLayer;
    }

    public void OnGrab(Vector3 mousePosition)
    {
        if (_raycast.HasHit(mousePosition, out RaycastHit hit, _grabbableLayer))
        {
            _currentGrabbed = hit.transform;
            _rigidbody = _currentGrabbed.GetComponent<Rigidbody>();
            _originalScale = _currentGrabbed.localScale;

            if (_raycast.HasHit(mousePosition, out RaycastHit groundHit, _groundLayer))
            {
                _rigidbody.isKinematic = true;
                _currentGrabbed.localScale = _originalScale * _grabbedScale;

                _dragOffset = _currentGrabbed.position - groundHit.point;
            }
        }
    }

    public void OnHold(Vector3 mousePosition)
    {
        if (_currentGrabbed == null)
            return;

        if (_raycast.HasHit(mousePosition, out RaycastHit hit, _groundLayer))
        {
            _currentGrabbed.position = hit.point + _dragOffset;
        }
    }

    public void OnRelease()
    {
        if (_rigidbody == null)
            return;

        _rigidbody.isKinematic = false;
        _currentGrabbed.localScale = _originalScale;

        _currentGrabbed = null;
        _rigidbody = null;
    }
}