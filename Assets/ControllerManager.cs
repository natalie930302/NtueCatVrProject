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

        // 檢查抓取按鈕是否按下
        if (IsTriggerPressed(interactor))
        {
            // 有抓到東西嗎？
            if (interactor.selectTarget != null)
            {
                GameObject grabbedObject = interactor.selectTarget.gameObject;

                // 嘗試取得 cubeType
                CubeTypeIdentifier identifier = grabbedObject.GetComponent<CubeTypeIdentifier>();
                if (identifier != null)
                {
                    Debug.Log("抓到了物件：" + identifier.cubeType);
                }
            }
        }
    }

    // 判斷是否按下 trigger 鍵
    private bool IsTriggerPressed(UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor interactor)
    {
        if (interactor.inputDevice.IsPressed(grabButton, out bool isPressed, activationThreshold))
        {
            return isPressed;
        }
        return false;
    }
}
