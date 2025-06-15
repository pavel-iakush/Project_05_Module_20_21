using UnityEngine;

public class SailRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 100.0f;

    private float _maxSailAngle = 90.0f;
    private float _currentSailAngle;
    private Vector3 _sailDirection;

    private KeyCode _rotateLeftKey = KeyCode.A;
    private KeyCode _rotateRightKey = KeyCode.D;

    public Vector3 SailDirection => _sailDirection;

    private void Update()
    {
        HandleInput();
        UpdateSailTransform();
    }

    private void HandleInput()
    {
        float sailInput = 0.0f;

        if (Input.GetKey(_rotateLeftKey))
            sailInput = -1.0f;

        if (Input.GetKey(_rotateRightKey))
            sailInput = 1.0f;

        _currentSailAngle = Mathf.Clamp(
            _currentSailAngle + sailInput * _rotationSpeed * Time.deltaTime,
            -_maxSailAngle,
            _maxSailAngle);
    }

    private void UpdateSailTransform()
    {
        transform.localRotation = Quaternion.Euler(0.0f, _currentSailAngle, 0.0f);

        _sailDirection = transform.TransformDirection(Vector3.forward);
    }
}