using UnityEngine;

public interface IGrabbable
{
    void OnGrab(Vector3 mousePosition);
    void OnHold(Vector3 mousePosition);
    void OnRelease();
}