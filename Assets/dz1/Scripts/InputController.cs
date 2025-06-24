using UnityEngine;

namespace Refactoring
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private LayerMask _grabbableLayer;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private ParticleSystem _explosionEffect;

        private GrabService _grabService;
        private ExplosionService _explosionService;
        private RaycastService _raycastService;

        private int _leftMouseButton = 0;
        private int _rightMouseButton = 1;

        private float _force = 10f;
        private float _radius = 4f;

        private RaycastHit _groundHit;
        private RaycastHit _objectHit;
        
        private void Awake()
        {
            _grabService = new GrabService();
            _explosionService = new ExplosionService(_explosionEffect);
            _raycastService = new RaycastService(Camera.main);
        }

        private void Update()
        {
            ProcessGrab();
            ProcessExplosion();
        }

        private void ProcessGrab()
        {
            if (Input.GetMouseButtonDown(_leftMouseButton) && _raycastService.HasHit(Input.mousePosition, _grabbableLayer, out _objectHit))
            {
                _raycastService.HasHit(Input.mousePosition, _groundLayer, out _groundHit);
                _grabService.GrabCurrent(_objectHit, _groundHit);
            }

            if (Input.GetMouseButton(_leftMouseButton) && _raycastService.HasHit(Input.mousePosition, _groundLayer, out _groundHit))
                _grabService.HoldCurrent(_groundHit);

            if (Input.GetMouseButtonUp(_leftMouseButton))
                _grabService.ReleaseCurrent();
        }

        private void ProcessExplosion()
        {
            if (Input.GetMouseButtonDown(_rightMouseButton) && _raycastService.HasHit(Input.mousePosition, _groundLayer, out _groundHit))
                _explosionService.CreateExplosionAtPoint(_groundHit.point, _force, _radius);
        }
    }
}