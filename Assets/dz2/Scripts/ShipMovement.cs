using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private WindGenerator _windGenerator;
    [SerializeField] private SailRotator _sailRotator;
    [SerializeField] private ShipRotator _shipRotator;

    private Rigidbody _rigidbody;
    private float _deadZone = 0.01f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float sailAling = Vector3.Dot(_windGenerator.WindDirection, _sailRotator.SailDirection);
        float shipAlign = Vector3.Dot(_windGenerator.WindDirection, _shipRotator.ShipDirection);

        if (sailAling > _deadZone)
        {
            if (shipAlign > _deadZone)
            {
                _rigidbody.velocity = _shipRotator.ShipDirection * sailAling * shipAlign;
            }
        }
    }
}