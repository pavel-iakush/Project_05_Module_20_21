using UnityEngine;

public class RotateByWind : MonoBehaviour
{
    [SerializeField] private WindGenerator _windGenerator;
    [SerializeField] private float _rotationSpeed = 10.0f;

    private void Update()
    {
        if (_windGenerator == null)
            return;

        ProcessRotation();
    }

    private void ProcessRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(-_windGenerator.WindDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}