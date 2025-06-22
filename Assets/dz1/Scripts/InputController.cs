using UnityEngine;

namespace Refactoring
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private LayerMask _grabLayer;
        [SerializeField] private LayerMask _explosionLayer;
        [SerializeField] private ParticleSystem _explosionEffect;

        private GrabService _grabService;
        private ExplosionService _explosionService;
        private RaycastService _raycastService;

        private float _initialGrabHeight;

        private void Awake()
        {
            _grabService = new GrabService();
            _explosionService = new ExplosionService(_explosionEffect);
            _raycastService = new RaycastService();
        }

        private void Update()
        {
            ProcessGrab();
            ProcessExplosion();
        }

        private void ProcessGrab()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_raycastService.HasHit(Input.mousePosition, _grabLayer, out RaycastHit hit))
                {
                    _initialGrabHeight = hit.point.y;
                    Vector3 grabPosition = new Vector3(hit.point.x, _initialGrabHeight, hit.point.z);

                    _grabService.TryGrabFromHit(hit);
                    _grabService.UpdatePosition(grabPosition);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _grabService.ReleaseCurrent();
            }

            if (Input.GetMouseButton(0))
            {
                if (_raycastService.HasHit(Input.mousePosition, _grabLayer, out RaycastHit hit))
                {
                    Vector3 newPosition = new Vector3(hit.point.x, _initialGrabHeight, hit.point.z);
                    _grabService.UpdatePosition(newPosition);
                }
            }
        }

        private void ProcessExplosion()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (_raycastService.HasHit(Input.mousePosition, _explosionLayer, out RaycastHit hit))
                    _explosionService.CreateExplosionAtPoint(hit.point, 10f, 5f);
            }
        }
    }
}