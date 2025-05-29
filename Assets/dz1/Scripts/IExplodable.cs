using UnityEngine;

public interface IExplodable
{
    void ApplyExplosion(Vector3 point, float force, float radius);
}