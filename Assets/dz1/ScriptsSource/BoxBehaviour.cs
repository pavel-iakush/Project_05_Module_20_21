using UnityEngine;

public class BoxBehaviour : MonoBehaviour, IGrabbable, IExplodable
{
    private RaycastService _raycast;
    private LayerMask _explodableLayer;
    private LayerMask _grabbableLayer;
    private LayerMask _groundLayer;
    private ParticleSystem _explosionEffect;

    private float _upwardsValue = 0.1f;
    private float _force = 10.0f;
    private float _radius = 5.0f;

    private Transform _currentGrabbed;
    private Rigidbody _rigidbody;
    private Vector3 _dragOffset;

    private Vector3 _originalScale;
    private float _grabbedScale = 1.1f;

    public void ApplyExplosion(Vector3 mousePosition)
    {
        if (_raycast.HasHit(mousePosition, out RaycastHit groundHit, _groundLayer))
        {
            ParticleSystem explosion = Object.Instantiate(_explosionEffect, groundHit.point, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(groundHit.point, _radius, _explodableLayer);

            foreach (Collider collider in colliders)
            {
                Rigidbody hitRigidbody = collider.GetComponent<Rigidbody>();

                if (hitRigidbody != null)
                {
                    hitRigidbody.AddExplosionForce(_force, groundHit.point, _radius, _upwardsValue, ForceMode.Impulse);
                }
            }
        }
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