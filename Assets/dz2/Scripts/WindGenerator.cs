using UnityEngine;

public class WindGenerator : MonoBehaviour
{
    [SerializeField] private float _windChangePeriod = 5.0f;

    private float _timeSinceLastChange;
    private Vector3 _windDirection;

    public Vector3 WindDirection => _windDirection;

    private void Awake()
    {
        RandomWindDirection();
    }

    private void Update()
    {
        _timeSinceLastChange += Time.deltaTime;

        if (_timeSinceLastChange >= _windChangePeriod)
        {
            RandomWindDirection();
            _timeSinceLastChange = 0.0f;
        }
    }

    private void RandomWindDirection()
    {
        float randomAngle = Random.Range(0.0f, 360.0f);

        _windDirection = Quaternion.Euler(0.0f, randomAngle, 0.0f) * Vector3.forward;
    }
}