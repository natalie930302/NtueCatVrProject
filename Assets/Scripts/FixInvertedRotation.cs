using UnityEngine;

public class FixInvertedRotation : MonoBehaviour
{
    void LateUpdate()
    {
        // 取得目前的旋轉
        Quaternion currentRotation = transform.rotation;

        // 修正旋轉
        Quaternion fixedRotation = new Quaternion(
            -currentRotation.x,  // X 軸取反
            currentRotation.y,   // Y 軸保持不變
            currentRotation.z,   // Z 軸保持不變
            currentRotation.w    // W 軸保持不變
        );

        // 將修正後的旋轉套用到物件上
        transform.rotation = fixedRotation;
    }
}