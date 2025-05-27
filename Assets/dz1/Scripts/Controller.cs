using UnityEngine;

public class CubeController : MonoBehaviour, IClickable
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private LayerMask _cubeLayer;
    [SerializeField] private LayerMask _groundLayer;

    private GameObject _selectedCube;

    private Vector3 dragOffset;
    private int _leftMouseButton = 0;
    private int _rightMouseButton = 1;
    private float _upwardsValue = 0.1f;

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            HandleLeftClickDown(Input.mousePosition);
        }

        if (Input.GetMouseButton(_leftMouseButton))
        {
            HandleLeftClickHold(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(_leftMouseButton))
        {
            HandleLeftClickUp();
        }

        if (Input.GetMouseButtonDown(_rightMouseButton))
        {
            HandleRightClick(Input.mousePosition);
        }
    }

    public void HandleLeftClickDown(Vector3 mousePosition)
    {
        if (TryGetRaycastHit(mousePosition, _cubeLayer, out RaycastHit cubeHit))
        {
            _selectedCube = cubeHit.collider.gameObject;

            if (TryGetRaycastHit(mousePosition, _groundLayer, out RaycastHit groundHit))
            {
                dragOffset = _selectedCube.transform.position - groundHit.point;
            }
        }
    }

    public void HandleLeftClickHold(Vector3 mousePosition)
    {
        if (_selectedCube == null)
            return;

        if (TryGetRaycastHit(mousePosition, _groundLayer, out RaycastHit hit))
        {
            _selectedCube.transform.position = hit.point + dragOffset;
        }
    }

    public void HandleLeftClickUp()
    {
        _selectedCube = null;
    }

    public void HandleRightClick(Vector3 mousePosition)
    {
        if (TryGetRaycastHit(mousePosition, _groundLayer, out RaycastHit hit))
        {
            CreateExplosion(hit.point);
            PlayExplosionEffects(hit.point);
        }
    }

    private bool TryGetRaycastHit(Vector3 mousePosition, LayerMask layerMask, out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
    }

    private void CreateExplosion(Vector3 center)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, _explosionRadius, _cubeLayer);
        foreach (Collider hitCollider in hitColliders)
        {
            Rigidbody hitRigidbody = hitCollider.GetComponent<Rigidbody>();
            if (hitRigidbody != null)
            {
                hitRigidbody.AddExplosionForce(_explosionForce, center, _explosionRadius, _upwardsValue, ForceMode.Impulse);
            }
        }
    }

    private void PlayExplosionEffects(Vector3 position)
    {
            Instantiate(_explosionEffect, position, Quaternion.identity);
    }
}