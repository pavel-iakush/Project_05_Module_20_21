using UnityEngine;

public class ShipRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 30f;

    private KeyCode _rotateLeftKey = KeyCode.Q;
    private KeyCode _rotateRightKey = KeyCode.E;

    private Rigidbody _rigidbody;
    private Vector3 _shipDirection;

    public Vector3 ShipDirection => _shipDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        if (Input.GetKey(_rotateLeftKey))
        {
            _rigidbody.AddTorque(Vector3.up * -_rotationSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(_rotateRightKey))
        {
            _rigidbody.AddTorque(Vector3.up * _rotationSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }

        _shipDirection = transform.TransformDirection(Vector3.forward);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(transform.position, _shipDirection * 5f);
    //}
}