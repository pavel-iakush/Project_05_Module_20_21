using UnityEngine;

public class ExplosionService : IExplodable
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
}