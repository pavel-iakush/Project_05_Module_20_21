using UnityEngine;

namespace Refactoring
{
    public class ExplosionService
    {
        private readonly ParticleSystem _explosionEffect;

        public ExplosionService(ParticleSystem explosionEffect)
        {
            _explosionEffect = explosionEffect;
        }

        public void CreateExplosionAtPoint(Vector3 point, float force, float radius)
        {
            Object.Instantiate(_explosionEffect, point, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(point, radius);

            foreach (Collider collider in colliders)
            {
                IExplodable explodable = collider.GetComponent<IExplodable>();

                if (explodable != null)
                    explodable.ApplyExplosion(point, force, radius);
            }
        }
    }
}