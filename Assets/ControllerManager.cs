using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerManager : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor interactor; // 手柄抓取器
    public InputHelpers.Button grabButton = InputHelpers.Button.Trigger;
    public float activationThreshold = 0.1f;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable currentInteractable;

    void Update()
    {
        if (interactor == null) return;

        if (IsTriggerPressed(interactor))
        {
            // 安全取得目前選中的第一個 Interactable
            if (interactor.interactablesSelected.Count > 0)
            {
                var selectedInteractable = interactor.interactablesSelected[0];
                GameObject grabbedObject = selectedInteractable.transform.gameObject;

                CubeTypeIdentifier identifier = grabbedObject.GetComponent<CubeTypeIdentifier>();
                if (identifier != null)
                {
                    Debug.Log("抓到了物件：" + identifier.cubeType);
                }
            }
        }
    }



    // 判斷是否按下 trigger 鍵
    bool IsTriggerPressed(UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor)
    {
        var controllerInteractor = interactor as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor;
        if (controllerInteractor != null)
        {
            InputDevice device = controllerInteractor.xrController.inputDevice;
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue))
            {
                return triggerValue;
            }
        }
        return false;
    }

}
