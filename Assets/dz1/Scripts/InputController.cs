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
                if (_raycastService.HasHit(Input.mousePosition, _grabbableLayer, out RaycastHit hit))
                {
                    _grabService.GrabCurrent(hit);
                    Debug.Log("grab");
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (_raycastService.HasHit(Input.mousePosition, _groundLayer, out RaycastHit hit))
                {
                    _grabService.HoldCurrent(hit);
                    Debug.Log("hold");
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _grabService.ReleaseCurrent();
                Debug.Log("release");
            }
        }

        private void ProcessExplosion()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (_raycastService.HasHit(Input.mousePosition, _groundLayer, out RaycastHit hit))
                    _explosionService.CreateExplosionAtPoint(hit.point, 10f, 5f);
            }
        }
    }
}