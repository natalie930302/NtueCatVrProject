using UnityEngine;

public class DebugCameraPosition : MonoBehaviour
{
    public Transform headTransform;

    void Update()
    {
        Debug.Log("Camera Position: " + transform.position + ", Rotation: " + transform.rotation);
    }
    void LateUpdate()
    {
        transform.position = headTransform.position;
        transform.rotation = headTransform.rotation;
    }
}
