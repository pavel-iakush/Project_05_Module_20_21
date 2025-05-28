using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _ship;

    void LateUpdate()
    {
        transform.position = _ship.position;
    }
}
