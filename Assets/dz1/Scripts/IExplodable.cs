using UnityEngine;

namespace Refactoring
{
    public interface IExplodable
    {
        void ApplyExplosion(Vector3 mousePosition, float force, float radius);
    }
}