using UnityEngine;

public interface IClickable
{
    void HandleLeftClickDown(Vector3 mousePosition);
    void HandleLeftClickHold(Vector3 mousePosition);
    void HandleLeftClickUp();
    void HandleRightClick(Vector3 mousePosition);
}