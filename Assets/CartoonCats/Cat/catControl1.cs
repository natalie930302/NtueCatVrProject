using UnityEngine;

public class CatControl1 : MonoBehaviour
{
    public float moveSpeed = 5f;     // 移動速度
    public float rotateSpeed = 180f; // 旋轉速度

    private Animator animator;       // 動畫控制器

    void Start()
    {
        // 獲取 Animator 組件
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Animator component is missing on this GameObject.");
        }
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // 檢查是否有移動輸入
        bool isMoving = false;

        // 前進 (W)
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
            isMoving = true;
        }

        // 後退 (S)
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.Self);
            isMoving = true;
        }

        // 左轉 (A)
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        }

        // 右轉 (D)
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }

        // 更新動畫
        if (animator != null)
        {
            animator.SetBool("IsRun", isMoving);
        }
    }
}
