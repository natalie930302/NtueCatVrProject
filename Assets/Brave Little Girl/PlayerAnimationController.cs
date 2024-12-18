using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;
    void Start()
    {
       // 獲取 Animator 組件
        animator = GetComponent<Animator>(); 
        // 檢查 Animator 是否正確附加
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on this GameObject!");
        }
    }

    // Update is called once per frame
    void Update()
    {
       // Idle 狀態（初始狀態）
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetTrigger("Idle");
        }

        // 動作對應按鍵
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetTrigger("Walk");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetTrigger("Walk0");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetTrigger("Run");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetTrigger("Run0");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetTrigger("Attack1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetTrigger("Attack2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SetTrigger("JumpIn");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SetTrigger("Fall");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SetTrigger("JumpOut");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetTrigger("Win");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetTrigger("Lose");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetTrigger("GatheringObject");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetTrigger("LookingDown");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetTrigger("SittingIdle");
        } 
    }
    /// <summary>
    /// 設置指定的 Trigger 並重置其他觸發器
    /// </summary>
    /// <param name="triggerName">要設置的 Trigger 名稱</param>
    private void SetTrigger(string triggerName)
    {
        // 確保 Animator 存在
        if (animator == null)
        {
            Debug.LogError("Animator is not assigned or missing!");
            return;
        }

        // 重置所有觸發器，避免觸發衝突
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(parameter.name);
            }
        }

        // 設置目標觸發器
        animator.SetTrigger(triggerName);
    }
}
