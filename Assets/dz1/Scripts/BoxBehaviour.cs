using UnityEngine;

namespace Refactoring
{
    public class BoxBehaviour : MonoBehaviour, IGrabbable, IExplodable
    {
        private Rigidbody _rigidbody;
        private float _upwardsValue = 0.1f;

        private Vector3 _originalScale;
        private float _grabbedScale = 1.1f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void OnGrab()
        {
            _rigidbody.isKinematic = true;
            
            _originalScale = gameObject.transform.localScale;
            gameObject.transform.localScale *= _grabbedScale;
        }

        public void OnRelease()
        {
            _rigidbody.isKinematic = false;
            
            gameObject.transform.localScale = _originalScale;
        }

        public void ApplyExplosion(Vector3 position, float force, float radius)
        {
            _rigidbody.AddExplosionForce(force, position, radius, _upwardsValue, ForceMode.Impulse);
        }
    }
}