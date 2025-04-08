using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRControllerManager : MonoBehaviour
{
    public ActionBasedController controller; // 指向控制器
    public GameObject grabbedObject = null;
    public string objectType = "";

    void Update()
    {
        if (IsTriggerPressed() && grabbedObject == null)
        {
            TryGrabObject();
        }
        else if (!IsTriggerPressed() && grabbedObject != null)
        {
            ReleaseObject();
        }
    }

    bool IsTriggerPressed()
    {
        // selectAction.action.ReadValue<float>() 會回傳按壓程度（0.0 ~ 1.0）
        return controller.selectAction.action.ReadValue<float>() > 0.8f;
    }

    void TryGrabObject()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Grabbable"))
            {
                grabbedObject = hit.gameObject;
                objectType = grabbedObject.GetComponent<CubeTypeIdentifier>().cubeType;
                grabbedObject.transform.SetParent(this.transform);
                grabbedObject.transform.localPosition = Vector3.zero;
                break;
            }
        }
    }

    void ReleaseObject()
    {
        grabbedObject.transform.SetParent(null);
        grabbedObject = null;
        objectType = "";
    }
}
