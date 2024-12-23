using UnityEngine;

public class SyncXRRigWithCharacter : MonoBehaviour
{
    public Transform character; // 角色模型的 Transform
    public Transform xrRig;     // XR Rig 的 Transform

    void Update()
    {
        // 將 XR Rig 的位置和旋轉同步到角色
        xrRig.position = character.position;
        xrRig.rotation = character.rotation;
    }
}
