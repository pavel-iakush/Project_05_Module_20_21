using UnityEngine;

public class ItemBehaviour : MonoBehaviour, IDraggable, IExplodable
{
    private Rigidbody _rb;
    private Vector3 _originalScale;
    private bool _isDragged;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _originalScale = transform.localScale;
    }

    // Реализация IDraggable
    public void OnGrab()
    {
        _isDragged = true;
        _rb.isKinematic = true;
        transform.localScale = _originalScale * 1.1f;
    }

    public void OnRelease()
    {
        _isDragged = false;
        _rb.isKinematic = false;
        transform.localScale = _originalScale;
    }

    // Реализация IExplodable
    public void ApplyExplosion(Vector3 explosionCenter, float force, float radius)
    {
        if (!_isDragged && _rb != null)
        {
            _rb.AddExplosionForce(force, explosionCenter, radius);
        }
    }
}