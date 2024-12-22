using UnityEngine;

public class CatJumpController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // 獲取 Animator 組件
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 按下空白鍵觸發 Jump 動畫
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump_F_RM");
        }
    }
}

