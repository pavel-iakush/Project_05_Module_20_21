using UnityEngine;

public class MouseRaycaster : MonoBehaviour
{
    public Vector3 WorldPosition;

    private void Update()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Physics.Raycast(cameraRay, out RaycastHit hitInfo);



        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo))
        {
            //Debug.Log(hitInfo.collider.gameObject.name);
            //Debug.Log(hitInfo.point.y);

            WorldPosition = hitInfo.point;
            Debug.Log(WorldPosition);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(cameraRay.origin, cameraRay.direction * 100);
    //    Gizmos.DrawSphere(WorldPosition, 1);
    //}
}