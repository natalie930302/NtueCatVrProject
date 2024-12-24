using UnityEngine;
using UnityEngine.InputSystem;

public class CatAttackManager : MonoBehaviour
{
    private Animator catAnimator;  // 貓的 Animator 組件
    private InputManager input;    // 輸入系統管理

    private void Awake()
    {
        // 獲取 Animator 組件
        catAnimator = GetComponent<Animator>();
        // 初始化輸入系統
        input = new InputManager();

        // 設定攻擊輸入事件
        input.cat.attack.performed += ctx => Attack();
    }

    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();

    private void Attack()
    {
        // 設定攻擊觸發器
        catAnimator.SetTrigger("attack");
    }
}