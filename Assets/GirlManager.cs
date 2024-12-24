using UnityEngine;
using UnityEngine.InputSystem;

public class GirlManager : MonoBehaviour
{
    private Animator animator; // 動畫控制器
    private Transform root; // 物件的根節點，用於移動和旋轉
    private InputManager input; // 輸入管理器，用於接收玩家輸入

    [Header("移動參數")]
    [SerializeField] private float moveSpeed = 3f; // 移動速度
    [SerializeField] private float rotationSpeed = 500f; // 旋轉速度

    private Vector3 currentMoveDirection = Vector3.zero; // 當前移動方向
    private bool isGathering = false; // 狀態追蹤

    private void Awake()
    {
        // 初始化動畫控制器
        animator = GetComponent<Animator>();

        // 設置根節點
        root = transform;

        // 初始化輸入管理器並綁定事件
        input = new InputManager();
        input.girl.move.performed += ctx => UpdateMoveDirection(ctx.ReadValue<Vector2>());
        input.girl.move.canceled += ctx => currentMoveDirection = Vector3.zero;
        input.girl.gather.performed += ctx => Gather(); // 切換 gather 狀態
        // input.girl.jump.performed += ctx => Jump();
        // input.girl.attack.performed += ctx => Attack();
    }

    private void OnEnable() => input.Enable();

    private void OnDisable() => input.Disable();

    private void Update()
    {
        Move();
    }

    private void UpdateMoveDirection(Vector2 inputDirection)
    {
        // 將輸入方向轉換為本地座標系中的三維向量
        currentMoveDirection = root.forward * inputDirection.y + root.right * inputDirection.x;
        currentMoveDirection = currentMoveDirection.normalized; // 正規化方向向量
    }

    private void Move()
    {
        // 判斷是否有移動輸入
        bool isRunning = currentMoveDirection != Vector3.zero;

        // 更新動畫參數
        animator.SetBool("IsRun", isRunning);

        if (!isRunning)
        {
            // 無移動輸入時退出
            return;
        }

        // 計算旋轉方向並平滑旋轉
        Quaternion targetRotation = Quaternion.LookRotation(currentMoveDirection, Vector3.up);
        root.rotation = Quaternion.RotateTowards(root.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // 執行移動（使用物件自身的方向）
        root.position += currentMoveDirection * moveSpeed * Time.deltaTime;
    }
    private void Gather()
{
    if (isGathering)
    {
        // 返回 Idle 狀態
        isGathering = false;
        animator.SetBool("IsGathering", false);
    }
    else
    {
        // 進入 Gather 狀態
        isGathering = true;
        animator.SetBool("IsGathering", true);
        animator.SetTrigger("GatherTrigger");
    }
}

    // private void Jump()
    // {
    //     animator.SetTrigger("jump");
    // }

    // private void Attack()
    // {
    //     animator.SetTrigger("attack");
    // }
}
