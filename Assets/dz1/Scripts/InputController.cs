using Unity.Burst.CompilerServices;
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
        private Camera _camera;
        
        private void Awake()
        {
            _grabService = new GrabService();
            _explosionService = new ExplosionService(_explosionEffect);
            _raycastService = new RaycastService();

            _camera = Camera.main;
        }

        private void Update()
        {
            ProcessGrab();
            ProcessExplosion();
        }

        private void ProcessGrab()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(_leftMouseButton) && IsObjectClicked(out RaycastHit objectHit))
            {
                _raycastService.HasHit(ray.origin, ray.direction, _groundLayer, out _groundHit);
                _grabService.GrabCurrent(objectHit, _groundHit);
            }

            if (Input.GetMouseButton(_leftMouseButton) && IsGroundClicked(out RaycastHit groundHit))
                _grabService.HoldCurrent(groundHit);

            if (Input.GetMouseButtonUp(_leftMouseButton))
                _grabService.ReleaseCurrent();
        }

        private void ProcessExplosion()
        {
            if (Input.GetMouseButtonDown(_rightMouseButton) && IsGroundClicked(out RaycastHit groundHit))
                _explosionService.CreateExplosionAtPoint(groundHit.point, _force, _radius);
        }

        private bool IsObjectClicked(out RaycastHit objectHit)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            return _raycastService.HasHit(ray.origin, ray.direction, _grabbableLayer, out objectHit);
        }

        private bool IsGroundClicked(out RaycastHit groundHit)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            return _raycastService.HasHit(ray.origin, ray.direction, _groundLayer, out groundHit);
        }
    }
}