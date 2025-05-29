using UnityEngine;

public class ExplosionService
{
    private readonly RaycastService _raycast;
    private readonly LayerMask _explodableLayer;
    private readonly LayerMask _groundLayer;
    private readonly ParticleSystem _explosionEffect;

    private float _upwardsValue = 0.1f;
    private float _force = 10.0f;
    private float _radius = 5.0f;

    public ExplosionService(RaycastService raycast, LayerMask explodableLayer,
                         LayerMask groundLayer, ParticleSystem explosionEffect)
    {
        _raycast = raycast;
        _explodableLayer = explodableLayer;
        _groundLayer = groundLayer;
        _explosionEffect = explosionEffect;
    }

    public void TryCreateExplosion(Vector3 mousePosition)
    {
        if (!_raycast.TryGetRaycastHit(mousePosition, _groundLayer, out var groundHit))
            return;

        PlayExplosionEffect(groundHit.point);
        ApplyExplosionForce(groundHit.point, _force, _radius);
    }

    private void PlayExplosionEffect(Vector3 position)
    {
        if (_explosionEffect == null)
            return;

        Object.Instantiate(_explosionEffect, position, Quaternion.identity);
    }

    private void ApplyExplosionForce(Vector3 center, float force, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(center, radius, _explodableLayer);

        foreach (Collider collider in colliders)
        {
            Rigidbody hitRigidbody = collider.GetComponent<Rigidbody>();

            if (hitRigidbody != null)
            {
                hitRigidbody.AddExplosionForce(force, center, radius, _upwardsValue, ForceMode.Impulse);
            }
        }
    }
}